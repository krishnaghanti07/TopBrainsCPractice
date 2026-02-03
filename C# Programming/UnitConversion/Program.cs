// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using System ;

public class UnitConversion
{
    public static void Main (string[] args)
    {
        Console.Write("Enter The Length in Feet (Integer) : ") ;
        int feet = Convert.ToInt32(Console.ReadLine()) ;

        if (feet < 0 || feet > int.MaxValue)
        {
            Console.WriteLine("Invalid Input") ;
            return ;
        }

        double cm = feet * 30.48 ;
        double res = Math.Round (cm, MidpointRounding.AwayFromZero) ;

        Console.WriteLine("Length in Centimeters (rounded): " + res) ;

    }
}