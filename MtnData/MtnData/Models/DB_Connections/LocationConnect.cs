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

        public Message AddLocation()
        {

        }
    }
}