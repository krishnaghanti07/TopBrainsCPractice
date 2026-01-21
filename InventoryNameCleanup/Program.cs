using System;
using System.Globalization;
public class Program
{
    public static void Main()
    {
        Console.Write("Enter product name: ");
        string productName = Console.ReadLine();
        string cleanedName = CleanProductName(productName);
        Console.WriteLine("Cleaned Product Name: " + cleanedName);
    }
    public static string CleanProductName(string name)
    {
        string trimmedName = name.Trim();
        string ans = "";
        foreach(char c in trimmedName)
        {
            if(ans.Length == 0 || ans[ans.Length-1] != c)
            {
                ans += c;
            }
        }
        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
        string finalResult = textInfo.ToTitleCase(ans.ToString());
        return finalResult;
    }
}