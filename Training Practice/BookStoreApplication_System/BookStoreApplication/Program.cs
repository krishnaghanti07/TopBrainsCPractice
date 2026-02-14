using System;

namespace BookStoreApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO:
            // 1. Read initial input
            // Format: BookID Title Price Stock
            Console.Write("Enter The Inputs : ");
            string input = Console.ReadLine();
            string[] strArr = input.Split(" ");

            Book book = new Book()
            {
                Id = strArr[0],
                Title = strArr[1],
                Author = strArr[2],
                Price = int.Parse(strArr[3]),
                Stock = int.Parse(strArr[4])
            };

            BookUtility utility = new BookUtility(book);

            while (true)
            {
                // TODO:
                // Display menu:
                // 1 -> Display book details
                // 2 -> Update book price
                // 3 -> Update book stock
                // 4 -> Exit
                DisplayMenu();

                Console.Write("Enter The Choice : ");
                int choice = int.Parse(Console.ReadLine()); // TODO: Read user choice

                switch (choice)
                {
                    case 1:
                        utility.GetBookDetails();
                        break;

                    case 2:
                        // TODO:
                        // Read new price
                        Console.Write("Enter the new price : ");
                        int newPrice = int.Parse(Console.ReadLine());
                        // Call UpdateBookPrice()
                        utility.UpdateBookPrice(newPrice);
                        break;

                    case 3:
                        // TODO:
                        // Read new stock
                        Console.Write("Enter the new stock : ");
                        int newStock = int.Parse(Console.ReadLine());
                        // Call UpdateBookStock()
                        utility.UpdateBookStock(newStock);
                        break;

                    case 4:
                        Console.WriteLine("Thank You");
                        return;

                    default:
                        // TODO: Handle invalid choice
                        Console.WriteLine("Wrong Entry");
                        break;
                }
            }
        }

        public static void DisplayMenu()
        {
            Console.WriteLine("1. Display Book Details.");
            Console.WriteLine("2. Update Book Details.");
            Console.WriteLine("3. Update Book Stock. ");
            Console.WriteLine("4. Exit");
        }
    }
}
