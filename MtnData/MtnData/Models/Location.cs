using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MtnData.Models
{
    public class Location
    {
        private string Name { get; set; }
        private string Region { get; set; }
        private int EvGain { get; set; }
        private float Distance { get; set; }
        private Coordinate Start { get; set; }
        private Coordinate End { get; set; }
        private int PDiff { get; set; }
        private int TDiff { get; set; }
        private int FinalEv { get; set; }
        private bool Verified { get; set; }

        public Location(string name, string region, int evGain, float distance, Coordinate start, Coordinate end, int pDiff, int tDiff, int finalEv, bool verified)
        {
            if (pDiff < 1 || pDiff > 5)
            {
                throw new ArgumentOutOfRangeException("pDiff", "PDiff must be in [1,5]");
            }

            if (tDiff < 1 || tDiff > 5)
            {
                throw new ArgumentOutOfRangeException("tDiff", "TDiff must be in [1,5]");
            }
            Name = name;
            Region = region;
            EvGain = evGain;
            Distance = distance;
            start = Start;
            end = End;
            tDiff = TDiff;
            pDiff = PDiff;
            finalEv = FinalEv;
            verified = Verified;
        }
    }

    public class Coordinate{
        private double NS;
        private double EW;

        public Coordinate(double ns, double ew)
        {
            if (ns > 90 || ns < -90)
            {
                throw new ArgumentOutOfRangeException("ns", "North south parameter must be in [-90, 90]");
            }
            if (ew > 180 || ew < -180)
            {
                throw new ArgumentOutOfRangeException("ew", "East parameter must be in [-180, 180]");
            }
            NS = ns;
            EW = ew;
        }
    }
}