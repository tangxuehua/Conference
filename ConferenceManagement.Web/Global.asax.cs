using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Conference.Common;
using ConferenceManagement.Web.Extensions;
using ECommon.Autofac;
using ECommon.Components;
using ECommon.Configurations;
using ECommon.JsonNet;
using ECommon.Log4Net;
using ECommon.Logging;
using ENode.Configurations;

namespace ConferenceManagement.Web
{
    public class MvcApplication : HttpApplication
    {
        private ILogger _logger;
        private Configuration _ecommonConfiguration;
        private ENodeConfiguration _enodeConfiguration;

        protected void Application_Start()
        {
             AreaRegistration.RegisterAllAreas();
             GlobalFilters.Filters.Add(new HandleErrorAttribute());
             RegisterRoutes(RouteTable.Routes);
             InitializeECommon();
             InitializeENode();
        }

        private void InitializeECommon()
        {
            _ecommonConfiguration = Configuration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .UseLog4Net()
                .UseJsonNet()
                .RegisterUnhandledExceptionHandler();

            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(GetType().FullName);
            _logger.Info("ECommon initialized.");
        }
        private void InitializeENode()
        {
            ConfigSettings.Initialize();

            var assemblies = new[]
            {
                Assembly.Load("ConferenceManagement.ReadModel"),
                Assembly.Load("ConferenceManagement.Web")
            };

            _enodeConfiguration = _ecommonConfiguration
                .CreateENode()
                .RegisterENodeComponents()
                .RegisterBusinessComponents(assemblies)
                .RegisterAllTypeCodes()
                .UseEQueue()
                .InitializeBusinessAssemblies(assemblies)
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
        private void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Conference.Locate",
                url: "locate",
                defaults: new { controller = "Conference", action = "Locate" }
            );

            routes.MapRoute(
                name: "Conference.Create",
                url: "create",
                defaults: new { controller = "Conference", action = "Create" }
            );

            routes.MapRoute(
                name: "Conference",
                url: "{slug}/{accessCode}/{action}",
                defaults: new { controller = "Conference", action = "Index" }
            );

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
