using System;

public class UnitConversion
{
    public static double FeetToCentimeters(int feet)
    {
        double centimeters = feet * 30.48;
        return Math.Round(centimeters, 2, MidpointRounding.AwayFromZero);
    }

    public static void Main(string[] args)
    {
        int feet = 10;   
        double result = FeetToCentimeters(feet);

        Console.WriteLine(result);
    }
}
