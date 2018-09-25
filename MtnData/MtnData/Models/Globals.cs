using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MtnData.Models
{
    public class Globals
    {
        public enum USER_TYPES { Owner, Admin, Regular };
        public enum LOG_LEVELS { Info, Audit, Error, Critical };
        public enum LOCATION_SEARCHABLE_ATTRIBUTES { Name, ID };
        public const string baseProgramDir = @"C:\Users\Nick-Desktop\Documents\MtnData\";
        public static Location PRESENT_LOC = null;
    }
}