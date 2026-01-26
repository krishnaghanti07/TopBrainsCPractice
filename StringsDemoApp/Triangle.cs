public class Triangle : Shape
{
    double b;
    double h;

    public Triangle(double b, double h)
    {
        this.b = b;
        this.h = h;
    }

    public override double Area()
    {
        return 0.5 * b * h;
    }
}

