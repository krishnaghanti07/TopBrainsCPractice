// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using System;

class GCDProgram
{
    static void Main()
    {
        Console.Write("Enter first number: ");
        int a = int.Parse(Console.ReadLine());

        Console.Write("Enter second number: ");
        int b = int.Parse(Console.ReadLine());

        int result = GCD(a, b);

        Console.WriteLine("GCD = " + result);
    }

    static int GCD(int a, int b)
    {
        if (b == 0)
            return a;

        return GCD(b, a % b);
    }
}
