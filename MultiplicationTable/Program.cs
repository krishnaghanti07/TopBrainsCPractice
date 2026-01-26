// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using System;

public class MultiplicationTable
{
    public static int[] GetTableRow(int n, int upto)
    {
        int[] row = new int[upto];

        for (int i = 1; i <= upto; i++)
        {
            row[i - 1] = n * i;
        }

        return row;
    }

    public static void Main(string[] args)
    {
        int n = 3;      
        int upto = 5;   

        int[] result = GetTableRow(n, upto);

        foreach (int value in result)
        {
            Console.Write(value + " ");
        }
    }
}
