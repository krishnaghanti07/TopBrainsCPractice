// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using System;

class ObjectArraySum
{
    static void Main()
    {
        object[] values = { 10, "hello", true, 25, null, 5.5, 15 };

        int result = SumIntegers(values);

        Console.WriteLine("Sum = " + result);
    }

    static int SumIntegers(object[] values)
    {
        int sum = 0;

        foreach (object item in values)
        {
            if (item is int number)
            {
                sum += number;
            }
        }

        return sum;
    }
}
