using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;


namespace MtnData.Models
{
    public class UserConnect : DBConnect
    {
        UserConnect()
        {
            conn = new SQLiteConnection(dbConnectString);
        }

        /// <summary>
        /// This function is used to add another user to the User table of the db
        /// </summary>
        /// <param name="name"> name of the user</param>
        /// <param name="username"> a unique username</param>
        /// <param name="password"> a sufficiently complex password</param>
        /// <param name="email"> a unique email</param>
        /// <returns>True if user is sucessfuly added, false otherwise</returns>
        public Message AddUser(string name, string username, string password, string email)
        {
            if (!UniqueInCol("Email", email))
            {
                return new Message(false, "Email is being used by another user");
            }
            if (!UniqueInCol("UserName", username))
            {
                return new Message(false, "Username is being used by another user");
            }
            if (!Utilities.GoodPassword(password))
            {
                return new Message(false, "Password too weak");
            }
            SQLiteCommand addUserSQL = new SQLiteCommand("INSERT INTO User (Name, Email, UserName, Password, Type) VALUES (?,?,?,?,?)", conn);
            addUserSQL.Parameters.Add(name);
            addUserSQL.Parameters.Add(email);
            addUserSQL.Parameters.Add(username);
            addUserSQL.Parameters.Add(password);
            addUserSQL.Parameters.Add(Globals.USER_TYPES.Regular); //any user made in this fashion must be a regular user
            try 
            {
                addUserSQL.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Utilities.ExceptionLogger("An exception has occured in the AddUser function. Exception message:" + ex.Message);
                return new Message(false, "An unexpected exception has occured"); 
                
            }
            return new Message(true, "Sucessfully added user");
        }

        private bool UniqueInCol(string col, Object entry)
        {
            SQLiteCommand findSame = new SQLiteCommand("SELECT * FROM User WHERE @col=@val", conn);
            findSame.Parameters.AddWithValue("@col", col);
            findSame.Parameters.AddWithValue("@val", entry);
            SQLiteDataReader res = findSame.ExecuteReader();
            if (res.HasRows)
            {
                return false;
            }
            return true;
        }
    }
}