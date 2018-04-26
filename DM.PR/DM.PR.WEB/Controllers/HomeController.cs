using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DM.PR.WEB.Controllers
{
    public class HomeController : Controller
    {   

        public ActionResult Index()
        {
            ViewBag.message = "Hello world!";

            return View();
        }
    }
}