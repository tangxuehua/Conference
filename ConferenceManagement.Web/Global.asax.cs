namespace Conference.Web.Admin
{
    using System.Data.Entity;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Conference.Common;

    public class MvcApplication : HttpApplication
    {
        //public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        //{
        //    filters.Add(new HandleErrorAttribute());
        //}

        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Conference.Locate",
            //    url: "locate",
            //    defaults: new { controller = "Conference", action = "Locate" }
            //);

            //routes.MapRoute(
            //    name: "Conference.Create",
            //    url: "create",
            //    defaults: new { controller = "Conference", action = "Create" }
            //);

            //routes.MapRoute(
            //    name: "Conference",
            //    url: "{slug}/{accessCode}/{action}",
            //    defaults: new { controller = "Conference", action = "Index" }
            //);

            //routes.MapRoute(
            //    name: "Home",
            //    url: "",
            //    defaults: new { controller = "Home", action = "Index" }
            //);

        }

        protected void Application_Start()
        {
//            DatabaseSetup.Initialize();

//            AreaRegistration.RegisterAllAreas();

//            RegisterGlobalFilters(GlobalFilters.Filters);
//            RegisterRoutes(RouteTable.Routes);

//            var serializer = new JsonTextSerializer();
////#if LOCAL
//            EventBus = new EventBus(new MessageSender(Database.DefaultConnectionFactory, "SqlBus", "SqlBus.Events"), serializer);
////#else
//            var settings = InfrastructureSettings.Read(HttpContext.Current.Server.MapPath(@"~\bin\Settings.xml")).ServiceBus;

//            if (!MaintenanceMode.IsInMaintainanceMode)
//            {
//            new ServiceBusConfig(settings).Initialize();
//            }

//            EventBus = new EventBus(new TopicSender(settings, "conference/events"), new StandardMetadataProvider(), serializer);
//#endif

//#if AZURESDK
//            if (Microsoft.WindowsAzure.ServiceRuntime.RoleEnvironment.IsAvailable)
//            {
//                System.Diagnostics.Trace.Listeners.Add(new Microsoft.WindowsAzure.Diagnostics.DiagnosticMonitorTraceListener());
//                System.Diagnostics.Trace.AutoFlush = true;
//            }
//#endif
        }
    }
}
