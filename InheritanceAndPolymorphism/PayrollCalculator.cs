using System;

public class PayrollCalculator
{
    public static decimal CalculateTotalPay(string[] employees)
    {
        decimal total = 0;

        foreach (string empData in employees)
        {
            string[] parts = empData.Split(' ');
            Employee emp = null;

            if (parts[0] == "H")
            {
                emp = new HourlyEmployee(
                    decimal.Parse(parts[1]),
                    decimal.Parse(parts[2])
                );
            }
            else if (parts[0] == "S")
            {
                emp = new SalariedEmployee(
                    decimal.Parse(parts[1])
                );
            }
            else if (parts[0] == "C")
            {
                emp = new CommissionEmployee(
                    decimal.Parse(parts[1]),
                    decimal.Parse(parts[2])
                );
            }

            if (emp != null)
                total += emp.GetPay();
        }

        return Math.Round(total, 2);
    }
}
