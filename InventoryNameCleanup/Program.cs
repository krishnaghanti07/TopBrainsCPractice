using System;
using System.Text;

public class InventoryNameCleanup
{
    public static void Main(string[] args)
    {
        Console.Write("Enter The Product Name: ");
        string productName = Console.ReadLine() ?? string.Empty;

        // Remove Consecutive Duplicates
        StringBuilder cleanName = new StringBuilder();
        foreach (char ch in productName)
        {
            if (cleanName.Length == 0 || char.ToLower(cleanName[cleanName.Length - 1]) != char.ToLower(ch))
            {
                cleanName.Append(ch);
            }
        }

        // Trim Leading and Trailing Spaces
        string finalName = cleanName.ToString().Trim();

        // Convert to Title Case
        finalName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(finalName.ToLower());

        Console.WriteLine("Cleaned Product Name: " + finalName);
    }
}