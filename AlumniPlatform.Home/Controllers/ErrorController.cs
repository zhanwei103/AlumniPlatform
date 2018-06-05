using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlumniPlatform.Home.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        public ActionResult ErrorDetail()
        {
            return View();
        }
        public ActionResult Error404()
        {
            return View();
        }
        public ActionResult Error500()
        {
            return View();
        }
    }
}
