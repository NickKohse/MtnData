﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using MtnData.Models.Messages;


namespace MtnData.Models
{
    public class UserConnect : DBConnect
    {
        public UserConnect() : base() { }

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
            if (!UniqueAttribute(email, Columns.Email))
            {
                return new Message(false, "Email is being used by another user");
            }
            if (!UniqueAttribute(username, Columns.Username))
            {
                return new Message(false, "Username is being used by another user");
            }
            if (!GoodPassword(password))
            {
                return new Message(false, "Password too weak");
            }
            string sqlString = @"INSERT INTO User (Name, Email, Username, Password, Type) VALUES (@name,@email,@username,@password,@type)";
            SQLiteCommand addUserSQL = new SQLiteCommand(sqlString, conn);
            addUserSQL.Parameters.Add(new SQLiteParameter("@name", name));
            addUserSQL.Parameters.Add(new SQLiteParameter("@email", email));
            addUserSQL.Parameters.Add(new SQLiteParameter("@username", username));
            addUserSQL.Parameters.Add(new SQLiteParameter("@password", password));
            addUserSQL.Parameters.Add(new SQLiteParameter("@type", Globals.USER_TYPES.Regular)); //any user made in this fashion must be a regular user

            return ExecuteUpdate(addUserSQL, "AddUser");
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
            if (attr == "Password" && !GoodPassword(newAttr))
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
            catch (Exception ex)
            {
                Utilities.ExceptionLogger("An exception has occured in the ChangePass function. Exception message:" + ex.Message);
                return new Message(false, "An unexpected exception has occured");
            }
            return new Message(true, "Password has been changed");
        }

        public Message RemoveUser(int id)
        {
            string sqlString = @"DELETE FROM Users WHERE Id=@id";
            SQLiteCommand deleteLocSQL = new SQLiteCommand(sqlString, conn);
            deleteLocSQL.Parameters.Add(new SQLiteParameter("@id", id));
            return ExecuteUpdate(deleteLocSQL, "RemoveUser");
        }

        /// <summary>
        /// This fuction checks a users credentials and logs them in if they're valid
        /// </summary>
        /// <param name="username">The username of the user logging in</param>
        /// <param name="pass">The passowrd of the user logging in</param>
        /// <returns>a message with a bool indicating success and a string mesage</returns>
        public LoginMessage Login(string username, string pass)
        {
            string SqlString = @"SELECT Id, Name, Username, Email, Type FROM User WHERE Username= @user AND Password= @pass";
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
                    object[] oarr = new object[5];
                    res.GetValues(oarr);
                    toReturn = new User(Int32.Parse(oarr[0].ToString()), oarr[1].ToString(), oarr[2].ToString(), oarr[3].ToString(), Int32.Parse(oarr[4].ToString()));
                }
                res.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                Utilities.ExceptionLogger("An exception has occurred in the Login function. Exception message:" + ex.Message);
                return new LoginMessage(false, "An unexpected exception has occurred", null);
            }
            return new LoginMessage(true, "Login succeded", toReturn);
        }

        /// <summary>
        /// These two function are pretty much the same because sqlite didn't like having the name of a column as a parameter
        /// *********see if you can fix that
        /// </summary>
        /// <param name="col"> column to check</param>
        /// <param name="entry"> value to check</param>
        /// <returns> true if it is unique</returns>
        private enum Columns{ Username, Email }
        private bool UniqueInUsername(string username, Columns c)
        {
            string SqlString = @"SELECT * FROM User WHERE Username= @val";
            SQLiteCommand findSame = new SQLiteCommand(SqlString, conn);
            findSame.Parameters.Add(new SQLiteParameter("@val", username));
            conn.Open();
            SQLiteDataReader res = findSame.ExecuteReader();  
            
            //*********handle exception by throwing
            if (res.HasRows)
            {
                conn.Close();
                return false;
            }
            conn.Close();
            return true;
        }

        private bool UniqueInEmail(string email, Columns c)
        {
            string SqlString = @"SELECT * FROM User WHERE Email= @val";
            SQLiteCommand findSame = new SQLiteCommand(SqlString, conn);
            findSame.Parameters.Add(new SQLiteParameter("@val", email));
            conn.Open();
            SQLiteDataReader res = findSame.ExecuteReader();

            //*********handle exception by throwing
            if (res.HasRows)
            {
                conn.Close();
                return false;
            }
            conn.Close();
            return true;
        }
        /// <summary>
        /// Hopefuly this fixes the above problem
        /// </summary>
        /// <param name="element"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        private bool UniqueAttribute(string element, Columns c)
        {
            string SqlString = "";
            switch (c)
            {
                case (Columns.Email):
                    SqlString = @"SELECT * FROM User WHERE Email= @val";
                    break;
                case (Columns.Username):
                    SqlString = @"SELECT * FROM User WHERE Username= @val";
                    break;
            }
            SQLiteCommand findSame = new SQLiteCommand(SqlString, conn);
            findSame.Parameters.Add(new SQLiteParameter("@val", element));
            conn.Open();
            SQLiteDataReader res = findSame.ExecuteReader();

            //*********handle exception by throwing
            if (res.HasRows)
            {
                conn.Close();
                return false;
            }
            conn.Close();
            return true;
        }

        /// <summary>
        /// Checks if a password is strong enough
        /// </summary>
        /// <param name="pass">The passsword in question</param>
        /// <returns>True for a good password, false if it's too weak</returns>
        private Boolean GoodPassword(String pass)
        {
            if (pass.Length < 8 || !pass.Any(char.IsUpper) || !pass.Any(char.IsLower) || !pass.Any(char.IsDigit))
            {
                return false;
            }
            return true;
        }

        public bool Exists(long id)
        {
            string sqlString = @"SELECT Id FROM USER WHERE Id=@id";
            SQLiteCommand check = new SQLiteCommand(sqlString, conn);
            check.Parameters.Add(new SQLiteParameter("@id", id));

            conn.Open();
            SQLiteDataReader res = check.ExecuteReader();
            bool tmp = res.HasRows;
            conn.Close();
            return tmp;
        }
    }
}