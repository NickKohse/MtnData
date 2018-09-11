using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MtnData.Models.Messages
{
    /// <summary>
    /// A class to pass messages inbetween models and controllers
    /// </summary>
    public class Message
    {
        private bool result;
        private string text;
        private object payload;
        public Message(bool r, string t, object p = null)
        {
            result = r;
            text = t;
            payload = p;
        }

        public bool GetResult() { return result; }
        public string GetText() { return text; }
        public object GetPayload() { return payload; }
    }

    public class LoginMessage : Message
    {
        private User user;
        public LoginMessage(bool r, string t, User u) : base(r, t)
        {
            user = u;
        }

        public User GetUser() { return user; }
    }
}