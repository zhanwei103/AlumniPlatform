using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlumniPlatform.Home.Models
{
    public class MyAuthorFilterAttribute : AuthorizeAttribute
    {
        public string Name { set; get; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["AuthorToken"] == null)
            {
                HttpContext.Current.Response.Write("<script>top.location.href = '/Account/Login';</script>");
                //HttpContext.Current.Response.Redirect("/Account/Login");
                filterContext.Result = new EmptyResult();
            }
            
        }
    }
}