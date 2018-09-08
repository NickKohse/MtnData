using MtnData.Models;
using MtnData.Models.DB_Connections;
using MtnData.Models.Messages;
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

        public ActionResult NewLoc(string name, string region, string evGain, string distance, string startN, string startW, string endN, string endW, string pDiff, string TDiff, string peakEv, string verified, string description)
        {
            int ev_gain = 0;
            int dist = 0;
            int physical = 0;
            int technical = 0;
            int final = 0;
            bool ver = false;
            Coordinate start = null;
            Coordinate end = null;
            try
            {
                ev_gain = int.Parse(evGain);
                dist = int.Parse(distance);
                physical = int.Parse(pDiff);
                technical = int.Parse(TDiff);
                final = int.Parse(peakEv);
                ver = bool.Parse(verified);
                start = new Coordinate(double.Parse(startN), double.Parse(startW));
                end = new Coordinate(double.Parse(endN), double.Parse(endW));

            }
            catch(Exception ex)
            {
                Utilities.ExceptionLogger("Unparseable input in NewLoc function. Exception message" + ex.Message);
            }
            Location newLoc = new Location(name, region, ev_gain, dist, start, end, physical, technical, final, ver, description);
            LocationConnect lc = new LocationConnect();
            Message response = lc.AddLocation(newLoc);

            if (response.GetResult())
            {
                Utilities.EventLogger("Created a new location named: " + name, Globals.LOG_LEVELS.Info);
                return View("Created");
            }
            else
            {
                Utilities.EventLogger("Couldn't create a new location named: " + name + " Resulting message: " + response.GetText(), Globals.LOG_LEVELS.Error);
                return View("NewLocation");
            }
        }
    }
}