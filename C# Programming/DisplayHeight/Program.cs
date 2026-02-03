// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System ;

public class DisplayHeight
{
    public static void Main (string[] args)
    {
        Console.Write("Enter The Height in Centimeters (Integer) : ") ;
        int height = Convert.ToInt32(Console.ReadLine()) ;

        if (height <= 0 || height >= 300)
        {
            Console.WriteLine("Invalid Input") ;
            return ;
        }

        if (height >= 180)
        {
            Console.WriteLine("Tall") ;
        }
        else if (height >= 150)
        {
            Console.WriteLine("Average") ;
        }
        else
        {
            Console.WriteLine("Short") ;
        }
    }
}