using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MtnData.Models
{
    public class Location
    {
        public string Name { get; private set; }
        public string Region { get; private set; }
        public int EvGain { get; private set; }
        public float Distance { get; private set; }
        public Coordinate Start { get; private set; }
        public Coordinate End { get; private set; }
        public int PDiff { get; private set; }
        public int TDiff { get; private set; }
        public int FinalEv { get; private set; }
        public bool Verified { get; private set; }
        public string Description { get; private set; }

        public Location(string name, string region, int evGain, float distance, Coordinate start, Coordinate end, int pDiff, int tDiff, int finalEv, bool verified, string description)
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
            Start = start;
            End = end;
            TDiff = tDiff;
            PDiff = pDiff;
            FinalEv = finalEv;
            Verified = verified;
            Description = description;
        }
    }

    public class Coordinate{
        private double NS;
        private double EW;

        public Coordinate(double ns, double ew)
        {
            CheckArgumentValidity(ns, ew);
            NS = ns;
            EW = ew;
        }

        public Coordinate(string x)
        {
            string[] split = x.Split(' ');
            if (split.Length != 4)
            {
                throw new ArgumentException("String must have four space seperated values. This string had" + split.Length);
            }
            else
            {
                double ns = 0;
                double ew = 0;
                try
                {
                    ns = double.Parse(split[1]);
                    ew = double.Parse(split[3]);
                }
                catch
                {
                    throw new ArgumentException("Unparseable field in input string, space seperated values 2 and 4 must be doubles.");
                }
                CheckArgumentValidity(ns, ew);
                new Coordinate(ns, ew);
            }
        }

        override
        public string ToString()
        {
            return "NS: " + NS.ToString() + " EW: " + EW.ToString();
        }

        private void CheckArgumentValidity(double ns, double ew)
        {
            if (ns > 90 || ns < -90)
            {
                throw new ArgumentOutOfRangeException("ns", "North south parameter must be in [-90, 90]");
            }
            if (ew > 180 || ew < -180)
            {
                throw new ArgumentOutOfRangeException("ew", "East parameter must be in [-180, 180]");
            }
        }
        
    }
}