// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");



using System ;

public class MahirlAndAlphabates
{
    public static void Main (string[] args)
    {
        Console.Write("Enter The First Word : ") ;
        string firstWord = Console.ReadLine() ;

        Console.Write("Enter The Second Word : ") ;
        string secondWord = Console.ReadLine() ;

        // Remove Common Consonants
        foreach (char ch in secondWord)
        {
            if (!"aeiou".Contains(char.ToLower(ch)))
            {
                firstWord = firstWord.Replace(ch.ToString(), "") ;
            }
        }

        // Remove Consecutive Duplicates
        string result = "" ;
        foreach (char ch in firstWord)
        {
            if (result.Length == 0 || char.ToLower(result[result.Length - 1]) != char.ToLower(ch))
            {
                result += ch ;
            }
        }

        Console.WriteLine("Resulting Word: " + result) ;
    }
}