using System;
using System.Numerics;
namespace Models
{
public abstract class IPoint2dPair {
    public IPoint2dPair(Point2d point1, Point2d point2){
        X1 = point1.X;
        Y1 = point1.Y;
        X2 = point2.X;
        Y2 = point2.Y;
    }
    public abstract float X1{get;set;}
    public abstract float Y1{get;set;}
    public abstract float X2{get;set;}
    public abstract float Y2{get;set;}
    public abstract float DistanceSquared();
}
public class Point2dPair : IPoint2dPair, IComparable
{
    public Point2dPair(Point2d point1, Point2d point2) : base(point1,point2)
    {

    }
    public override float X1
    {
        get => left.X; 
        set 
        {
            left = new Vector2(value, Y1);
        }
    }
    public override float Y1
    {
        get => left.Y; 
        set 
        { 
            left = new Vector2(X1, value);
        }
    }
    public override float X2
    {
        get => right.X; 
        set 
        {
            right = new Vector2(value, Y2);
        }
    }
    public override float Y2
    {
        get => right.Y; 
        set 
        { 
            right = new Vector2(X2, value);
        }
    }
    protected Vector2 left {get;set;}
    protected Vector2 right {get;set;}
    public override float DistanceSquared()
    {
        return Vector2.DistanceSquared(left, right);
    }
    public int CompareTo(object other)
    {
        if (other is Point2dPair)
        {
            return this.CompareTo((Point2dPair)other);
        }

        // Error condition: other is not a Vector3 object
        throw new ArgumentException("not a Vector2");
    }

    public int CompareTo(Point2dPair other)
    {
        if (Vector2.DistanceSquared(this.left,this.right) < Vector2.DistanceSquared(other.left,other.right))
        {
            return -1;
        }
        else if (Vector2.DistanceSquared(this.left,this.right) > Vector2.DistanceSquared(other.left,other.right))
        {
            return 1;
        }
        return 0;
    }

    
}
}