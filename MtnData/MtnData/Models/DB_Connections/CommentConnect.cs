using MtnData.Models.Messages;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace MtnData.Models.DB_Connections
{
    public class CommentConnect : DBConnect
    {
        public CommentConnect() : base() {}

        /// <summary>
        /// Function to add a comment to the comment table
        /// </summary>
        /// <param name="text">The content of the comment</param>
        /// <param name="userID">The id of the user making the comment</param>
        /// <param name="locID">The id of the location the comment is being made about</param>
        /// <returns>A message which realys whether or not the operation was sucessful</returns>
        public Message AddComment(string text, long userID, long locID)
        {
            //check referential integrity
            string sqlString = @"INSERT INTO Comment (DestId, UserId, Time, Text) VALUES(@locID, @userID, @time, @text)";
            SQLiteCommand addCommentSQL = new SQLiteCommand(sqlString, conn);
            addCommentSQL.Parameters.Add(new SQLiteParameter("@locID", locID));
            addCommentSQL.Parameters.Add(new SQLiteParameter("@userID", userID));
            addCommentSQL.Parameters.Add(new SQLiteParameter("@time", Utilities.UnixTimeNow()));
            addCommentSQL.Parameters.Add(new SQLiteParameter("@text", text));
            return ExecuteUpdate(addCommentSQL, "AddComment");
        }
        
        public Message GetLocationComments(string locID)
        {
            string sqlString = @"SELECT * FROM COMMENT WHERE Id=@id";
            SQLiteCommand getCommentsSQL = new SQLiteCommand(sqlString, conn);
            getCommentsSQL.Parameters.Add(new SQLiteParameter("@id", locID));

            conn.Open();
            SQLiteDataReader results = getCommentsSQL.ExecuteReader();

            if (results.HasRows)
            {
                List<Comment> commentList = new List<Comment>();
                while (results.Read())
                {
                    object[] oarr = new object[5];
                    try
                    {
                        results.GetValues(oarr);
                        commentList.Add(new Comment((long)oarr[3], (long)oarr[2], (long)oarr[1], (string)oarr[4]));
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        Utilities.ExceptionLogger("Hit exception while trying to parse comment from DB: " + ex.Message);
                        return new Message(false, "ERROR: Unable to parse comments successfully at this time.");
                    }

                }
                conn.Close();
                return new Message(true, "Found comments for location, returning List<Comment>", commentList);
            }
            else
            {
                conn.Close();
                return new Message(false, "No comments exist for this location");
            }
        }
    }
}