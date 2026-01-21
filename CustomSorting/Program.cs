using System;
using System.Collections.Generic;

public class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    public double Marks { get; set; }
}

// Custom sorting using IComparer
public class StudentComparer : IComparer<Student>
{
    public int Compare(Student x, Student y)
    {
        if (x == null || y == null) return 0;

        int result = y.Marks.CompareTo(x.Marks);

        if (result == 0)
        {
            result = x.Age.CompareTo(y.Age);
        }

        return result;
    }
}

public class CustomSorting
{
    public static void Main(string[] args)
    {
        List<Student> students = new List<Student>()
        {
            new Student { Name = "Alice", Age = 20, Marks = 85.5 },
            new Student { Name = "Bob", Age = 22, Marks = 90.0 },
            new Student { Name = "Charlie", Age = 21, Marks = 85.5 },
            new Student { Name = "Peter", Age = 19, Marks = 85.5 },
            new Student { Name = "David", Age = 23, Marks = 95.0 },
            new Student { Name = "Robin", Age = 18, Marks = 95.0 }
        };

        students.Sort(new StudentComparer());

        Console.WriteLine("Students sorted by Marks (DESC) and Age (ASC):");
        foreach (var student in students)
        {
            Console.WriteLine($"Name: {student.Name}, Age: {student.Age}, Marks: {student.Marks}");
        }
    }
}
