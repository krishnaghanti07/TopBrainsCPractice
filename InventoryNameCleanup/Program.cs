using System;
using System.Text;
using System.Globalization;

public class InventoryNameCleanup
{
    public static void Main(string[] args)
    {
        Console.Write("Enter The Product Name : ");
        string productName = Console.ReadLine();

        string trimmedName = productName.Trim();
        StringBuilder cleanName = new StringBuilder();

        bool previousSpace = false;

        foreach (char ch in trimmedName)
        {
            if (ch == ' ')
            {
                if (!previousSpace)
                {
                    cleanName.Append(ch);
                    previousSpace = true;
                }
            }
            else
            {
                cleanName.Append(ch);
                previousSpace = false;
            }
        }

        string finalName = CultureInfo.CurrentCulture.TextInfo
            .ToTitleCase(cleanName.ToString().ToLower());

        Console.WriteLine("Cleaned Product Name: " + finalName);
    }
}
