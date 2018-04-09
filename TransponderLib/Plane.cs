using System;

namespace ATM
{
    public class Plane
    {
        public string Tag { get; set; }
        public int XCoord { get; set; }
        public int YCoord { get; set; }
        public int Altitude { get; set; }
        public uint Speed { get; set; }
        public uint Course { get; set; }

        public DateTime LastUpdated { get; set; }

        public Plane()
        {

        }
    }
}