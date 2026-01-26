// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System;

class ParsingSum
{
    static void Main()
    {
        string[] tokens = { "10", "abc", "20", "999", "-5", "30" };

        int result = SumParsedIntegers(tokens);

        Console.WriteLine("Sum = " + result);
    }

    static int SumParsedIntegers(string[] tokens)
    {
        int sum = 0;

        foreach (string token in tokens)
        {
            int value;
            if (int.TryParse(token, out value))
            {
                sum += value;
            }
        }

        return sum;
    }
}
