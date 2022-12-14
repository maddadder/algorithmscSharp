using System;
using System.Numerics;

public class Point2d : IComparable
{
    public Point2d(float x, float y){
        X = x;
        Y = y;
    }
    public float X 
    {
        get => adapter.X; 
        set 
        {
            adapter = new Vector2(value, Y);
        }
    }
    public float Y
    {
        get => adapter.Y; 
        set 
        { 
            adapter = new Vector2(X, value);
        }
    }
    protected Vector2 adapter {get;set;} = new Vector2();
    public int CompareTo(object other)
    {
        if (other is Point2d)
        {
            return this.CompareTo((Point2d)other);
        }

        // Error condition: other is not a Vector3 object
        throw new ArgumentException("not a Vector2");
    }

    public int CompareTo(Point2d other)
    {
        if (Vector2.DistanceSquared(this.adapter,other.adapter) < 0)
        {
            return -1;
        }
        else if (Vector2.DistanceSquared(this.adapter,other.adapter) > 0)
        {
            return 1;
        }
        return 0;
    }
}