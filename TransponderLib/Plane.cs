using System;

namespace TransponderLib
{
    public class Plane
    {
        public string Tag { get; set; }
        public int XCoord { get; set; }
        public int YCoord { get; set; }
        public int Altitude { get; set; }
        public double Speed { get; set; }
        public double Course { get; set; }
        public DateTime LastUpdated { get; set; }

        public Plane()
        {

        }
    }
}