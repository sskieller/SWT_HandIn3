using ATM;
using NUnit.Framework;

namespace TestUnit
{
    [TestFixture]
    class UT_Plane
    {
        private Plane _uut;
        [SetUp]
        public void SetUp()
        {
            _uut = new Plane();
        }

        [Test]
        public void tester()
        {
            string tag = "wat";
            _uut.Tag = tag;

            Assert.That(_uut.Tag, Is.EqualTo(tag));
        }
    }
}
