// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using System ;

public class TimeConversion
{
    public static void Main(string[] args)
    {
        Console.Write("Enter The Time in Seconds (Integer) : ") ;
        int totalSeconds = Convert.ToInt32(Console.ReadLine()) ;

        if (totalSeconds < 0 || totalSeconds > 3599)
        {
            Console.WriteLine("Invalid Input") ;
            return ;
        }

        int minutes = totalSeconds / 60 ;
        int seconds = totalSeconds % 60 ;

        string formatted = String.Format("{0}:{1:D2}", minutes, seconds) ;
        Console.WriteLine("Formatted Time (m:ss): " + formatted) ;

    }
}