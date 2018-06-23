using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MtnData.Models
{
    /// <summary>
    /// A class to pass messages inbetween models and controllers
    /// </summary>
    public class Message
    {
        private bool result;
        private string text;
        public Message(bool r, string t)
        {
            result = r;
            text = t;
        }
    }

    public class LoginMessage : Message
    {
        private User user;
        public LoginMessage(bool r, string t, User u) : base(r, t)
        {
            user = u;
        }
    }
}