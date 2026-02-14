// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Enter The String : ");
        string str = Console.ReadLine() ;
        int n = str.Length ;

        int i = 0 ;
        while (i < n)
        {
            int count = 1 ;
            int j = i + 1 ;

            while (j<n && str[i] == str[j])
            {
                count++ ;
                j++ ;
            }
            if (count == 1)
            {
                Console.Write(str[i]) ;
            } else
            {
                Console.Write(str[i] + count.ToString()) ;
            }
            i = j ;
        }
    }
}