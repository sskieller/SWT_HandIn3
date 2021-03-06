﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ATM_ns;
using TransponderLib;


namespace TestUnit
{
    [TestFixture]
    class UT_ATM
    {
        private ATM _uut;
        private Plane plane;
        [SetUp]
        public void SetUp()
        {
            _uut = new ATM();
            plane = new Plane();
        }

        [Test]
        public void test()
        {
            plane.Tag = "ARF234";
            plane.XCoord = 23300;
            plane.YCoord = 20200;
            plane.Altitude = 12000;
            plane.Course = 33.3;
            plane.Speed = 200.42;

            var date = DateTime.Parse("2018-11-22 01:13:37.323");

            _uut.UpdatePlane(plane, 23800, 21000, 11500, date);

            Assert.That(plane.XCoord, Is.EqualTo(23800));
        }
    }
}
