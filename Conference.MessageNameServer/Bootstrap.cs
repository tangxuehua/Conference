using System;
using System.Configuration;
using System.Net;
using Conference.Common;
using ECommon.Components;
using ECommon.Configurations;
using ECommon.Socketing;
using ECommon.Logging;
using EQueue.NameServer;
using EQueue.Configurations;
using ECommonConfiguration = ECommon.Configurations.Configuration;

namespace Conference.MessageNameServer
{
    public class Bootstrap
    {
        private static ILogger _logger;
        private static ECommonConfiguration _configuration;
        private static NameServerController _nameServer;

        public static void Initialize()
        {
            ConfigSettings.Initialize();
            InitializeECommon();
            InitializeNameServer();
        }

        public static void Start()
        {
            try
            {
                _nameServer.Start();
            }
            catch (Exception ex)
            {
                _logger.Error("NameServer start failed.", ex);
                throw;
            }
        }
        public static void Stop()
        {
            try
            {
                if (_nameServer != null)
                {
                    _nameServer.Shutdown();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("NameServer stop failed.", ex);
                throw;
            }
        }

        private static void InitializeECommon()
        {
            _configuration = ECommonConfiguration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .UseLog4Net()
                .UseJsonNet()
                .RegisterUnhandledExceptionHandler();
        }

        private static void InitializeNameServer()
        {
            _configuration.RegisterEQueueComponents().BuildContainer();
            var setting = new NameServerSetting()
            {
                BindingAddress = new IPEndPoint(SocketUtils.GetLocalIPV4(), ConfigSettings.NameServerPort)
            };
            _nameServer = new NameServerController(setting);
            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(Bootstrap).FullName);
            _logger.Info("NameServer initialized.");
        }
    }
}
