﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TransponderLib;
using TransponderReceiver;

namespace ATM
{
    class Program
    {
        static void Main(string[] args)
        {
            var atm = new ATM();
            atm.UpdateScreen();
            while (Console.Read() != 'q') ;
        }
    }

    public class ATM
    {
        private readonly ITransponderReceiver _receiver;
        private ITransponderDataParser _dataParser;
        private CollisionDetector _detector;


        private List<Plane> _planes = new List<Plane>();

        public ATM()
        {
            _detector = new CollisionDetector();
            _dataParser = new TransponderDataParser();
            _receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            //_receiver.TransponderDataReady += ReceiverOnTransponderDataReady;
        }

        public void UpdateScreen()
        {
            Console.Clear();
            Console.WriteLine("{0,-2}Plane Tag {0,-2} | {0,-2}Plane Speed {0,-2} | {0,-2}Plane Course {0,-2} | {0,-2}Plane Seperation {0,-2} | {0,-2}Plane Updated {0,-2}",
                string.Empty);

            Console.WriteLine("{0,-2}{1,-9} {0,-2} | {0,-5}{2,-5} {0,-4} | {0,-2}{3,-6} {0,-2} | {0,-2}{4,-5} {0,-2} | {0,-2}{5,-10} {0,-2}",
                string.Empty, "MSG5440664444", 230.44555, 110.3, true, DateTime.Now.TimeOfDay.ToString());
        }

        internal void UpdatePlane(Plane planeToUpdate, int xCoord, int yCoord, int altitude, DateTime time)
        {
            int deltaXCoord = Math.Abs(xCoord - planeToUpdate.XCoord);
            int deltaYCoord = Math.Abs(yCoord - planeToUpdate.YCoord);

            double euclidianDistance = Math.Sqrt(deltaXCoord * deltaXCoord + deltaYCoord * deltaYCoord);

            double deltaTime = time.TimeOfDay.TotalMilliseconds - planeToUpdate.LastUpdated.TimeOfDay.TotalMilliseconds;

            double direction = (Math.Atan2(deltaYCoord, deltaXCoord) * (180/Math.PI));

            double speed = euclidianDistance / (deltaTime / 1000);


            planeToUpdate.LastUpdated = time;
            planeToUpdate.Speed = speed;
            planeToUpdate.XCoord = xCoord;
            planeToUpdate.YCoord = yCoord;
            planeToUpdate.Altitude = altitude;
            planeToUpdate.Course = direction;
        }
        internal void HandleData(string data)
        {
            string tag;
            int xCoord;
            int yCoord;
            int altitude;
            DateTime time;

            try
            {
                _dataParser.ParseData(data, out tag, out xCoord, out yCoord, out altitude, out time);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }

            if ((xCoord > 90000 || xCoord < 10000 || yCoord > 90000 || yCoord < 10000) &&
                _planes.Exists(s => s.Tag == tag))
                _planes.Remove(_planes.Find(p => p.Tag == tag)); // Remove existing plane, since out of airspace

            else if (xCoord > 90000 || xCoord < 10000 || yCoord > 90000 || yCoord < 10000)
                return; // Do not add plane


            // Update plane if it exists, otherwise add the plane.
            if (_planes.Exists(s => s.Tag == tag))
                UpdatePlane(_planes.Find(p => p.Tag == tag), xCoord, yCoord, altitude, time);
            else
                _planes.Add(new Plane()
                    { Tag = tag, XCoord = xCoord, YCoord = yCoord, Altitude = altitude, LastUpdated = time, Course = 0, Speed = 0 });
        }
        internal void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs rawTransponderDataEventArgs)
        {
            foreach (var data in rawTransponderDataEventArgs.TransponderData)
            {
                HandleData(data);
            }
            //_detector.DetectCollision(_planes);
            // Used to check planes
        }
    }
}
