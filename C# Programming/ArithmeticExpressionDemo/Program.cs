// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
using System ;
using System.IO.Pipelines;

class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Enter The Expression : ");
        string expression = Console.ReadLine() ;

        string ansStr = Calculate(expression) ;
        Console.WriteLine(ansStr) ;
    }

    public static string Calculate(string str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return "Error : Null or Empty String Error" ;
        }

        str[] arr = str.Split() ;

        string x1 = int.parse(arr[0]);
        string op = arr[1] ;
        int x2 = int.parse(arr[2]);

        if ((!int.TryParse(x1, out int num1))|| (!int.TryParse(x2, out int num2))) {
            return "Error : InvalidNumber" ;
        }

        if (op=="/" && num2==0)
        {
            return "Error : DivideByZero" ;
        }

        if (arr.Length < 3)
        {
            return "Error : FormatError" ;
        }

        int result ;
        switch(op)
        {
            case "+" :
                result = num1 + num2 ;
                break ;
            case "-" :
                result = num1 - num2 ;
                break ;
            case "*" :
                result = num1 * num2 ;
                break ;
            case "/" :
                result = num1 / num2 ;
                break ;
            case "%" :
                result = num1 % num2 ;
                break ;
            default :
                return "Error : InvalidInput Operation" ;
        }
        return result.ToString() ;
    }
}