using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using MtnData.Models.Messages;


namespace MtnData.Models
{
    public class UserConnect : DBConnect
    {
        public UserConnect()
        {
            conn = new SQLiteConnection(dbConnectString);
        }
        public readonly List<string> CHANGABLE_USER_ATTRIBUTES = new List<string>() { "Email", "Password", "Name" };
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
            if (!UniqueInCol("Username", username))
            {
                return new Message(false, "Username is being used by another user");
            }
            if (!Utilities.GoodPassword(password))
            {
                return new Message(false, "Password too weak");
            }
            SQLiteCommand addUserSQL = new SQLiteCommand("INSERT INTO User (Name, Email, Username, Password, Type) VALUES (?,?,?,?,?)", conn);
            addUserSQL.Parameters.Add(name);
            addUserSQL.Parameters.Add(email);
            addUserSQL.Parameters.Add(username);
            addUserSQL.Parameters.Add(password);
            addUserSQL.Parameters.Add(Globals.USER_TYPES.Regular); //any user made in this fashion must be a regular user
            try 
            {
                int rows = addUserSQL.ExecuteNonQuery();
                if(rows != 1)
                {
                    throw new Exception(rows + "rows were affected by inserting a user instead of the expected 1");
                }
            }
            catch (Exception ex)
            {
                Utilities.ExceptionLogger("An exception has occured in the AddUser function. Exception message:" + ex.Message);
                return new Message(false, "An unexpected exception has occured");

            }
            return new Message(true, "Sucessfully added user");
        }

        /// <summary>
        /// This function allows certain attributes of a user to be changed, these attribuets are specified in the CHANGEBALE_USER_ATTRIBUTES
        /// list in this class
        /// </summary>
        /// <param name="username"> username of the user to be modified</param>
        /// <param name="attr"> the attribute to be modified</param>
        /// <param name="newAttr"> the new value for this attribute</param>
        /// <returns> a message with a bool indicating success and a string mesage</returns>
        public Message ChangeAttribute(string username, string attr, string newAttr)
        {
            if (!CHANGABLE_USER_ATTRIBUTES.Contains(attr))
            {
                return new Message(false, "This is not a changeable attribute.");
            }
            if (attr == "Password" && !Utilities.GoodPassword(newAttr))
            {
                return new Message(false, "Password too weak");
            }
            SQLiteCommand change = new SQLiteCommand("UPDATE Users SET @attr=@new WHERE Username=@name");
            change.Parameters.AddWithValue("@attr", attr);
            change.Parameters.AddWithValue("@new", newAttr);
            change.Parameters.AddWithValue("@name", username);
            try
            {
                int rows = change.ExecuteNonQuery();
                if (rows != 1)
                {
                    throw new Exception(rows + "rows were affected by changing a password instead of the expected 1");
                }

            }
            catch(Exception ex)
            {
                Utilities.ExceptionLogger("An exception has occured in the ChangePass function. Exception message:" + ex.Message);
                return new Message(false, "An unexpected exception has occured");
            }
            return new Message(true, "Password has been changed");
        }

        /// <summary>
        /// This fuction checks a users credentials and logs them in if they're valid
        /// </summary>
        /// <param name="username">The username of the user logging in</param>
        /// <param name="pass">The passowrd of the user logging in</param>
        /// <returns>a message with a bool indicating success and a string mesage</returns>
        public LoginMessage Login(string username, string pass)
        {
            string SqlString = @"SELECT Name, Username, Email, Type FROM User WHERE Username= @user AND Password= @pass";
            SQLiteCommand login = new SQLiteCommand(SqlString, conn);
            
            login.Parameters.Add(new SQLiteParameter("@user", username));
            login.Parameters.Add(new SQLiteParameter("@pass", pass));
            User toReturn = null;

            try
            {
                conn.Open();
                SQLiteDataReader res = login.ExecuteReader();
                if (!res.HasRows)
                {
                    return new LoginMessage(false, "Username or Password Incorrect", null);
                }
                else //not checking for the case of more than one row returned as its logically impossible
                {
                    object [] oarr = new object[4];
                    res.GetValues(oarr);
                    toReturn = new User(oarr[0].ToString(), oarr[1].ToString(), oarr[2].ToString(), Int32.Parse(oarr[3].ToString()));
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                Utilities.ExceptionLogger("An exception has occurred in the Login function. Exception message:" + ex.Message);
                return new LoginMessage(false, "An unexpected exception has occurred", null);
            }
            return new LoginMessage(true, "Login succeded", toReturn);
        }

        /// <summary>
        /// A utility function for this class that chekcs if a value is unique within the column it would be inserted into
        /// </summary>
        /// <param name="col"> column to check</param>
        /// <param name="entry"> value to check</param>
        /// <returns> true if it is unique</returns>
        private bool UniqueInCol(string col, Object entry)
        {
            SQLiteCommand findSame = new SQLiteCommand("SELECT * FROM User WHERE @col=@val", conn);
            findSame.Parameters.AddWithValue("@col", col);
            findSame.Parameters.AddWithValue("@val", entry);
            SQLiteDataReader res = findSame.ExecuteReader();
            //*********handle exception by throwing
            if (res.HasRows)
            {
                return false;
            }
            return true;
        }
    }
}