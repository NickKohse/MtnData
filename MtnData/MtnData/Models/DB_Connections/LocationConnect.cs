using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using MtnData.Models.Messages;

namespace MtnData.Models.DB_Connections
{
    public class LocationConnect : DBConnect
    {
        public LocationConnect()
        {
            conn = new SQLiteConnection(dbConnectString);
        }

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
            addLocSQL.Parameters.Add(new SQLiteParameter("@distance", toAdd.Distance));
            addLocSQL.Parameters.Add(new SQLiteParameter("@coords", toAdd.Start));
            addLocSQL.Parameters.Add(new SQLiteParameter("@endcoords", toAdd.End));
            addLocSQL.Parameters.Add(new SQLiteParameter("@pdiff", toAdd.PDiff));
            addLocSQL.Parameters.Add(new SQLiteParameter("@tdiff", toAdd.TDiff));
            addLocSQL.Parameters.Add(new SQLiteParameter("@peakev", toAdd.FinalEv));
            addLocSQL.Parameters.Add(new SQLiteParameter("@verified", toAdd.Verified));
            addLocSQL.Parameters.Add(new SQLiteParameter("@description", toAdd.Description));
            return ExecuteUpdate(addLocSQL, "AddLocation");

            /*
            try
            {
                conn.Open();
                int rows = addLocSQL.ExecuteNonQuery();
                if (rows != 1)
                {
                    throw new Exception(rows + "rows were affected by inserting a location instead of the expected 1");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Utilities.ExceptionLogger("An exception has occured in the AddLocation function. Exception message:" + ex.Message);
                return new Message(false, "An unexpected exception has occured");
            }

            return new Message(true, "Sucessfully added a location");
            */

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
            /*
            try
            {
                conn.Open();
                int rows = updateLocSQL.ExecuteNonQuery();
                if (rows != 1)
                {
                    throw new Exception(rows + "rows were affected by modifying a location instead of the expected 1");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Utilities.ExceptionLogger("An exception has occured in the ModifyLocation function. Exception message:" + ex.Message);
                return new Message(false, "An unexpected exception has occured");
            }

            return new Message(true, "Sucessfully modified a location");
            */

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

            /*
            try
            {
                conn.Open();
                int rows = deleteLocSQL.ExecuteNonQuery();
                if (rows != 1)
                {
                    throw new Exception(rows + "rows were affected by deleting a location instead of the expected 1");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Utilities.ExceptionLogger("An exception has occured in the RemoveLocation function. Exception message:" + ex.Message);
                return new Message(false, "An unexpected exception has occured");
            }

            return new Message(true, "Sucessfully deleted a location");
            */
        }
    }
}