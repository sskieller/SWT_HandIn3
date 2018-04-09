using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TransponderLib;

namespace TestUnit
{
	[TestFixture]
	public class CollisionDetectorUnitTest
	{
		private List<Plane> _planes;
		[SetUp]
		public void SetUp()
		{
			_planes = new List<Plane>();
		}
	}
}
