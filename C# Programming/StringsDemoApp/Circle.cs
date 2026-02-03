using System;

public class Circle : Shape
{
    double r;

    public Circle(double r)
    {
        this.r = r;
    }

    public override double Area()
    {
        return Math.PI * r * r;
    }
}
