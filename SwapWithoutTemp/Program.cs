// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using System ;

public class SwapWithoutTemp 
{
    public static void Main (string[] args)
    {
        Console.Write("Enter The First Number : ") ;
        int a = int.Parse(Console.ReadLine()) ;

        Console.Write("Enter The Second Number : ") ;
        int b = int.Parse(Console.ReadLine()) ;

        Console.WriteLine("Before Swapping: a = " + a + ", b = " + b) ;
        Swap(ref a, ref b) ;
        Console.WriteLine("After Swapping using ref : a = " + a + ", b = " + b) ;

        // use of out keyword

        SwapOut(ref a, ref b, out c, out d) ;
        Console.WriteLine("After Swapping using out : c = " + c + ", d = " + d) ;
    }

    public static void Swap (ref int x, ref int y)
    {
        x = x + y ;
        y = x - y ;
        x = x - y ;
    }

    public static void SwapOut (ref int x, ref int y, out int a, out int b)
    {
        a = x + y ;
        b = a - y ;
        a = a - b ;
    }
}