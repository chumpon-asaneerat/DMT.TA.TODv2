﻿#region Using

using System;
using System.Windows;

using NLib;
using NLib.Logs;

#endregion

namespace DMT
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Services.TODWebServer appServ = null;

        /// <summary>
        /// OnStartup.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Console.WriteLine("OnStartUp");
            if (null != AppDomain.CurrentDomain)
            {
                if (null != System.Threading.Thread.CurrentContext)
                {
                    Console.WriteLine("Thread CurrentContext is not null.");
                }
            }

            #region Create Application Environment Options

            EnvironmentOptions option = new EnvironmentOptions()
            {
                /* Setup Application Information */
                AppInfo = new NAppInformation()
                {
                    /*  This property is required */
                    CompanyName = "DMT",
                    /*  This property is required */
                    ProductName = AppConsts.Application.TOD.ApplicationName,
                    /* For Application Version */
                    Version = AppConsts.Application.TOD.Version,
                    Minor = AppConsts.Application.TOD.Minor,
                    Build = AppConsts.Application.TOD.Build,
                    LastUpdate = AppConsts.Application.TOD.LastUpdate
                },
                /* Setup Storage */
                Storage = new NAppStorage()
                {
                    StorageType = NAppFolder.ProgramData
                },
                /* Setup Behaviors */
                Behaviors = new NAppBehaviors()
                {
                    /* Set to true for allow only one instance of application can execute an runtime */
                    IsSingleAppInstance = true,
                    /* Set to true for enable Debuggers this value should always be true */
                    EnableDebuggers = true
                }
            };

            #endregion

            #region Setup Option to Controller and check instance

            WpfAppContoller.Instance.Setup(option);

            if (option.Behaviors.IsSingleAppInstance &&
                WpfAppContoller.Instance.HasMoreInstance)
            {
                return;
            }

            #endregion

            #region Init Preload classes

            ApplicationManager.Instance.Preload(() =>
            {

            });

            #endregion

            // Load Config service.
            Services.TODConfigManager.Instance.LoadConfig();

            // Start Local Database Service
            Services.TODLocalDbServer.Instance.Start();

            // Start RabbitMQ Service
            Services.RabbitMQService.Instance.Start(Services.TODConfigManager.Instance.RabbitMQ, "TOD");

            // Start App Notify Server.
            appServ = new Services.TODWebServer();
            appServ.Start();

            Window window = null;
            window = new MainWindow();

            // Start log manager
            LogManager.Instance.Start();

            if (null != window)
            {
                WpfAppContoller.Instance.Run(window);
            }
        }
        /// <summary>
        /// OnExit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExit(ExitEventArgs e)
        {
            if (null != appServ)
            {
                appServ.Shutdown();
            }
            appServ = null;

            // Shutdown RabbitMQ Service
            Services.RabbitMQService.Instance.Shutdown();

            // Shutdown Local Database Service
            Services.TODLocalDbServer.Instance.Shutdown();

            // Shutdown log manager
            LogManager.Instance.Shutdown();

            // Wpf shutdown process required exit code.

            /* If auto close the single instance must be true */
            bool autoCloseProcess = true;
            WpfAppContoller.Instance.Shutdown(autoCloseProcess, e.ApplicationExitCode);

            base.OnExit(e);
        }
    }
}
