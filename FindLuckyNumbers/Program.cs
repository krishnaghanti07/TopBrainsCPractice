// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System ;

public class FindLuckyNumbers
{
    public static void Main (string[] args)
    {
        Console.Write("Enter The Starting of The Range : ") ;
        int startRange = int.Parse(Console.ReadLine()) ;
        Console.Write("Enter The Ending of The Range : ") ;
        int endRange = int.Parse(Console.ReadLine()) ;

        int Count = 0 ;

        Console.WriteLine("Lucky Numbers in the given range : ") ;
        for (int number = startRange ; number <= endRange ; number++)
        {
            if (IsLuckyNumber(number))
            {
                Count++;
            }
        }
        Console.WriteLine("Total Lucky Numbers between " + startRange + " and " + endRange + " is: " + Count) ;
    }

    public static bool IsLuckyNumber (int number)
    {
        if (!IsPrime(number))
        {
            // Console.WriteLine(number + " is not prime.") ;
            int x = number ;
            int x2 = number * number ;

            return (S(x2) == (S(x) * S(x))) ? true : false ;  
        }
        return false ;
    }

    public static bool IsPrime (int number)
    {
        if (number < 2) return false ;

        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0) return false ;
        }
        return true ;
    }

    public static int S (int number)
    {
        int sum = 0 ;
        while (number > 0)
        {
            sum += number % 10 ;
            number /= 10 ;
        }
        return sum ;
    }
}