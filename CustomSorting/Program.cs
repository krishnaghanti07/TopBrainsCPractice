// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


// Perform Custom Sorting using IComparable and IComparer interfaces

using System ;
using System.Collections.Generic ;

public class Student : IComparable<Student>
{
    public string Name { get; set; }
    public int Age { get; set; }
    public double Marks { get; set; }


    public int CompareTo (Student other)
    {
        if (other == null) return 1 ;

        int result = other.Marks.CompareTo(this.Marks) ;
        if (result == 0)
        {
            result = this.Age.CompareTo(other.Age) ;
        }
        return result ;
    }

}

public class CustomSorting
{
    public static void Main (string[] args)
    {
        List<Student> students = new List<Student>()
        {
            new Student{Name = "Alice", Age = 20, Marks = 85.5},
            new Student{Name = "Bob", Age = 22, Marks = 90.0},
            new Student{Name = "Charlie", Age = 21, Marks = 85.5},
            new Student{Name = "Peter", Age = 19, Marks = 85.5},
            new Student{Name = "David", Age = 23, Marks = 95.0},
            new Student{Name = "Robin", Age = 18, Marks = 95.0}
        } ;

        students.Sort() ;

        Console.WriteLine("Students sorted by Marks (desc) and Age (asc):") ;
        foreach (var student in students)
        {
            Console.WriteLine($"Name: {student.Name}, Age: {student.Age}, Marks: {student.Marks}") ;
        }
    }
}