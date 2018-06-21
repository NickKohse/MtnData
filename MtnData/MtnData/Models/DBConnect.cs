using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;

namespace MtnData.Models
{
    public abstract class DBConnect
    {        
        protected string dbConnectString = "Data Source = C:\\Users\\Nick-Desktop\\Documents\\MtnData\\mtn.sqlite3; Version=3;";
        protected SQLiteConnection conn;       
    }
}