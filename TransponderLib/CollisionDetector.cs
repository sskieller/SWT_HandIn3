using System;
using System.Collections.Generic;
using System.Text;

namespace TransponderLib
{
	//This class is used to detect whether or not any planes are colliding
    public class CollisionDetector
    {
	    public event EventHandler<CollisionEventArgs> SeparationEvent;

	    public void DetectCollision(List<Plane> planes)
	    {
			//We don't want to deal with empty lists or less than 2 planes
		    if (planes == null || planes.Count < 2)
			    return;

			//The dreaded n^2 loop
		    for (int i = 0; i < planes.Count - 1; ++i)
		    {
			    for (int j = i + 1; j < planes.Count; ++j)
			    {
				    if (planes[j].Speed == 0)
					    break; //Break in case of new plane


				    int differenceInAltitude = planes[j].Altitude - planes[i].Altitude;
				    if (differenceInAltitude < 300 && differenceInAltitude > -300)
				    {
						//Planes are within 300 meters in altitude, otherwise just ignore
						//Calculate euclidian distance
					    double xDifference = planes[j].XCoord - planes[i].XCoord;
					    double yDifference = planes[j].YCoord - planes[i].YCoord;

						double hypotenuse = Math.Pow(xDifference, 2) + Math.Pow(yDifference, 2);

						double distance = Math.Sqrt(hypotenuse);

					    if (distance < 5000)
					    {
							SeparationEvent?.Invoke(this, new CollisionEventArgs(planes[i], planes[j]));
					    }
				    }
				}
		    }
	    }
    }

	public class CollisionEventArgs : EventArgs
	{
		public Plane CollidingPlane1 { get; set; }
		public Plane CollidingPlane2 { get; set; }

		public CollisionEventArgs(Plane plane1, Plane plane2)
		{
			CollidingPlane1 = plane1;
			CollidingPlane2 = plane2;
		}
	}
}
