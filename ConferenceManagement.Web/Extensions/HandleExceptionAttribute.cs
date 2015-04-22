using System;
using System.Net;
using System.Web.Mvc;
using ECommon.Components;
using ECommon.Logging;

namespace ConferenceManagement.Web.Extensions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class HandleExceptionAttribute : HandleErrorAttribute
    {
        private static ILogger _logger;

        public override void OnException(ExceptionContext filterContext)
        {
            TryLogException(filterContext.Exception);
            var errorMessage = GetErrorMessage(filterContext);

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        success = false,
                        errorMsg = errorMessage
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                filterContext.ExceptionHandled = true;
            }
            else
            {
                if (filterContext.Exception != null && filterContext.Exception is TimeoutException)
                {
                    View = "TimeoutError";
                }
                base.OnException(filterContext);
            }
        }

        private static void TryLogException(Exception ex)
        {
            if (_logger == null)
            {
                try
                {
                    _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(HandleExceptionAttribute).FullName);
                }
                catch { }
            }
            if (_logger == null) return;

            _logger.Error(ex);
        }
        private static string GetErrorMessage(ExceptionContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                if (filterContext.Exception is TimeoutException)
                {
                    return "Sorry, your request process is timeout, please wait for a while to check the process result.";
                }
                return filterContext.Exception.Message;
            }
            return "Sorry, an error occurred while processing your request.";
        }
    }
}