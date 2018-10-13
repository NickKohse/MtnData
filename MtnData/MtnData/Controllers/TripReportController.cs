using MtnData.Models.DB_Connections;
using MtnData.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MtnData.Controllers
{
    public class TripReportController : Controller
    {
        // GET: TripReport
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowTripReport(int id)
        {
            TripReportConnect tc = new TripReportConnect();
            Message response = tc.FindTrip(id);

            if (response.GetResult())
            {
                //send it 
            }
            else
            {
                //do something else
            }
            
        }
    }
}