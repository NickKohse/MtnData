using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MtnData.Models
{
    public class User
    {
        private string username;
        private string name;
        private string email;
        private int type;

        public User(string n, string un, string e, int t)
        {
            username = un;
            name = n;
            email = e;
            type = t;
        }
    }
}