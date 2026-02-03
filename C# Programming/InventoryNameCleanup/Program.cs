class Program
{
    public static void Main()
    {
        string str = " llapppptop bag ";
        string result = CleanupInventoryName(str);
       
        Console.WriteLine($"Output: \"{result}\"");
    }

    public static string CleanupInventoryName(string input)
    {
        // Step 1: Trim extra spaces
        string str = input.Trim();

        // Step 2: Remove consecutive duplicate characters
        string noDuplicates = "";
        for (int i = 0; i < str.Length; i++)
        {
            if (i == 0 || str[i] != str[i - 1])
            {
                noDuplicates += str[i];
            }
        }

        // Step 3: Remove extra spaces between words (keep only single spaces)
        string cleaned = "";
        for (int i = 0; i < noDuplicates.Length; i++)
        {
            if (noDuplicates[i] == ' ')
            {
                if (i == 0 || noDuplicates[i - 1] != ' ')
                {
                    cleaned += ' ';
                }
            }
            else
            {
                cleaned += noDuplicates[i];
            }
        }

        // Step 4: Convert to TitleCase (using built-in)
        string titleCase = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cleaned.ToLower());

        return titleCase;
    }
}