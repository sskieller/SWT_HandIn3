using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace ATM
{
    class Program
    {
        static void Main(string[] args)
        {
            //var atm = new ATM();

            //while (Console.Read() != 'q') ;
        }
    }

    public class ATM
    {
        private readonly ITransponderReceiver _receiver;
        private ITransponderDataParser _dataParser;

        private List<Plane> _planes = new List<Plane>();

        public ATM()
        {
            _dataParser = new TransponderDataParser();
            _receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            _receiver.TransponderDataReady += ReceiverOnTransponderDataReady;
        }

        private void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs rawTransponderDataEventArgs)
        {
            foreach (var data in rawTransponderDataEventArgs.TransponderData)
            {
                string tag;
                int xCoord;
                int yCoord;
                uint altitude;
                DateTime time;

                try
                {
                    _dataParser.ParseData(data, out tag, out xCoord, out yCoord, out altitude, out time);

                    if (_planes.Exists(s => s.Tag == tag))
                    {
                        // Ignore
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
