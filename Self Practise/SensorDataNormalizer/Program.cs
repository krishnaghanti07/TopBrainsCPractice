// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


public class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Enter The Input : ");
        string input = Console.ReadLine() ;


        string[] strArr = input.Split(",") ;
        List<float> floats = new List<float>() ;

        foreach (string str in strArr)
        {
            string st = str.Trim() ;
            if (float.TryParse(st, out float ft) && !float.IsNaN(ft))
            {
                float f = MathF.Round(ft, 2) ;
                floats.Add(f) ;
            }
        }

        Console.Write("{ ") ;
        foreach (float ft in floats)
        {
            Console.Write(ft.ToString("0.00") + " ") ;
        }
        Console.Write("}") ;
    }
}