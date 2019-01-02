using System;
using System.Reflection;
using Conference.Common;
using ECommon.Components;
using ECommon.Configurations;
using ECommon.Logging;
using ENode.Configurations;
using ENode.SqlServer;
using ECommonConfiguration = ECommon.Configurations.Configuration;

namespace Payments.ProcessorHost
{
    public class Bootstrap
    {
        private static ILogger _logger;
        private static ECommonConfiguration _ecommonConfiguration;
        private static ENodeConfiguration _enodeConfiguration;

        public static void Initialize()
        {
            InitializeECommon();
            InitializeENode();
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
        }
        private static void InitializeENode()
        {
            ConfigSettings.Initialize();

            var assemblies = new[]
            {
                Assembly.Load("Conference.Common"),
                Assembly.Load("Payments.Commands"),
                Assembly.Load("Payments.Domain"),
                Assembly.Load("Payments.Messages"),
                Assembly.Load("Payments.CommandHandlers"),
                Assembly.Load("Payments.MessagePublishers"),
                Assembly.Load("Payments.ReadModel"),
                Assembly.GetExecutingAssembly()
            };

            var connectionString = ConfigSettings.ConferenceENodeConnectionString;

            _enodeConfiguration = _ecommonConfiguration
                .CreateENode()
                .RegisterENodeComponents()
                .RegisterBusinessComponents(assemblies)
                .UseSqlServerLockService()
                .UseSqlServerEventStore()
                .UseSqlServerPublishedVersionStore()
                .UseEQueue()
                .BuildContainer()
                .InitializeSqlServerEventStore(connectionString)
                .InitializeSqlServerPublishedVersionStore(connectionString)
                .InitializeSqlServerLockService(connectionString)
                .InitializeBusinessAssemblies(assemblies);

            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(Bootstrap).FullName);
            _logger.Info("ENode initialized.");
        }
    }
}
