using MtnData.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MtnData.Models.DB_Connections
{
    public class TripReportConnect : DBConnect
    {
        public TripReportConnect() : base() { }

        public Message AddTripReport()
        {

        }

        /// <summary>
        /// Find all the trip reports for the location with the given id
        /// </summary>
        /// <param name="locId"></param>
        /// <returns>A message with a list of trip reoprts pertaining to a loation
        /// if any are available</returns>
        public Message FindLocationsTrips(int locId)
        {

        }

        /// <summary>
        /// Find the trip report with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Message FindTrip(int id)
        {

        }

        /// <summary>
        /// Remove a trip report with the specified id
        /// </summary>
        /// <param name="tripId"></param>
        /// <returns>A message wiuth result true if the delaton was successful</returns>
        public Message DeleteTripReport(int tripId)
        {

        }
    }
}