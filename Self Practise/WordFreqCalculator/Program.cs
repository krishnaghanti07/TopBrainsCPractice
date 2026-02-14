// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Enter The String : ") ;
        string str = Console.ReadLine() ;

        Dictionary<char, int> dict = new Dictionary<char, int>() ;

        foreach (char ch in str)
        {
            if (dict.ContainsKey(ch))
            {
                dict[ch]++ ;
            } else
            {
                dict[ch] = 1 ;
            }
        }

        Console.WriteLine("Now Calculating The Word Frequency of Each Character of The String : " + str) ;
        Console.WriteLine() ;

        Dictionary<char, int> newDict = dict.OrderBy(pair => pair.Value).ToDictionary(pair=>pair.Key, pair=>pair.Value) ;

        foreach (var ele in newDict)
        {
            Console.WriteLine($"The Count of The Letter {ele.Key} is : -- {ele.Value}") ;
        }
    }


}