using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Conference.Common;
using ECommon.Autofac;
using ECommon.Components;
using ECommon.JsonNet;
using ECommon.Log4Net;
using ECommon.Logging;
using ENode.Configurations;
using ECommonConfiguration = ECommon.Configurations.Configuration;

namespace Conference.Web.Public
{
    public partial class MvcApplication : HttpApplication
    {
        private ILogger _logger;
        private ECommonConfiguration _ecommonConfiguration;
        private ENodeConfiguration _enodeConfiguration;

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        protected void Application_Start()
        {
            ConfigSettings.Initialize();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            AreaRegistration.RegisterAllAreas();
            AppRoutes.RegisterRoutes(RouteTable.Routes);

            InitializeECommon();
            InitializeENode();
        }

        private void InitializeECommon()
        {
            _ecommonConfiguration = ECommonConfiguration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .UseLog4Net()
                .UseJsonNet();

            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(GetType().FullName);
            _logger.Info("ECommon initialized.");
        }
        private void InitializeENode()
        {
            var assemblies = new[]
            {
                Assembly.Load("Registration"),
                Assembly.Load("Registration.CommandHandlers"),
                Assembly.Load("Registration.EventHandlers"),
                Assembly.Load("Registration.ReadModel"),

                Assembly.Load("Payments"),
                Assembly.Load("Payments.CommandHandlers"),
                Assembly.Load("Payments.EventHandlers"),
                Assembly.Load("Payments.ReadModel"),

                Assembly.Load("Conference.Web.Public")
            };

            var setting = new ConfigurationSetting { SqlServerDefaultConnectionString = ConfigSettings.ConnectionString };

            _enodeConfiguration = _ecommonConfiguration
                .CreateENode(setting)
                .RegisterENodeComponents()
                .RegisterBusinessComponents(assemblies)
                .UseSqlServerCommandStore()
                .UseSqlServerEventStore()
                .UseEQueue()
                .InitializeBusinessAssemblies(assemblies)
                .StartENode(NodeType.CommandProcessor | NodeType.EventProcessor | NodeType.ExceptionProcessor)
                .StartEQueue();

            RegisterControllers();
            _logger.Info("ENode initialized.");
        }
        private void RegisterControllers()
        {
            var webAssembly = Assembly.GetExecutingAssembly();
            var container = (ObjectContainer.Current as AutofacObjectContainer).Container;
            var builder = new ContainerBuilder();
            builder.RegisterControllers(webAssembly);
            builder.Update(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
