using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MtnData.Controllers
{
    public class LocationController : Controller
    {
        // GET: Location
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewLocation()
        {
            return View();
        }

        public ActionResult NewLoc()
        {
            return View("Index");
        }
    }
}