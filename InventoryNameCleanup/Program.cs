using System ;

public class InventoryNameCleanup
{
    public static void Main (string[] args)
    {
        Console.Write("Enter The Product Name : ") ;
        string productName = Console.ReadLine() ;

        string finalName = productName.Trim() ;
        String cleanName = "" ;
        foreach (char ch in finalName)
        {
            if (cleanName.Length == 0 || char.ToLower(cleanName[cleanName.Length - 1]) != char.ToLower(ch))
            {
                cleanName += ch ;
            }
        }

        

        cleanName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cleanName.ToLower()) ;

        Console.WriteLine("Cleaned Product Name: " + cleanName) ;
    }
}