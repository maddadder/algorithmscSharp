using System;
using System.Numerics;
namespace Lib.Model
{
    public class LineSegment : IComparable
    {
        
        public LineSegment(Vector2 point1, Vector2 point2)
        {
            this.point1 = point1;
            this.point2 = point2;
        }
        protected Vector2 point1 {get;set;}
        protected Vector2 point2 {get;set;}
        public float DistanceSquared()
        {
            return Vector2.DistanceSquared(point1, point2);
        }
        public int CompareTo(object other)
        {
            if (other is LineSegment)
            {
                return this.CompareTo((LineSegment)other);
            }

            // Error condition: other is not a Vector3 object
            throw new ArgumentException("not a Vector2");
        }

        public int CompareTo(LineSegment other)
        {
            if (Vector2.DistanceSquared(this.point1,this.point2) < Vector2.DistanceSquared(other.point1,other.point2))
            {
                return -1;
            }
            else if (Vector2.DistanceSquared(this.point1,this.point2) > Vector2.DistanceSquared(other.point1,other.point2))
            {
                return 1;
            }
            return 0;
        }
    }
}