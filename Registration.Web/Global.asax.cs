using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Conference.Common;
using ECommon.Autofac;
using ECommon.Components;
using ECommon.Configurations;
using ECommon.JsonNet;
using ECommon.Log4Net;
using ECommon.Logging;
using ENode.Configurations;
using Registration.Web.Extensions;

namespace Registration.Web
{
    public partial class MvcApplication : HttpApplication
    {
        private ILogger _logger;
        private Configuration _ecommonConfiguration;
        private ENodeConfiguration _enodeConfiguration;

        protected void Application_Start()
        {
            ConfigSettings.Initialize();

            AreaRegistration.RegisterAllAreas();
            GlobalFilters.Filters.Add(new HandleExceptionAttribute());
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
                Assembly.Load("Registration.ReadModel"),
                Assembly.Load("Payments.ReadModel"),
                Assembly.Load("Registration.Web")
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
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute(
                "Home",
                string.Empty,
                new { controller = "Default", action = "Index" });

            // Registration routes

            routes.MapRoute(
                "ViewConference",
                "{conferenceCode}/",
                new { controller = "Conference", action = "Display" });

            routes.MapRoute(
                "RegisterStart",
                "{conferenceCode}/register",
                new { controller = "Registration", action = "StartRegistration" });

            routes.MapRoute(
                "RegisterRegistrantDetails",
                "{conferenceCode}/registrant",
                new { controller = "Registration", action = "SpecifyRegistrantAndPaymentDetails" });

            routes.MapRoute(
                "StartPayment",
                "{conferenceCode}/pay",
                new { controller = "Registration", action = "StartPayment" });

            routes.MapRoute(
                "ExpiredOrder",
                "{conferenceCode}/expired",
                new { controller = "Registration", action = "ShowExpiredOrder" });

            routes.MapRoute(
                "RegisterConfirmation",
                "{conferenceCode}/confirmation",
                new { controller = "Registration", action = "ThankYou" });

            routes.MapRoute(
                "OrderFind",
                "{conferenceCode}/order/find",
                new { controller = "Order", action = "Find" });

            routes.MapRoute(
                "AssignSeats",
                "{conferenceCode}/order/{orderId}/seats",
                new { controller = "Order", action = "AssignSeats" });

            routes.MapRoute(
                "AssignSeatsWithoutAssignmentsId",
                "{conferenceCode}/order/{orderId}/seats-redirect",
                new { controller = "Order", action = "AssignSeatsForOrder" });

            routes.MapRoute(
                "OrderDisplay",
                "{conferenceCode}/order/{orderId}",
                new { controller = "Order", action = "Display" });

            routes.MapRoute(
                "InitiateThirdPartyPayment",
                "{conferenceCode}/third-party-payment",
                new { controller = "Payment", action = "ThirdPartyProcessorPayment" });

            routes.MapRoute(
                "PaymentAccept",
                "{conferenceCode}/third-party-payment-accept",
                new { controller = "Payment", action = "ThirdPartyProcessorPaymentAccepted" });

            routes.MapRoute(
                "PaymentReject",
                "{conferenceCode}/third-party-payment-reject",
                new { controller = "Payment", action = "ThirdPartyProcessorPaymentRejected" });
        }
    }
}
