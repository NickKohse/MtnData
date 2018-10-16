using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MtnData.Models
{
    public class TripReport
    {
        public long Id { get; private set; }
        public long UserId { get; private set; }
        public long DestinatinId { get; private set; }
        public long Date { get; private set; }
        public long Time { get; private set; }
        public bool Result { get; private set; }
        public string Description { get; private set; }

        public TripReport(long id, long uid, long did, long date, long time, bool result, string desc)
        {
            Id = id;
            UserId = uid;
            DestinatinId = did;
            Date = date;
            Time = time;
            Result = result;
            Description = desc;
        }
    }
}