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
        public Boolean AddUser(string name, string username, string password, string email)
        {
            //************check uniqueness of username and email
            //************add password reqs
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
                //****************log exception
                return false; 
                
            }
            return true;
        }
    }
}