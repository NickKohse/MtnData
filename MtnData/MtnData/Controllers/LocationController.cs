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

        public ActionResult NewLoc(string name, string region, string evGain, string distance, string start, string end, string pDiff, string TDiff, string peakEv, string verified, string description)
        {
            return View("Index");
        }
    }
}