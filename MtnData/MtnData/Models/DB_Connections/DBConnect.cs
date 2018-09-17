using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using MtnData.Models.Messages;

namespace MtnData.Models
{
    public abstract class DBConnect
    {        
        protected string dbConnectString = @"Data Source = " + Globals.baseProgramDir + "mtn.sqlite3; Version=3;";
        protected SQLiteConnection conn;

        public DBConnect()
        {
            conn = new SQLiteConnection(dbConnectString);
        }
        
        /// <summary>
        /// A function that handles performing a database update and the error checking/logging associated with that
        /// </summary>
        /// <param name="command">The SQLite command to be performed</param>
        /// <param name="fnName">The name of the function that is calling thi one, used for logging purposes</param>
        /// <param name="rowsUpdated">The number of rows expected to be updated by this command, defaults to one</param>
        /// <returns></returns>
        protected Message ExecuteUpdate(SQLiteCommand command, string fnName, int rowsUpdated = 1)
        {
            try
            {
                conn.Open();
                int rows = command.ExecuteNonQuery();
                if (rows != rowsUpdated)
                {
                    throw new Exception(rows + "rows were affected by deleting a location instead of the expected " + rowsUpdated);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Utilities.ExceptionLogger("An exception has occured in the " + fnName + " function. Exception message:" + ex.Message);
                return new Message(false, "An unexpected exception has occured");
            }

            return new Message(true, "Sucessfully updated " + rowsUpdated + " rows in a table after dExecuteUpdate called from " + fnName);
        }
    }

}