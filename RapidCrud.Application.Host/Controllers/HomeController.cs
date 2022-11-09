using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RapidCrud.Application.Host.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public string Test()
        {
            return "Test";
        }

        public string TestLocalMerge()
        {
            return "Test Local merge";
        }
    }
}
