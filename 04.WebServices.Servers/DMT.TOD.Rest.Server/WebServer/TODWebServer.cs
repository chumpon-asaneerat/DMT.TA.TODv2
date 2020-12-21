#region Using

using System;
using System.Reflection;
using Microsoft.Owin.Hosting;
using System.Web.Http;
using NLib;

#endregion

namespace DMT.Services
{
    /// <summary>
    /// Web Server StartUp class.
    /// </summary>
    public class StartUp : DMTRestServerStartUp
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public StartUp() : base()
        {
            this.AuthenticationValidator = (string userName, string password) =>
            {
                return (userName == "DMTUSER" && password == "DMTPASS2");
            };
            this.EnableSwagger = true;
            this.ApiName = "TOD Application API";
            this.ApiVersion = "v1";
        }

        #endregion

        internal static class MapControllers
        {
            internal static class Notify
            {
                internal static void MapRoutes(HttpConfiguration config)
                {
                    string controllerName, actionName, actionUrl;

                    // Set Controller Name.
                    controllerName = RouteConsts.Notify.ControllerName;

                    // TSB Changed
                    actionName = RouteConsts.Notify.TSBChanged.Name;
                    actionUrl = RouteConsts.Notify.TSBChanged.Url;
                    Helper.MapRoute(config, controllerName, actionName, actionUrl); // Map Route.

                    // Shift Changed
                    actionName = RouteConsts.Notify.ShiftChanged.Name;
                    actionUrl = RouteConsts.Notify.ShiftChanged.Url;
                    Helper.MapRoute(config, controllerName, actionName, actionUrl); // Map Route.
                }
            }
        }

        #region Override Methods

        /// <summary>
        /// Init Map Routes.
        /// </summary>
        /// <param name="config">The HttpConfiguration instance.</param>
        protected override void InitMapRoutes(HttpConfiguration config)
        {
            // Handle route by specificed controller (Route Order is important).

            // Notify
            MapControllers.Notify.MapRoutes(config);

            #region Default Route (do not used)

            // If comment below line the auto map default controllers will not load and cannot access.
            //InitDefaultMapRoute(config);

            #endregion
        }

        #endregion
    }

    /// <summary>
    /// TOD WebServer Web Server (Self Host).
    /// </summary>
    public class TODWebServer
    {
        #region Internal Variables

        private WebServiceConfig _cfg = null;
        private IDisposable server = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TODWebServer() : base() { }

        #endregion

        #region Private Methods

        private void CheckConfig()
        {
            // Gets TOD App local server config.
            _cfg = (null != TODConfigManager.Instance.TODApp) ? TODConfigManager.Instance.TODApp.Service : null;
        }

        private string BaseAddress
        {
            get
            {
                string result = string.Empty;
                if (null != _cfg)
                {
                    result = string.Format(@"{0}://{1}:{2}", _cfg.Protocol, "+", _cfg.PortNumber);
                }
                return result;
            }
        }

        private void InitOwinFirewall()
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            if (null == _cfg)
            {
                med.Err("Server Configuration is null.");
                return;
            }
            string portNum = _cfg.PortNumber.ToString();
            string appName = "DMT TOD App Service(REST)";
            var nash = new CommandLine();
            nash.Run("http add urlacl url=http://+:" + portNum + "/ user=Everyone");
            nash.Run("advfirewall firewall add rule dir=in action=allow protocol=TCP localport=" + portNum + " name=\"" + appName + "\" enable=yes profile=Any");
        }

        private void ReleaseOwinFirewall()
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            if (null == _cfg)
            {
                med.Err("Server Configuration is null.");
                return;
            }
            string portNum = _cfg.PortNumber.ToString();
            string appName = "DMT TOD App Service(REST)";
            var nash = new CommandLine();
            nash.Run("http delete urlacl url=http://+:" + portNum + "/");
            nash.Run("advfirewall firewall delete rule name=\"" + appName + "\"");
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Start service.
        /// </summary>
        public void Start()
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            CheckConfig(); // Check Config.
            if (null == _cfg)
            {
                med.Err("Server Configuration is null.");
                return;
            }

            if (null == server)
            {
                InitOwinFirewall();
                server = WebApp.Start<StartUp>(url: BaseAddress);
            }
            med.Info("TOD App local nofify service start.");
        }
        /// <summary>
        /// Shutdown service.
        /// </summary>
        public void Shutdown()
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            if (null != server)
            {
                server.Dispose();
            }
            server = null;
            ReleaseOwinFirewall();
            med.Info("TOD App local nofify service shutdown.");
        }

        #endregion
    }
}
