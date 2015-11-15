using System;
using System.Configuration;
using System.Net;
using Conference.Common;
using ECommon.Autofac;
using ECommon.Components;
using ECommon.JsonNet;
using ECommon.Log4Net;
using ECommon.Logging;
using ECommon.Socketing;
using EQueue.Broker;
using EQueue.Configurations;
using ECommonConfiguration = ECommon.Configurations.Configuration;

namespace Conference.MessageBroker
{
    public class Bootstrap
    {
        private static ILogger _logger;
        private static ECommonConfiguration _configuration;
        private static BrokerController _broker;

        public static void Initialize()
        {
            ConfigSettings.Initialize();
            InitializeECommon();
            try
            {
                InitializeEQueue();
            }
            catch (Exception ex)
            {
                _logger.Error("Initialize EQueue failed.", ex);
                throw;
            }
        }
        public static void Start()
        {
            try
            {
                _broker.Start();
            }
            catch (Exception ex)
            {
                _logger.Error("Broker start failed.", ex);
                throw;
            }
        }
        public static void Stop()
        {
            try
            {
                if (_broker != null)
                {
                    _broker.Shutdown();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Broker stop failed.", ex);
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
            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(Bootstrap).FullName);
            _logger.Info("ECommon initialized.");
        }
        private static void InitializeEQueue()
        {
            _configuration.RegisterEQueueComponents();
            var storePath = ConfigurationManager.AppSettings["equeueStorePath"];
            var setting = new BrokerSetting(storePath)
            {
                ProducerAddress = new IPEndPoint(SocketUtils.GetLocalIPV4(), ConfigSettings.BrokerProducerPort),
                ConsumerAddress = new IPEndPoint(SocketUtils.GetLocalIPV4(), ConfigSettings.BrokerConsumerPort),
                AdminAddress = new IPEndPoint(SocketUtils.GetLocalIPV4(), ConfigSettings.BrokerAdminPort)
            };
            _broker = BrokerController.Create(setting);
            _logger.Info("EQueue initialized.");
        }
    }
}
