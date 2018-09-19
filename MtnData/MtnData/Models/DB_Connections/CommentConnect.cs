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
        public Message AddComment(string text, int userID, int locID)
        {
            string sqlString = @"INSERT INTO Comment (DestId, UserId, Time, Text) VALUES(@locID, @userID, @time, @text)";
            SQLiteCommand addCommentSQL = new SQLiteCommand(sqlString, conn);
            addCommentSQL.Parameters.Add(new SQLiteParameter("@locID", locID));
            addCommentSQL.Parameters.Add(new SQLiteParameter("@userID", userID));
            addCommentSQL.Parameters.Add(new SQLiteParameter("@time", Utilities.UnixTimeNow()));
            addCommentSQL.Parameters.Add(new SQLiteParameter("@text", text));
            return ExecuteUpdate(addCommentSQL, "AddComment");
        }
        /*
        public Message GetLocationComments(int locID)
        {

        }*/
    }
}