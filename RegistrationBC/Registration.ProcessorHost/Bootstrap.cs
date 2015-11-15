using System;
using System.Reflection;
using Conference.Common;
using ECommon.Autofac;
using ECommon.Components;
using ECommon.JsonNet;
using ECommon.Log4Net;
using ECommon.Logging;
using ENode.Configurations;
using ECommonConfiguration = ECommon.Configurations.Configuration;

namespace Registration.ProcessorHost
{
    public class Bootstrap
    {
        private static ILogger _logger;
        private static ECommonConfiguration _ecommonConfiguration;
        private static ENodeConfiguration _enodeConfiguration;

        public static void Initialize()
        {
            InitializeECommon();
            try
            {
                InitializeENode();
            }
            catch (Exception ex)
            {
                _logger.Error("Initialize ENode failed.", ex);
                throw;
            }
        }
        public static void Start()
        {
            try
            {
                _enodeConfiguration.StartEQueue();
            }
            catch (Exception ex)
            {
                _logger.Error("EQueue start failed.", ex);
                throw;
            }
        }
        public static void Stop()
        {
            try
            {
                _enodeConfiguration.ShutdownEQueue();
            }
            catch (Exception ex)
            {
                _logger.Error("EQueue stop failed.", ex);
                throw;
            }
        }

        private static void InitializeECommon()
        {
            _ecommonConfiguration = ECommonConfiguration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .UseLog4Net()
                .UseJsonNet()
                .RegisterUnhandledExceptionHandler();
            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(Bootstrap).FullName);
            _logger.Info("ECommon initialized.");
        }
        private static void InitializeENode()
        {
            ConfigSettings.Initialize();

            var assemblies = new[]
            {
                Assembly.Load("Conference.Common"),
                Assembly.Load("Registration.Domain"),
                Assembly.Load("Registration.CommandHandlers"),
                Assembly.Load("Registration.ProcessManagers"),
                Assembly.Load("Registration.ReadModel"),
                Assembly.Load("Registration.ProcessorHost")
            };
            var setting = new ConfigurationSetting
            {
                SqlDefaultConnectionString = ConfigSettings.ConferenceENodeConnectionString
            };

            _enodeConfiguration = _ecommonConfiguration
                .CreateENode(setting)
                .RegisterENodeComponents()
                .RegisterBusinessComponents(assemblies)
                .RegisterAllTypeCodes()
                .UseSqlServerLockService()
                .UseSqlServerCommandStore()
                .UseSqlServerEventStore()
                .UseSqlServerSequenceMessagePublishedVersionStore()
                .UseSqlServerMessageHandleRecordStore()
                .UseEQueue()
                .InitializeBusinessAssemblies(assemblies);
            _logger.Info("ENode initialized.");
        }
    }
}
