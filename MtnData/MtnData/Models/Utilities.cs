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
            if(pass.Length < 8 || !pass.Any(char.IsUpper) || !pass.Any(char.IsLower) || !pass.Any(char.IsDigit))
            {
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// Returns current unix time in seconds
        /// </summary>
        /// <returns></returns>
        public static long UnixTimeNow()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }

        public static void ExceptionLogger(string messageToLog)
        {
            //**********handle exceptions            
            System.IO.File.AppendAllText(Globals.baseProgramDir + "server_error_log.txt", DateTime.Now.ToString() + ": " +  messageToLog + "\n");
        }
        private static IReadOnlyDictionary<Globals.LOG_LEVELS, string> logLevels = new Dictionary<Globals.LOG_LEVELS, string>()
        {
            {Globals.LOG_LEVELS.Info, "INFO: "},
            {Globals.LOG_LEVELS.Audit, "AUDIT: "},
            {Globals.LOG_LEVELS.Error, "ERROR: "},
            {Globals.LOG_LEVELS.Critical, "CRITICAL: "}

        };
        public static void EventLogger(string messageToLog, Globals.LOG_LEVELS level)
        {
            string result;
            logLevels.TryGetValue(level, out result);
            System.IO.File.AppendAllText(Globals.baseProgramDir + "server_event_log.txt", DateTime.Now.ToString() + ": " + result + messageToLog + "\n");
        }
            
    }
}