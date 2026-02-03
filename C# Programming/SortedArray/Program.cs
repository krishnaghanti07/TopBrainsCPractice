using System;

public class Program
{
    public static T[] MergeSortedArrays<T>(T[] a, T[] b) where T : IComparable<T>
    {
        T[] result = new T[a.Length + b.Length];
        int i = 0, j = 0, k = 0;

        while (i < a.Length && j < b.Length)
        {
            if (a[i].CompareTo(b[j]) <= 0)
                result[k++] = a[i++];
            else
                result[k++] = b[j++];
        }

        while (i < a.Length)
            result[k++] = a[i++];

        while (j < b.Length)
            result[k++] = b[j++];

        return result;
    }

    public static double? GetAverage(double?[] values)
    {
        double sum = 0;
        int count = 0;

        foreach (double? v in values)
        {
            if (v.HasValue)
            {
                sum += v.Value;
                count++;
            }
        }

        if (count == 0)
            return null;

        return Math.Round(sum / count, 2, MidpointRounding.AwayFromZero);
    }

    public static void Main()
    {
        int[] a = { 1, 3, 5 };
        int[] b = { 2, 4, 6 };

        int[] merged = MergeSortedArrays(a, b);

        foreach (int n in merged)
            Console.Write(n + " ");

        Console.WriteLine();

        double?[] values = { 10.5, null, 20.25, null, 5.25 };
        double? avg = GetAverage(values);

        if (avg.HasValue)
            Console.WriteLine(avg);
        else
            Console.WriteLine("null");
    }
}
