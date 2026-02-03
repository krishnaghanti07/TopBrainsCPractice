// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using System;

public class Program
{
    public static string Evaluate(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
            return "Error:InvalidExpression";

        string[] parts = expression.Split(' ');
        if (parts.Length != 3)
            return "Error:InvalidExpression";

        int a, b;
        if (!int.TryParse(parts[0], out a) || !int.TryParse(parts[2], out b))
            return "Error:InvalidNumber";

        string op = parts[1];

        if (op == "+")
            return (a + b).ToString();

        if (op == "-")
            return (a - b).ToString();

        if (op == "*")
            return (a * b).ToString();

        if (op == "/")
        {
            if (b == 0)
                return "Error:DivideByZero";

            return (a / b).ToString();
        }

        return "Error:UnknownOperator";
    }

    public static void Main()
    {
        string expression = Console.ReadLine();
        string result = Evaluate(expression);
        Console.WriteLine(result);
    }
}
