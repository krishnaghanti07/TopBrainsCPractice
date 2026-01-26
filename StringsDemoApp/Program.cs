// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using System;

class Program
{
    static void Main()
    {
        string[] shapes =
        {
            "C 3",
            "R 4 5",
            "T 6 2"
        };

        double total = 0;

        foreach (string s in shapes)
        {
            string[] p = s.Split(' ');
            Shape shape = null;

            if (p[0] == "C")
                shape = new Circle(double.Parse(p[1]));
            else if (p[0] == "R")
                shape = new Rectangle(
                    double.Parse(p[1]),
                    double.Parse(p[2])
                );
            else if (p[0] == "T")
                shape = new Triangle(
                    double.Parse(p[1]),
                    double.Parse(p[2])
                );

            if (shape != null)
                total += shape.Area();
        }

        total = Math.Round(total, 2, MidpointRounding.AwayFromZero);

        Console.WriteLine("Total Area = " + total);
    }
}
