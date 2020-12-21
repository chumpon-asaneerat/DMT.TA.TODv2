#region Using

using System;
using System.IO;
using System.Reflection;
using System.Threading;
using NLib;
using NLib.IO;
using Newtonsoft.Json;
using NLib.Controls.Design;

#endregion

namespace DMT.Services
{
    #region Plaza Config and related classes

    #region ApplicationConfig

    /// <summary>
    /// The DMT Config class.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class DMTConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DMTConfig()
        {
            this.network = "4";
            this.tsb = "97";
            this.terminal = "49701";
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// IsEquals.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool IsEquals(object obj)
        {
            if (null == obj || !(obj is DMTConfig)) return false;
            return this.GetString() == (obj as DMTConfig).GetString();
        }
        /// <summary>
        /// GetString.
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            string code = string.Format("network:{0}, tsb:{1}, terminal:{2}",
                this.network, this.tsb, this.terminal);
            return code;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets network.
        /// </summary>
        public string network { get; set; }
        /// <summary>
        /// Gets or sets tsb.
        /// </summary>
        public string tsb { get; set; }
        /// <summary>
        /// Gets or sets terminal.
        /// </summary>
        public string terminal { get; set; }

        #endregion
    }

    #endregion

    #region WebServiceConfig

    /// <summary>
    /// The WebServiceConfig class.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class WebServiceConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public WebServiceConfig()
        {
            this.Protocol = "http";
            this.HostName = "localhost";
            this.PortNumber = 9000;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// IsEquals.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool IsEquals(object obj)
        {
            if (null == obj || !(obj is WebServiceConfig)) return false;
            return this.GetString() == (obj as WebServiceConfig).GetString();
        }
        /// <summary>
        /// GetString.
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            string code = string.Format("{0}://{1}:{2}",
                this.Protocol, this.HostName, this.PortNumber);
            return code;
        }
        
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets protocol.
        /// </summary>
        public string Protocol { get; set; }
        /// <summary>
        /// Gets or sets Host Name or IP Address.
        /// </summary>
        public string HostName { get; set; }
        /// <summary>
        /// Gets or sets port number.
        /// </summary>
        public int PortNumber { get; set; }
        /// <summary>
        /// Gets or sets User Name.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets Password.
        /// </summary>
        public string Password { get; set; }

