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
            float dist = 0;
            int physical = 0;
            int technical = 0;
            int final = 0;
            //bool ver = false; **currently recieves this as null, possibly due to the diabled attribute in the html
            Coordinate start = null;
            Coordinate end = null;
            try
            {
                ev_gain = int.Parse(evGain);
                dist = float.Parse(distance);
                physical = int.Parse(pDiff);
                technical = int.Parse(TDiff);
                final = int.Parse(peakEv);
                //ver = bool.Parse(verified);
                start = new Coordinate(double.Parse(startN), double.Parse(startW));
                end = new Coordinate(double.Parse(endN), double.Parse(endW));

            }
            catch(Exception ex)
            {
                Utilities.ExceptionLogger("Unparseable input in NewLoc function. Exception message: " + ex.Message);
            }
            Location newLoc = new Location(name, region, ev_gain, dist, start, end, physical, technical, final, 0 /*for now a new location is always unverified*/, description);
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
                ViewBag.createdlocationResponse = response.GetText();
                return View("NewLocation");
            }
        }

        public ActionResult SearchLoc(string keyword)
        {
            LocationConnect lc = new LocationConnect();
            Message response = lc.SearchLocation(keyword);

            if (response.GetResult())
            {
                //somehow return the payload of the message - List<Location> - to the page as clickable links
                ViewBag.SearchResponse = "Found something, but showing it isn't implemented yet.\n";
                return View("Index");
            }
            else
            {
                //return something that shows there are no responses
                ViewBag.SearchResponse = response.GetText() + "\n";
                return View("Index");
            }
        }

        public ActionResult ShowLocation(string name)
        {
            LocationConnect lc = new LocationConnect();
            Message response = lc.SearchLocation(name);

            if (!response.GetResult())
            {
                ViewBag.searchLocationMessage = "We have no record of a location by that name";
            }
            else
            {
                List<Location> locList = (List<Location>)response.GetPayload();
                if(locList.Count > 1) //This should realisiticly never happen
                {
                    ViewBag.searchLocationMessage = "Something has gone worng.";
                    Utilities.EventLogger("Found more than one location with the same name trying to show a location.", Globals.LOG_LEVELS.Critical);
                }
                ViewBag.searchLocationResult = locList.ElementAt(0);
            }
            return View();
            
        }
    }
}