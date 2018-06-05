using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlumniPlatform.Home.Models
{
    public class MyExceptionFilterAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            //filterContext.HttpContext.Response.StatusCode
            string controllerName = (string)filterContext.RouteData.Values["controller"];
            string actionName = (string)filterContext.RouteData.Values["action"];
            HandleErrorInfo info = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
            HttpRequestBase request = filterContext.RequestContext.HttpContext.Request;
            
            //获取浏览器信息
            string broser = request.Browser.Browser;
            string broserVersion = request.Browser.Version;
            //获取客户端系统信息
            string system = request.Browser.Platform;


            Exception exception = filterContext.Exception;
            if (!filterContext.ExceptionHandled)
            {
                //当customErrors=Off时
                //当customErrors = RemoteOnly，且在本地调试时
                filterContext.Result = new ViewResult() { ViewName = "/Views/Error/ErrorDetail.cshtml", ViewData = new ViewDataDictionary<HandleErrorInfo>(info) };
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

            }
            else
            {
                HttpException httpException = new HttpException(null, exception);
                string message = filterContext.Exception.Message;//可获取错误信息

                /*
                 * 1、根据对应的HTTP错误码跳转到错误页面
                 * 2、这里对HTTP 404/400错误进行捕捉和处理
                 * 3、其他错误默认为HTTP 500服务器错误
                 */
                int httpCode = httpException.GetHttpCode();
                if (httpException != null && httpCode == 500)
                {
                    filterContext.Result = new ViewResult() { ViewName = "/Views/Error/Error500.cshtml", ViewData = new ViewDataDictionary<HandleErrorInfo>(info) };

                }
                else
                {
                    filterContext.Result = new ViewResult() { ViewName = "/Views/Error/Error500.cshtml", ViewData = new ViewDataDictionary<HandleErrorInfo>(info) };
                }
                /*---------------------------------------------------------
                 * 这里可进行相关自定义业务处理，比如日志记录等
                 ---------------------------------------------------------*/

                //filterContext.Result = new ViewResult() { ViewName = "/Views/Shared/ErrorDetails.cshtml", ViewData = new ViewDataDictionary<HandleErrorInfo>(info) };

                //设置异常已经处理,否则会被其他异常过滤器覆盖
                filterContext.ExceptionHandled = true;

                //在派生类中重写时，获取或设置一个值，该值指定是否禁用IIS自定义错误。
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            }
        }
    }
}