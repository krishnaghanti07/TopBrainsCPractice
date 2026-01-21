// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System ;

public class GreatestOfThree
{
    public static void Main (string[] args)
    {
        Console.Write("Enter The First Number : ") ;
        int num1 = int.Parse(Console.ReadLine()) ;

        Console.Write("Enter The Second Number : ") ;
        int num2 = int.Parse(Console.ReadLine()) ;

        Console.Write("Enter The Third Number : ") ;
        int num3 = int.Parse(Console.ReadLine()) ;

        int greatest ;

        if (num1 >= num2 && num1 >= num3)
        {
            greatest = num1 ;
        }
        else if (num2 >= num1 && num2 >= num3)
        {
            greatest = num2 ;
        }
        else
        {
            greatest = num3 ;
        }

        Console.WriteLine("The Greatest Number is : " + greatest) ;
    }
}