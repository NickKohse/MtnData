using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MtnData.Models
{
    public static class Utilities
    {
        /// <summary>
        /// Checks if a password is strong enough
        /// </summary>
        /// <param name="pass">The passsword in question</param>
        /// <returns>True for a good password, false if it's too weak</returns>
        public static Boolean GoodPassword(String pass)
        {
            if(pass.Length < 8 || !pass.Any(char.IsUpper) || !pass.Any(char.IsLower) || !pass.Any(char.IsSymbol))
            {
                return false;
            }
            
            return true;
        }

        public static void ExceptionLogger(string messageToLog)
        {
            //**********handle exceptions            
            System.IO.File.AppendAllText(Globals.baseProgramDir + "server_error_log.txt", DateTime.Now.ToString() + " : " +  messageToLog);
        }
            
    }
}