        #endregion
    }

    #endregion

    #region RabbitMQServiceConfig

    /// <summary>
    /// The RabbitMQServiceConfig class.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class RabbitMQServiceConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RabbitMQServiceConfig()
        {
            HostName = "172.30.73.11";
            PortNumber = 5672;
            VirtualHost = "cbe";
            QueueName = "qp.parameters.th03x009.taa01";
            UserName = "taa";
            Password = "taa123";
            Enabled = true;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// IsEquals.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool IsEquals(object obj)
        {
            if (null == obj || !(obj is RabbitMQServiceConfig)) return false;
            return this.GetString() == (obj as RabbitMQServiceConfig).GetString();
        }
        /// <summary>
        /// GetString.
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            string code = string.Format("Host:{0}, Port: {1}, VHost:{2}, QueueName: {3}",
                this.HostName, this.PortNumber, this.VirtualHost, this.QueueName);
            return code;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets Host Name.
        /// </summary>
        public string HostName { get; set; }
        /// <summary>
        /// Gets or sets Port Number.
        /// </summary>
        public int PortNumber { get; set; }
        /// <summary>
        /// Gets or sets Virtual Host Name.
        /// </summary>
        public string VirtualHost { get; set; }
        /// <summary>
        /// Gets or sets Queue Name.
        /// </summary>
        public string QueueName { get; set; }
        /// <summary>
        /// Gets or sets User Name.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets Password.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets Enabled.
        /// </summary>
        public bool Enabled { get; set; }

        #endregion
    }

    #endregion

    #region TAxTODWebServiceConfig

    /// <summary>
    /// The TAxTODWebServiceConfig class.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class TAxTODWebServiceConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TAxTODWebServiceConfig()
        {
            this.Service = new WebServiceConfig()
            {
                Protocol = "http",
                HostName = "localhost",
                PortNumber = 3000,
                UserName = "DMTUSER",
                Password = "DMTPASS"
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// IsEquals.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool IsEquals(object obj)
        {
            if (null == obj || !(obj is TAxTODWebServiceConfig)) return false;
            return this.GetString() == (obj as TAxTODWebServiceConfig).GetString();
        }
        /// <summary>
        /// GetString.
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            if (null != this.Service)
                return string.Format("{0}", this.Service.GetString());
            else return "TAxTOD http is null.";
        }
        
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets Http service.
        /// </summary>
        public WebServiceConfig Service { get; set; }

        #endregion
    }

    #endregion

    #region SCWWebServiceConfig

    /// <summary>
    /// The SCWWebServiceConfig class.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class SCWWebServiceConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SCWWebServiceConfig()
        {
            this.Service = new WebServiceConfig()
            {
                Protocol = "http",
                HostName = "172.30.192.9",
                PortNumber = 8110,
                UserName = "DMTUSER",
                Password = "DMTPASS"
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// IsEquals.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool IsEquals(object obj)
        {
            if (null == obj || !(obj is SCWWebServiceConfig)) return false;
            return this.GetString() == (obj as SCWWebServiceConfig).GetString();
        }
        /// <summary>
        /// GetString.
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            if (null != this.Service)
                return string.Format("{0}", this.Service.GetString());
            else return "DC http is null.";
        }
        
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets Http service.
        /// </summary>
        public WebServiceConfig Service { get; set; }

        #endregion
    }

    #endregion

    #region TAAppConfig

    /// <summary>
    /// The TAAppConfig class.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class TAAppConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TAAppConfig()
        {
            this.Service = new WebServiceConfig()
            {
                Protocol = "http",
                HostName = "localhost",
                PortNumber = 9001,
                UserName = "DMTUSER",
                Password = "DMTPASS"
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// IsEquals.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool IsEquals(object obj)
        {
            if (null == obj || !(obj is TAAppConfig)) return false;
            return this.GetString() == (obj as TAAppConfig).GetString();
        }
        /// <summary>
        /// GetString.
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            if (null != this.Service)
                return string.Format("{0}", this.Service.GetString());
            else return "TA App http is null.";
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets Http service.
        /// </summary>
        public WebServiceConfig Service { get; set; }

        #endregion
    }

    #endregion

    #region TODAppConfig

    /// <summary>
    /// The TAAppConfig class.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class TODAppConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TODAppConfig()
        {
            this.Service = new WebServiceConfig()
            {
                Protocol = "http",
                HostName = "localhost",
                PortNumber = 9002,
                UserName = "DMTUSER",
                Password = "DMTPASS"
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// IsEquals.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool IsEquals(object obj)
        {
            if (null == obj || !(obj is TODAppConfig)) return false;
            return this.GetString() == (obj as TODAppConfig).GetString();
        }
        /// <summary>
        /// GetString.
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            if (null != this.Service)
                return string.Format("{0}", this.Service.GetString());
            else return "TOD App http is null.";
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets Http service.
        /// </summary>
        public WebServiceConfig Service { get; set; }

        #endregion
    }

    #endregion

    #region TAPlazaConfig

    /// <summary>
    /// The TAPlazaConfig class.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class TAPlazaConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TAPlazaConfig() : base()
        {
            this.DMT = new DMTConfig();
            this.TAxTOD = new TAxTODWebServiceConfig();
            this.SCW = new SCWWebServiceConfig();
            this.RabbitMQ = new RabbitMQServiceConfig();
            this.TODApp = new TODAppConfig();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// IsEquals.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool IsEquals(object obj)
        {
            if (null == obj || !(obj is TAPlazaConfig)) return false;
            return this.GetString() == (obj as TAPlazaConfig).GetString();
        }
        /// <summary>
        /// GetString.
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            string code = string.Empty;
            // Application
            if (null == this.DMT)
            {
                code += "DMT: null" + Environment.NewLine;
            }
            else
            {
                code += string.Format("DMT: {0}",
                    this.DMT.GetString()) + Environment.NewLine;
            }
            // TAxTOD server
            if (null == this.TAxTOD)
            {
                code += "TAxTOD: null" + Environment.NewLine;
            }
            else
            {
                code += string.Format("TAxTOD: {0}", 
                    this.TAxTOD.GetString()) + Environment.NewLine;
            }
            // SCW server
            if (null == this.SCW)
            {
                code += "SCW: null" + Environment.NewLine;
            }
            else
            {
                code += string.Format("DC: {0}", 
                    this.SCW.GetString()) + Environment.NewLine;
            }
            // RabbitMQ
            if (null == this.RabbitMQ)
            {
                code += "RabbitMQ: null" + Environment.NewLine;
            }
            else
            {
                code += string.Format("RabbitMQ: {0}",
                    this.RabbitMQ.GetString()) + Environment.NewLine;
            }
            // TOD Application (Plaza)
            if (null == this.TODApp)
            {
                code += "TODApp: null" + Environment.NewLine;
            }
            else
            {
                code += string.Format("TODApp: {0}",
                    this.TODApp.GetString()) + Environment.NewLine;
            }
            return code;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets DMT Config.
        /// </summary>
        public DMTConfig DMT { get; set; }
        /// <summary>
        /// Gets or sets TAxTOD Service Config.
        /// </summary>
        public TAxTODWebServiceConfig TAxTOD { get; set; }
        /// <summary>
        /// Gets or sets SCW Service Config.
        /// </summary>
        public SCWWebServiceConfig SCW { get; set; }
        /// <summary>
        /// Gets or sets Rabbit MQ Service Config.
        /// </summary>
        public RabbitMQServiceConfig RabbitMQ { get; set; }
        /// <summary>
        /// Gets or sets TOD App Service Config (for notify).
        /// </summary>
        public TODAppConfig TODApp { get; set; }

        #endregion
    }

    #endregion

    #region TODPlazaConfig

    /// <summary>
    /// The TODPlazaConfig class.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class TODPlazaConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TODPlazaConfig() : base()
        {
            this.DMT = new DMTConfig();
            this.TAxTOD = new TAxTODWebServiceConfig();
            this.SCW = new SCWWebServiceConfig();
            this.RabbitMQ = new RabbitMQServiceConfig();
            this.TAApp = new TAAppConfig();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// IsEquals.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool IsEquals(object obj)
        {
            if (null == obj || !(obj is TODPlazaConfig)) return false;
            return this.GetString() == (obj as TODPlazaConfig).GetString();
        }
        /// <summary>
        /// GetString.
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            string code = string.Empty;
            // Application
            if (null == this.DMT)
            {
                code += "DMT: null" + Environment.NewLine;
            }
            else
            {
                code += string.Format("DMT: {0}",
                    this.DMT.GetString()) + Environment.NewLine;
            }
            // TAxTOD server
            if (null == this.TAxTOD)
            {
                code += "TAxTOD: null" + Environment.NewLine;
            }
            else
            {
                code += string.Format("TAxTOD: {0}",
                    this.TAxTOD.GetString()) + Environment.NewLine;
            }
            // SCW server
            if (null == this.SCW)
            {
                code += "SCW: null" + Environment.NewLine;
            }
            else
            {
                code += string.Format("DC: {0}",
                    this.SCW.GetString()) + Environment.NewLine;
            }
            // RabbitMQ
            if (null == this.RabbitMQ)
            {
                code += "RabbitMQ: null" + Environment.NewLine;
            }
            else
            {
                code += string.Format("RabbitMQ: {0}",
                    this.RabbitMQ.GetString()) + Environment.NewLine;
            }
            // TA Application (Plaza)
            if (null == this.TAApp)
            {
                code += "TAApp: null" + Environment.NewLine;
            }
            else
            {
                code += string.Format("TAApp: {0}",
                    this.TAApp.GetString()) + Environment.NewLine;
            }
            return code;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets DMT Config.
        /// </summary>
        public DMTConfig DMT { get; set; }
        /// <summary>
        /// Gets or sets TAxTOD Service Config.
        /// </summary>
        public TAxTODWebServiceConfig TAxTOD { get; set; }
        /// <summary>
        /// Gets or sets SCW Service Config.
        /// </summary>
        public SCWWebServiceConfig SCW { get; set; }
        /// <summary>
        /// Gets or sets Rabbit MQ Service Config.
        /// </summary>
        public RabbitMQServiceConfig RabbitMQ { get; set; }
        /// <summary>
        /// Gets or sets TA App Service Config (for notify).
        /// </summary>
        public TAAppConfig TAApp { get; set; }

        #endregion
    }

    #endregion

    #endregion

    #region Plaza Config and related interfaces

    #region RabbitMQConfig Interface

    /// <summary>
    /// The IRabbitMQConfig inferface.
    /// </summary>
    public interface IRabbitMQConfig
    {
        /// <summary>
        /// Gets RabbitMQ Config.
        /// </summary>
        RabbitMQServiceConfig RabbitMQ { get; }
    }

    #endregion

    #region ISCWConfig Interface

    /// <summary>
    /// The ISCWConfig inferface.
    /// </summary>
    public interface ISCWConfig
    {
        /// <summary>
        /// Gets SCW Config.
        /// </summary>
        SCWWebServiceConfig SCW { get; }
    }

    #endregion

    #region ITAxTODConfig Interface

    /// <summary>
    /// The ITAxTODConfig inferface.
    /// </summary>
    public interface ITAxTODConfig
    {
        /// <summary>
        /// Gets TAxTOD Config.
        /// </summary>
        TAxTODWebServiceConfig TAxTOD { get; }
    }

    #endregion

    #endregion

    #region ConfigManager (abstract)

    /// <summary>
    /// The ConfigManager abstract class.
    /// </summary>
    /// <typeparam name="T">The Config Class Type.</typeparam>
    public abstract class ConfigManager<T> 
        where T:new()
    {
        #region Internal Variables

        private T _cfg = new T();

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ConfigManager() : base()
        {
        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~ConfigManager()
        {
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets Config File Name.
        /// </summary>
        public abstract string FileName { get; }
        /// <summary>
        /// Raise Config Changed Event.
        /// </summary>
        protected void RaiseConfigChanged()
        {
            // Raise event.
            ConfigChanged.Call(this, EventArgs.Empty);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Load Config from file.
        /// </summary>
        public virtual void LoadConfig()
        {
            lock (this)
            {
                MethodBase med = MethodBase.GetCurrentMethod();
                try
                {
                    // save back to file.
                    if (!NJson.ConfigExists(FileName))
                    {
                        // File not exist.
                        if (null == _cfg)
                        {
                            _cfg = new T();
                        }
                        NJson.SaveToFile(_cfg, FileName);
                    }
                    else
                    {
                        // Check file size.
                        long len = new FileInfo(FileName).Length;
                        if (len <= 0)
                        {
                            // File size is zero.
                            if (null == _cfg)
                            {
                                _cfg = new T();
                            }
                            NJson.SaveToFile(_cfg, FileName);
                        }
                        else
                        {
                            _cfg = NJson.LoadFromFile<T>(FileName);
                        }
                    }
                    // Raise event.
                    RaiseConfigChanged();
                }
                catch (Exception ex)
                {
                    med.Err(ex);
                }
            }
        }
        /// <summary>
        /// Save Config to file.
        /// </summary>
        public virtual void SaveConfig()
        {
            lock (this)
            {
                MethodBase med = MethodBase.GetCurrentMethod();
                try
                {
                    // save back to file.
                    if (null == _cfg)
                    {
                        _cfg = new T();
                    }
                    NJson.SaveToFile(_cfg, FileName);
                }
                catch (Exception ex)
                {
                    med.Err(ex);
                }
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets current config.
        /// </summary>
        public T Value { get { return _cfg; } set { } }

        #endregion

        #region Public Events

        /// <summary>
        /// The ConfigChanged Event Handler.
        /// </summary>
        public event EventHandler ConfigChanged;

        #endregion
    }

    #endregion

    #region TAConfigManager

    /// <summary>
    /// TA Config Manager class.
    /// </summary>
    public class TAConfigManager : ConfigManager<TAPlazaConfig>, IRabbitMQConfig, ISCWConfig, ITAxTODConfig
    {
        #region Static Instance Access

        private static TAConfigManager _instance = null;

        /// <summary>
        /// Gets ConfigManager instance access.
        /// </summary>
        public static TAConfigManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(TAConfigManager))
                    {
                        _instance = new TAConfigManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Internal Variables

        private string _fileName = NJson.LocalConfigFile("TA.app.config.json");

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private TAConfigManager() : base()
        {
            
        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~TAConfigManager()
        {
            //Shutdown();
        }

        #endregion

        #region Override Methods and Properties

        /// <summary>
        /// Gets Config File Name.
        /// </summary>
        public override string FileName { get { return _fileName; } }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets RabbitMQ Config.
        /// </summary>
        public RabbitMQServiceConfig RabbitMQ
        {
            get
            {
                if (null == Value) LoadConfig();
                return (null != Value) ? Value.RabbitMQ : null;
            }
        }
        /// <summary>
        /// Gets SCW Config.
        /// </summary>
        public SCWWebServiceConfig SCW
        {
            get
            {
                if (null == Value) LoadConfig();
                return (null != Value) ? Value.SCW : null;
            }
        }
        /// <summary>
        /// Gets TAxTOD Config.
        /// </summary>
        public TAxTODWebServiceConfig TAxTOD
        {
            get
            {
                if (null == Value) LoadConfig();
                return (null != Value) ? Value.TAxTOD : null;
            }
        }


        #endregion
    }

    #endregion

    #region TODConfigManager

    /// <summary>
    /// TOD Config Manager class.
    /// </summary>
    public class TODConfigManager : ConfigManager<TODPlazaConfig>, IRabbitMQConfig, ISCWConfig, ITAxTODConfig
    {
        #region Static Instance Access

        private static TODConfigManager _instance = null;

        /// <summary>
        /// Gets ConfigManager instance access.
        /// </summary>
        public static TODConfigManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(TODConfigManager))
                    {
                        _instance = new TODConfigManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Internal Variables

        private string _fileName = NJson.LocalConfigFile("TOD.app.config.json");

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private TODConfigManager() : base()
        {

        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~TODConfigManager()
        {
            //Shutdown();
        }

        #endregion

        #region Override Methods and Properties

        /// <summary>
        /// Gets Config File Name.
        /// </summary>
        public override string FileName { get { return _fileName; } }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets RabbitMQ Config.
        /// </summary>
        public RabbitMQServiceConfig RabbitMQ
        {
            get
            {
                if (null == Value) LoadConfig();
                return (null != Value) ? Value.RabbitMQ : null;
            }
        }
        /// <summary>
        /// Gets SCW Config.
        /// </summary>
        public SCWWebServiceConfig SCW
        {
            get
            {
                if (null == Value) LoadConfig();
                return (null != Value) ? Value.SCW : null;
            }
        }
        /// <summary>
        /// Gets TAxTOD Config.
        /// </summary>
        public TAxTODWebServiceConfig TAxTOD 
        {
            get 
            {
                if (null == Value) LoadConfig();
                return (null != Value) ? Value.TAxTOD : null; 
            }
        }

        #endregion
    }

    #endregion
}
