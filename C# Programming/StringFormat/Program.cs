// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using System;
using System.Collections.Generic;
using System.Text.Json;

public record Student(string Name, int Score);

public class Program
{
    public static string BuildJson(string[] items, int minScore)
    {
        List<Student> students = new List<Student>();

        foreach (string item in items)
        {
            string[] parts = item.Split(':');
            string name = parts[0];
            int score = int.Parse(parts[1]);

            if (score >= minScore)
                students.Add(new Student(name, score));
        }

        students.Sort((a, b) =>
        {
            int scoreCompare = b.Score.CompareTo(a.Score);
            if (scoreCompare != 0)
                return scoreCompare;

            return a.Name.CompareTo(b.Name);
        });

        return JsonSerializer.Serialize(students);
    }

    public static void Main()
    {
        string[] items = { "Alice:85", "Bob:70", "Carol:85", "Dave:60" };
        int minScore = 70;

        string json = BuildJson(items, minScore);
        Console.WriteLine(json);
    }
}
