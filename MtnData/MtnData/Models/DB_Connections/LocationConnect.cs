using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using MtnData.Models.Messages;
using static MtnData.Models.Globals;

namespace MtnData.Models.DB_Connections
{
    public class LocationConnect : DBConnect
    {
        public LocationConnect() : base() {}

        /// <summary>
        /// Add a new location to the database. This funcation doesn't check to see if any values are unique because no values in this tbale have to be 
        /// unique except for the id which is auto generated.
        /// </summary>
        /// <param name="toAdd">A location to be added</param>
        /// <returns>A message show the result of the attempted insertion</returns>
        public Message AddLocation(Location toAdd)
        {
            string sqlString = @"INSERT INTO Destination (Name, Region, EvGain, Distance, Coords, EndCoords, PDiff, TDiff, PeakEv, Verified, Description) VALUES (@name,@region,@evgain,@distance,@coords,@endcoords,@pdiff,@tdiff,@peakev,@verified,@description)";
            SQLiteCommand addLocSQL = new SQLiteCommand(sqlString, conn);
            addLocSQL.Parameters.Add(new SQLiteParameter("@name", toAdd.Name));
            addLocSQL.Parameters.Add(new SQLiteParameter("@region", toAdd.Region));
            addLocSQL.Parameters.Add(new SQLiteParameter("@evgain", toAdd.EvGain));
            addLocSQL.Parameters.Add(new SQLiteParameter("@distance", Math.Round(toAdd.Distance, 2)));
            addLocSQL.Parameters.Add(new SQLiteParameter("@coords", toAdd.Start)); //why does this work?
            addLocSQL.Parameters.Add(new SQLiteParameter("@endcoords", toAdd.End));
            addLocSQL.Parameters.Add(new SQLiteParameter("@pdiff", toAdd.PDiff));
            addLocSQL.Parameters.Add(new SQLiteParameter("@tdiff", toAdd.TDiff));
            addLocSQL.Parameters.Add(new SQLiteParameter("@peakev", toAdd.FinalEv));
            addLocSQL.Parameters.Add(new SQLiteParameter("@verified", toAdd.Verified));
            addLocSQL.Parameters.Add(new SQLiteParameter("@description", toAdd.Description));
            return ExecuteUpdate(addLocSQL, "AddLocation");

        }

        /// <summary>
        /// Modify an existing location 
        /// </summary>
        /// <param name="id">The id in the table of the location to be modified</param>
        /// <param name="loc">The infomation that will replace what is currently stored at the given id in the table</param>
        /// <returns>A status message</returns>
        public Message ModifyLocation(int id, Location toUpdate)
        {
            string sqlString = @"UPDATE Destination SET Name=@name , Region=@region , EvGain=@evgain , Distance=@distance , Coords=@coords , EndCoords=@endcoords , PDiff=@pdiff , TDiff=@tdiff , PeakEv=@peakev 
                                  , Verified=@verified , Description=@description WHERE Id=@id";
            SQLiteCommand updateLocSQL = new SQLiteCommand(sqlString, conn);
            updateLocSQL.Parameters.Add(new SQLiteParameter("@name", toUpdate.Name));
            updateLocSQL.Parameters.Add(new SQLiteParameter("@region", toUpdate.Region));
            updateLocSQL.Parameters.Add(new SQLiteParameter("@evgain", toUpdate.EvGain));
            updateLocSQL.Parameters.Add(new SQLiteParameter("@distance", toUpdate.Distance));
            updateLocSQL.Parameters.Add(new SQLiteParameter("@coords", toUpdate.Start));
            updateLocSQL.Parameters.Add(new SQLiteParameter("@endcoords", toUpdate.End));
            updateLocSQL.Parameters.Add(new SQLiteParameter("@pdiff", toUpdate.PDiff));
            updateLocSQL.Parameters.Add(new SQLiteParameter("@tdiff", toUpdate.TDiff));
            updateLocSQL.Parameters.Add(new SQLiteParameter("@peakev", toUpdate.FinalEv));
            updateLocSQL.Parameters.Add(new SQLiteParameter("@verified", toUpdate.Verified));
            updateLocSQL.Parameters.Add(new SQLiteParameter("@description", toUpdate.Description));
            updateLocSQL.Parameters.Add(new SQLiteParameter("@id", id));
            return ExecuteUpdate(updateLocSQL, "ModifyLocation");
  
        }

        /// <summary>
        /// Deletes a location from the database
        /// </summary>
        /// <param name="id">the id of the location to be deleted</param>
        /// <returns>a status message</returns>
        public Message RemoveLocation(int id)
        {
            string sqlString = @"DELETE FROM Destination WHERE Id=@id";
            SQLiteCommand deleteLocSQL = new SQLiteCommand(sqlString, conn);
            deleteLocSQL.Parameters.Add(new SQLiteParameter("@id", id));
            return ExecuteUpdate(deleteLocSQL, "RemoveLocation");
        }


        /// <summary>
        /// Searchs the Destination table for a location whose name contains tha keyword provided.
        /// In the future this will support searching values from other columns
        /// </summary>
        /// <param name="keyword">The keyword to search for</param>
        /// <returns>A message with a list of Locations as its payload if any are found</returns>
        public Message SearchLocation(string keyword, LOCATION_SEARCHABLE_ATTRIBUTES sa)
        {
            SQLiteCommand searchLocSQL = null;
            string sqlString = "";
            switch (sa)
            {           
                case LOCATION_SEARCHABLE_ATTRIBUTES.Name:
                    keyword = "%" + keyword + "%";
                    sqlString = @"SELECT * FROM DESTINATION WHERE Name LIKE @key";
                    break;
                case LOCATION_SEARCHABLE_ATTRIBUTES.ID:
                    sqlString = @"SELECT * FROM DESTINATION WHERE Id=@key";
                    break;
            }
            searchLocSQL = new SQLiteCommand(sqlString, conn);
            searchLocSQL.Parameters.Add(new SQLiteParameter("@key", keyword));
            conn.Open();

            SQLiteDataReader res = searchLocSQL.ExecuteReader();

            if (res.HasRows)
            {
                List<Location> found = new List<Location>();
                object[] oarr;
                while (res.Read())
                {
                    oarr = new object[12];
                    try
                    {
                        res.GetValues(oarr);
                        found.Add(new Location((long)oarr[0], (string)oarr[1], (string)oarr[2], (long)oarr[3], (double)oarr[4], new Coordinate((string)oarr[5]), new Coordinate((string)oarr[6]),
                            (long)oarr[7], (long)oarr[8], (long)oarr[9], (long)oarr[10], (string)oarr[11]));//this isn't ideal
                        System.Diagnostics.Debug.WriteLine(found.Count + "-----");
                    }
                    catch (Exception ex)
                    {
                        Utilities.ExceptionLogger("Exception in SearchLocation function: " + ex.Message);
                        conn.Close();
                        return new Message(false, "Hit following exception while trynig to parse location search results: " + ex.Message);
                    }
                }
                conn.Close();
                return new Message(true, "Found location(s) corresponing to the given keyword", found);
            }
            else
            {
                conn.Close();
                return new Message(false, "Couldn't find a location containing the following string: " + keyword);
            }
        }
    }
}