// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System ;
using System.Collections.Generic ;

class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Enter The String : ");
        string str = Console.ReadLine() ;

        char[] arr = str.ToCharArray() ;
        Array.Reverse(arr) ; 

        for (int i=0 ; i<arr.Length ; i++)
        {
            switch (arr[i])
            {
                case 'a' :
                    arr[i] = 'e' ;
                    break ;
                case 'A' :
                    arr[i] = 'E' ;
                    break ;
                case 'e' :
                    arr[i] = 'i' ;
                    break ;
                case 'E' :
                    arr[i] = 'I' ;
                    break ;
                case 'i' :
                    arr[i] = 'o' ;
                    break ;
                case 'I' :
                    arr[i] = 'O' ;
                    break ;
                case 'o' :
                    arr[i] = 'u' ;
                    break ;
                case 'O' :
                    arr[i] = 'U' ;
                    break ;
                case 'u' :
                    arr[i] = 'a' ;
                    break ;
                case 'U' :
                    arr[i] = 'A' ;
                    break ;
                default :
                    break ;
            }
        }
        HashSet<char> set = new HashSet<char>() ;
        for (int i=0 ; i<arr.Length ; i++)
        {
            set.Add(arr[i]) ;
        }

        char[] newArr = set.ToArray() ;

        Console.Write("Enter The Rotation Number : ") ;
        int k = int.Parse(Console.ReadLine());

        int size = newArr.Length ;
        k = k % size ;

        char[] res = new char[size] ;

        for (int i=0 ; i<size ; i++)
        {
            res[i] = newArr[(i-k + size) % size] ;
        }

        string answer = new string (res) ;

        Console.WriteLine($"Result : {answer}") ;
    }
}