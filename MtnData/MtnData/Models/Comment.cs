using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MtnData.Models
{
    public class Comment
    {
        public long Timestamp { get; private set; }
        public long UserID { get; private set; }
        public long LocationID { get; private set; }
        public string Text { get; private set; }

        public Comment(long ts, long uid, long lid, string txt)
        {
            Timestamp = ts;
            UserID = uid;
            LocationID = lid;
            Text = txt;
        }
    }
}