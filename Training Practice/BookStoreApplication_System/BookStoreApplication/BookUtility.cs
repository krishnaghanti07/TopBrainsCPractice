using System;

namespace BookStoreApplication
{

    public class NegativeStockException : Exception
    {
        public NegativeStockException(string message) : base(message) { } 
    }

    public class NegativePriceException : Exception
    {
        public NegativePriceException(string message) : base(message) { } 
    }


    public class BookUtility
    {
        private Book _book;

        public BookUtility(Book book)
        {
            // TODO: Assign book object
            _book = book;
        }

        public void GetBookDetails()
        {
            // TODO:
            // Print format:
            // Details: <BookId> <Title> <Price> <Stock>
            Console.WriteLine($"Book Details:   Id: {_book.Id}, Title:{_book.Title}, Price:{_book.Price}, Stock:{_book.Stock}");
        }

        public void UpdateBookPrice(int newPrice)
        {
            // TODO:
            // Validate new price
            if (newPrice < 0) throw new NegativePriceException("Updated Price Must be Greater Than Zero.");
            // Update price
            _book.Price = newPrice;
            // Print: Updated Price: <newPrice>
            Console.WriteLine($"Updated Price: {_book.Price}");
        }

        public void UpdateBookStock(int newStock)
        {
            // TODO:
            // Validate new stock
            if (newStock < 0) throw new NegativeStockException("Updated Stock Must be Greater Than Zero.");
            // Update stock
            _book.Stock = newStock;
            // Print: Updated Stock: <newStock>
            Console.WriteLine($"Updated Stock: {_book.Stock}");
        }
    }
}
