// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


// Perform Custom Sorting using IComparable interfaces

using System ;
using System.Collections.Generic ;

class Student : IComparable<Student>
{
    public string Name { get; set; }
    public int Age { get; set; }
    public int Marks { get; set; }

    public override string ToString()
    {
        return $"Name: {Name} Id:{Age} Marks: {Marks}";
    }

    public int CompareTo(Student other)
    {
        if (other == null) return 1;

        int res = other.Age.CompareTo(this.Age); // descending
        if (res != 0) return res;

        return this.Marks.CompareTo(other.Marks); // ASCENDING

    }
}

class program
{
    public static void Main()
    {
        List<Student> students = new List<Student>()
        {
            new Student {Name = "Krishna", Age = 100, Marks = 20},
            new Student {Name = "Shubham", Age = 100, Marks = 10},
            new Student {Name = "Alto", Age = 200, Marks = 30}
        };

        students.Sort(); // compare to persormaed internally

        foreach (var val in students)
        {
            Console.WriteLine(val);
        }
    }
}