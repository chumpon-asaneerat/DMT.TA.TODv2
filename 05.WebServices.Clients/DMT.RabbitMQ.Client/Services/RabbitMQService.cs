#region Using

using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Timers;
using System.Threading.Tasks;

using NLib;
using NLib.IO;

#endregion

namespace DMT.Services
{
    /// <summary>
    /// The Rabbit MQ Service class.
    /// </summary>
    public class RabbitMQService
    {
        #region Singelton

        private static RabbitMQService _instance = null;
        /// <summary>
        /// Singelton Access.
        /// </summary>
        public static RabbitMQService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(RabbitMQService))
                    {
                        _instance = new RabbitMQService();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Internal Variables

        private RabbitMQClient rabbitClient = null;
        private Timer _rabbit_timer = null;
        private bool _rabbit_scanning = false;

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private RabbitMQService() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~RabbitMQService()
        {
            Shutdown();
        }

        #endregion

        #region Rabbit related properties and methods

        /// <summary>
        /// Gets local message json folder path name.
        /// </summary>
        public string LocalRabbitMessageFolder
        {
            get
            {
                // Common Messages folder.
                string msgFolder = Folders.Combine(Folders.Assemblies.CurrentExecutingAssembly, "messages");
                // Rabbit MQ Root Folder
                string rootFolder = Folders.Combine(msgFolder, "rabbit.mq");
                // TA/TOR Sub Folder
                string localFolder = (string.IsNullOrEmpty(SubFolder)) ? 
                    rootFolder : Folders.Combine(rootFolder, SubFolder);
                if (!Folders.Exists(localFolder))
                {
                    Folders.Create(localFolder);
                }
                return localFolder;
            }
        }
        /// <summary>
        /// Gets or sets message sub folder (usually used app name).
        /// </summary>
        public string SubFolder { get; set; }

        private void WriteRabbitFile(string fullFileName, string message)
        {
            if (string.IsNullOrEmpty(message)) return;
            MethodBase med = MethodBase.GetCurrentMethod();
            // Save message.
            try
            {
                using (var stream = File.CreateText(fullFileName))
                {
                    stream.Write(message);
                    stream.Flush();
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        private void WriteRabbitFile(string message)
        {
            // Create file.
            string fileName = "msg." + DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss.ffffff",
                System.Globalization.DateTimeFormatInfo.InvariantInfo) + ".json";
            string fullFileName = Path.Combine(LocalRabbitMessageFolder, fileName);
            // Save message.
            WriteRabbitFile(fullFileName, message);
        }

        private void RabbitMoveToBackup(string file)
        {
            string parentDir = Path.GetDirectoryName(file);
            string fileName = Path.GetFileName(file);
            string targetPath = Path.Combine(parentDir, "Backup");
            if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
            if (!Directory.Exists(targetPath)) return;
            string targetFile = Path.Combine(targetPath, fileName);
            MethodBase med = MethodBase.GetCurrentMethod();
            try
            {
                if (File.Exists(targetFile)) File.Delete(targetFile);
                File.Move(file, targetFile);
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        private void RabbitMoveToError(string file)
        {
            string parentDir = Path.GetDirectoryName(file);
            string fileName = Path.GetFileName(file);
            string targetPath = Path.Combine(parentDir, "Error");
            if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
            if (!Directory.Exists(targetPath)) return;
            string targetFile = Path.Combine(targetPath, fileName);
            MethodBase med = MethodBase.GetCurrentMethod();
            try
            {
                if (File.Exists(targetFile)) File.Delete(targetFile);
                File.Move(file, targetFile);
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        private void RabbitMoveToInvalid(string file)
        {
            string parentDir = Path.GetDirectoryName(file);
            string fileName = Path.GetFileName(file);
            string targetPath = Path.Combine(parentDir, "Invalid");
            if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
            if (!Directory.Exists(targetPath)) return;
            string targetFile = Path.Combine(targetPath, fileName);
            MethodBase med = MethodBase.GetCurrentMethod();
            try
            {
                if (File.Exists(targetFile)) File.Delete(targetFile);
                File.Move(file, targetFile);
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        #endregion

        #region Timer Handler

        private void _rabbit_timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_rabbit_scanning) return;
            _rabbit_scanning = true;
            try
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                List<string> files = new List<string>();
                var msgFiles = Directory.GetFiles(LocalRabbitMessageFolder, "*.json");
                if (null != msgFiles && msgFiles.Length > 0) files.AddRange(msgFiles);
                files.ForEach(file =>
                {
                    try
                    {
                        string json = File.ReadAllText(file);
                        // Update to database
                        var msg = json.FromJson<Models.RabbitMQMessage>();
                        if (null != msg)
                        {
                            if (msg.parameterName == "STAFF")
                            {
                                var mq = json.FromJson<Models.RabbitMQStaffMessage>();
                                if (null != mq)
                                {
                                    var staffs = Models.RabbitMQStaff.ToLocals(mq.staff);
                                    if (null != staffs && staffs.Count > 0)
                                    {
                                        Task.Run(() =>
                                        {
                                            Models.User.SaveUsers(staffs);
                                        });
                                    }
                                    // process success backup file.
                                    RabbitMoveToBackup(file);
                                }
                                else
                                {
                                    med.Info("Cannot convert to STAFF message.");
                                    // process success error file.
                                    RabbitMoveToError(file);
                                }
                            }
                            else
                            {
                                med.Info("message is not STAFF message.");
                                // process not staff list so Invalid file.
                                RabbitMoveToInvalid(file);
                            }
                        }
                        else
                        {
                            med.Info("message is null or cannot convert to json object.");
                            // process success error file.
                            RabbitMoveToError(file);
                        }
                    }
                    catch (Exception ex)
                    {
                        med.Err(ex);
                    }
                });
            }
            catch (Exception) { }
            _rabbit_scanning = false;
        }

        #endregion

        #region RabbitClient Message Handler

        private void RabbitClient_OnMessageArrived(object sender, QueueMessageEventArgs e)
        {
            // Save message.
            WriteRabbitFile(e.Message);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Start service.
        /// </summary>
        /// <param name="MQConfig">The RabbitMQ config.</param>
        public void Start(RabbitMQServiceConfig MQConfig)
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            // Init Scanning Timer
            _rabbit_scanning = false;
            _rabbit_timer = new Timer();
            _rabbit_timer.Interval = 1000;
            _rabbit_timer.Elapsed += _rabbit_timer_Elapsed;
            _rabbit_timer.Start();
            // Init Rabbit Client
            if (null == rabbitClient)
            {
                if (null != MQConfig && MQConfig.Enabled)
                {
                    //WriteTAFile("init");
                    med.Info("Rabbit Host Info: " + MQConfig.GetString());
                    try
                    {
                        rabbitClient = new RabbitMQClient();
                        rabbitClient.HostName = MQConfig.HostName;
                        rabbitClient.PortNumber = MQConfig.PortNumber;
                        rabbitClient.VirtualHost = MQConfig.VirtualHost;
                        rabbitClient.UserName = MQConfig.UserName;
                        rabbitClient.Password = MQConfig.Password;
                        rabbitClient.OnMessageArrived += RabbitClient_OnMessageArrived;
                        if (rabbitClient.Connect() && rabbitClient.Listen(MQConfig.QueueName))
                        {
                            med.Info("Success connected to : " + MQConfig.GetString());
                            med.Info("Listen to Queue: " + MQConfig.QueueName);
                        }
                        else
                        {
                            med.Err("failed connected to : " + MQConfig.HostName);
                        }

                    }
                    catch (Exception ex)
                    {
                        med.Err(ex);
                    }
                }
            }
        }
        /// <summary>
        /// Shutdown service.
        /// </summary>
        public void Shutdown()
        {
            //MethodBase med = MethodBase.GetCurrentMethod();
            // Free Scanning Timer 
            try
            {
                if (null != _rabbit_timer)
                {
                    _rabbit_timer.Elapsed -= _rabbit_timer_Elapsed;
                    _rabbit_timer.Stop();
                    _rabbit_timer.Dispose();
                }
            }
            catch { }
            _rabbit_timer = null;
            _rabbit_scanning = false;

            // Free Rabbit Client
            try
            {
                if (null != rabbitClient)
                {
                    rabbitClient.OnMessageArrived -= RabbitClient_OnMessageArrived;
                    rabbitClient.Disconnect();

                }
            }
            catch { }
            rabbitClient = null;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Checks is Rabbit MQ Connected.
        /// </summary>
        public bool Connected 
        {
            get { return (null != rabbitClient && rabbitClient.Connected); }
        }

        #endregion
    }
}
