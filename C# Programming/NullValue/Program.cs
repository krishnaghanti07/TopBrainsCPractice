using System;

public class Program
{
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

        double avg = sum / count;
        return Math.Round(avg, 2, MidpointRounding.AwayFromZero);
    }

    public static void Main()
    {
        double?[] values = { 12.5, null, 7.5, 10.0, null };

        double? result = GetAverage(values);

        if (result.HasValue)
            Console.WriteLine(result);
        else
            Console.WriteLine("null");
    }
}
