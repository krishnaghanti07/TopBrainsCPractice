// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using System;

class Program
{
    static void Main()
    {
        string[] employees =
        {
            "H 200 10",
            "S 30000",
            "C 5000 20000"
        };

        decimal totalPay = PayrollCalculator.CalculateTotalPay(employees);

        Console.WriteLine("Total Payroll = " + totalPay);
    }
}
