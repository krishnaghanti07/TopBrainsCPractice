// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using System;
using System.Collections.Generic;

public static class StringExtensions
{
    public static string[] DistinctById(this string[] items)
    {
        List<string> result = new List<string>();
        HashSet<string> seenIds = new HashSet<string>();

        foreach (string item in items)
        {
            string[] parts = item.Split(':');
            string id = parts[0];
            string name = parts[1];

            if (!seenIds.Contains(id))
            {
                seenIds.Add(id);
                result.Add(name);
            }
        }

        return result.ToArray();
    }
}

public class Program
{
    public static void Main()
    {
        string[] items = { "1:Alice", "2:Bob", "1:Carol", "3:Dave", "2:Eve" };

        string[] names = items.DistinctById();

        foreach (string name in names)
            Console.WriteLine(name);
    }
}
