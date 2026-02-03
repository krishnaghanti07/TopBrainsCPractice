using System;

public class ArraySum
{
    public static int SumPositiveUntilZero(int[] nums)
    {
        int sum = 0;

        foreach (int num in nums)
        {
            if (num == 0)
                break;

            if (num < 0)
                continue;

            sum += num;
        }

        return sum;
    }

    public static void Main(string[] args)
    {
        int[] nums = { 5, -3, 7, 0, 10, 2 }; 
        int result = SumPositiveUntilZero(nums);

        Console.WriteLine(result);
    }
}
