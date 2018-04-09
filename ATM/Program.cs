using System;
using System.Collections.Generic;
using System.Linq;
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
            _receiver.TransponderDataReady += ReceiverOnTransponderDataReady;
        }


        public void UpdatePlane(Plane plane, int xCoord, int yCoord, int altitude, DateTime time)
        {
            int deltaXCoord = (xCoord - plane.XCoord);
            int deltaYCoord = (yCoord - plane.YCoord);

            double euclidianDistance = Math.Sqrt(deltaXCoord * deltaXCoord + deltaYCoord * deltaYCoord);

            double deltaTime = time.TimeOfDay.TotalMilliseconds - plane.LastUpdated.TimeOfDay.TotalMilliseconds;

            double direction = Math.Atan2(deltaXCoord, deltaYCoord);

            double speed = euclidianDistance / (deltaTime / 1000);


            plane.LastUpdated = time;
            plane.Speed = speed;
            plane.XCoord = xCoord;
            plane.YCoord = yCoord;
            plane.Altitude = altitude;
            plane.Course = direction;

        }


        private void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs rawTransponderDataEventArgs)
        {
            foreach (var data in rawTransponderDataEventArgs.TransponderData)
            {
                string tag;
                int xCoord;
                int yCoord;
                int altitude;
                DateTime time;

                try
                {
                    _dataParser.ParseData(data, out tag, out xCoord, out yCoord, out altitude, out time);

                    if (_planes.Exists(s => s.Tag == tag))
                    {
                        UpdatePlane(_planes.Find(p => p.Tag == tag), xCoord, yCoord, altitude, time);
                    }
                    else
                    {
                        _planes.Add(new Plane() {Tag = tag, XCoord = xCoord, YCoord = yCoord, Altitude = altitude, LastUpdated = time});
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }
        }
    }
}
