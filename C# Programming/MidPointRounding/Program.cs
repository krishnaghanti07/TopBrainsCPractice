// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using System ;

public class MidPointRouting
{
    public static void Main (string[] args)
    {
        Console.Write("Enter The Radius of The Circle : ") ;
        double radius = double.Parse(Console.ReadLine()) ;

        if (radius < 0 || radius > double.MaxValue)
        {
            Console.WriteLine("Invalid Radius") ;
            return ;
        }

        double area = Math.PI * Math.Pow(radius, 2) ;
        double roundedArea = Math.Round(area, MidpointRounding.AwayFromZero) ;
        Console.WriteLine("Area of the Circle rounded using MidpointRounding.AwayFromZero: " + roundedArea) ;
    }
}