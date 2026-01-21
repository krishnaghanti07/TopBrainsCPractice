// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System ;

public class InventoryNameCleanup
{
    public static void Main (string[] args)
    {
        // Console.Write("Enter The Product Name : ") ;
        // string productName = Console.ReadLine() ;
        string productName = " llapppptop bag " ;

        // Remove Consecutive Duplicates
        String cleanName = "" ;
        foreach (char ch in productName)
        {
            if (cleanName.Length == 0 || char.ToLower(cleanName[cleanName.Length - 1]) != char.ToLower(ch))
            {
                cleanName += ch ;
            }
        }

        // Trim Leading and Trailing Spaces
        string finalName = cleanName.Trim() ;

        // Convert to Title Case
        finalName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(finalName.ToLower()) ;

        // Console.WriteLine("Cleaned Product Name: " + finalName) ;
        Console.WriteLine(finalName) ;
    }
}