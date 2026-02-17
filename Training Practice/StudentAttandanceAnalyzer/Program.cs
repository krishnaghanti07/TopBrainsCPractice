// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Enter The Input : ") ;
        string input = Console.ReadLine() ;
        string[] strArr = input.Split(",") ;

        Dictionary<int, bool?> dictionary = new Dictionary<int, bool?>() ;
        int notMarked = 0 ;
        int totalPresent = 0 ;
        int totalAbsent = 0 ;

        foreach (string str in strArr)
        {
            string[] arr = str.Split(":") ;

            int.TryParse((arr[0]), out int id) ;
            if(id==0) continue;

            if (arr[1] != "")
            {
                bool attandance = (arr[1] == "Present") ? true : false ;
                dictionary.Add(id, attandance) ;
            } else
            {
                notMarked++ ;
                dictionary.Add(id, null) ;
            }
        }
        Console.WriteLine("Attandance Report") ;
        Console.WriteLine("-----------------") ;

        foreach (var ele in dictionary)
        {
            if (ele.Value == true)
            {
                totalPresent++ ;
            } else
            {
                totalAbsent++ ;
            }
        }

        foreach (var ele in dictionary)
        {
            string res = (ele.Value == null) ? "Not Marked" : ((ele.Value == true) ? "Present" : "Absent") ;
            Console.WriteLine($"{ele.Key} -> {res}");
        }
        Console.WriteLine() ;

        Console.WriteLine($"Total Present : {totalPresent}") ;
        Console.WriteLine($"Total Absent : {totalAbsent}") ;
        Console.WriteLine($"Not Marked : {notMarked}") ;
    }
}