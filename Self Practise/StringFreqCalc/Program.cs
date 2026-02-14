// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Enter The String : ") ;
        string str = Console.ReadLine().ToLower() ;

        string[] strArr = str.Split(" ") ;
        var dict = new Dictionary<string, int>() ;

        foreach (var ele in strArr)
        {
            dict[ele] = dict.GetValueOrDefault(ele, 0) + 1 ;
        }

        var newDict = dict.OrderByDescending(pair=>pair.Key).ToDictionary(pair=>pair.Key, pair=>pair.Value) ;

        foreach (var ele in newDict)
        {
            Console.WriteLine($"The Count of The Word '{ele.Key}' is :-- {ele.Value}") ;
        }
    }
}