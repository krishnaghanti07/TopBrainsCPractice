// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using System ;
using System.Collections ;

public class DictonaryLookup
{
    public static void Main (string[] args)
    {
        Dictionary<int, int> employee = new Dictionary<int, int>()
        {
            {101, 50000},
            {102, 60000},
            {103, 55000},
            {104, 70000},
            {105, 65000}
        };
        
        int totalSalary = 0 ;

        foreach (KeyValuePair<int, int> emp in employee)
        {
            totalSalary += emp.Value ;
        }

        Console.WriteLine("Total Salary of all Employees : " + totalSalary) ;
    }
}