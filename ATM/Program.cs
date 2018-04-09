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

            }
        }
    }


}
