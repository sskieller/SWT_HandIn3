using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ATM;

namespace TestUnit
{
    [TestFixture]
    public class UT_TransponderDataParser
    {

        private ITransponderDataParser _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new TransponderDataParser();
        }

        [Test]
        public void ParseTime_AllowedNumbers_ExpectedResult_True()
        {
            var date = new DateTime(1010,10,10,10,10,10,111);
            var time = "10101010101010111";
            Assert.That(_uut.ParseTime(time),Is.EqualTo(date));
        }

        [TestCase("20180101246060", TestName = "Min/Sec too high")]
        [TestCase("20180230444444444", TestName = "30th february")]
        [TestCase("99999999999999999", TestName = "AllNines")]
        [TestCase("00000000000000000", TestName = "AllZeroes")]
        public void ParseTime_NoneligibleInput_ExpectedResult_Exception(string time)
        {
            Assert.That(() => _uut.ParseTime(time),Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        [TestCase("abc11111111111111", TestName = "Letters as input")]
        public void ParseTime_NonNumberInput_ExpectedResult_Exception(string time)
        {
            Assert.That(() => _uut.ParseTime(time), Throws.Exception.TypeOf<FormatException>());
        }

        //ATR423;39045;12932;14000;20151006213456789
        [Test]
        public void ParseData_Test()
        {
            var data = "ATR423;39045;12932;14000;20151006213456789";
            _uut.ParseData(data);
            Assert.That();
        }
    }
}
