using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MtnData.Models
{
    public class User
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public int Type { get; private set; }

        public User(int i, string n, string un, string e, int t)
        {
            Id = i;
            Username = un;
            Name = n;
            Email = e;
            Type = t;
        }
    }
}