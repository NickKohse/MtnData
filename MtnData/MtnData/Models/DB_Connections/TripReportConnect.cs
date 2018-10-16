using MtnData.Models.Messages;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace MtnData.Models.DB_Connections
{
    public class TripReportConnect : DBConnect
    {
        public TripReportConnect() : base() { }

        public Message AddTripReport(TripReport t)
        {
            string sqlString = @"INSERT INTO TripReport (UserId, DestId, Date, Time, Result, Description) VALUES (@uid, @did, date, @time, @result, @description)";
            SQLiteCommand addTRSQL = new SQLiteCommand(sqlString, conn);
            addTRSQL.Parameters.Add(new SQLiteParameter("@uid", t.UserId));
            addTRSQL.Parameters.Add(new SQLiteParameter("@did", t.DestinatinId));
            addTRSQL.Parameters.Add(new SQLiteParameter("@date", t.Date));
            addTRSQL.Parameters.Add(new SQLiteParameter("@time", t.Time));
            addTRSQL.Parameters.Add(new SQLiteParameter("@result", t.Result));
            addTRSQL.Parameters.Add(new SQLiteParameter("@description", t.Description));
            return ExecuteUpdate(addTRSQL, "AddTripReport");
        }

        /// <summary>
        /// Find all the trip reports for the location with the given id
        /// </summary>
        /// <param name="locId"></param>
        /// <returns>A message with a dictionary of trip reports(id, date) pertaining to a loation
        /// if any are available</returns>
        public Message FindLocationsTrips(int locId)
        {
            string sqlString = "SELECT Id, Date FROM TripReport WHERE DestId=@id";
            SQLiteCommand findTRSQL = new SQLiteCommand(sqlString, conn);
            findTRSQL.Parameters.Add(new SQLiteParameter("@id", locId));
            conn.Open();

            SQLiteDataReader reader = findTRSQL.ExecuteReader();
            if (reader.HasRows)
            {
                Dictionary<long, long> trips = new Dictionary<long, long>();
                object[] oarr;
                while (reader.Read()) {
                    oarr = new object[2];
                    try
                    {
                        reader.GetValues(oarr);
                        trips.Add((long)oarr[0], (long)oarr[1]);
                    }
                    catch (Exception ex)
                    {
                        Utilities.ExceptionLogger("Exception in FindTrip function: " + ex.Message);
                        return new Message(false, "Error while reading from trip report database");
                    }
                }
                return new Message(true, "found report(s)", trips);
            }
            else
            {
                return new Message(false, "No trips have been reported for this location");
            }
        }

        /// <summary>
        /// Find the trip report with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Message FindTrip(int id)
        {
            string sqlString = "SELECT * FROM TripReport WHERE Id=@id";
            SQLiteCommand findTRSQL = new SQLiteCommand(sqlString, conn);
            findTRSQL.Parameters.Add(new SQLiteParameter("@id", id));
            conn.Open();

            SQLiteDataReader reader = findTRSQL.ExecuteReader();

            if (reader.HasRows)
            {
                object[] oarr = new object[7];
                try
                {
                    reader.GetValues(oarr);
                    return new Message(true, "found it", new TripReport((long)oarr[0], (long)oarr[1], (long)oarr[2], (long)oarr[3], (long)oarr[4], (bool)oarr[5], (string)oarr[6]));
                }
                catch(Exception ex)
                {
                    Utilities.ExceptionLogger("Exception in FindTrip function: " + ex.Message);
                    return new Message(false, "Error while reading from trip report database");
                }
            }
            else
            {
                return new Message(false, "Something has gone wrong, that trip report doesn't exist");
            }
        }

        /// <summary>
        /// Remove a trip report with the specified id
        /// </summary>
        /// <param name="tripId"></param>
        /// <returns>A message wiuth result true if the delaton was successful</returns>
        public Message DeleteTripReport(int tripId)
        {
            string sqlString = @"DELETE FROM TripReport WHERE Id=@id";
            SQLiteCommand deleteTRSQL = new SQLiteCommand(sqlString, conn);
            deleteTRSQL.Parameters.Add(new SQLiteParameter("@id", tripId));
            return ExecuteUpdate(deleteTRSQL, "DeleteTripReport");
        }
    }
}