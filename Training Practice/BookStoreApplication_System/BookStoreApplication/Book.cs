using System;

namespace BookStoreApplication
{
    public class Book
    {
        // TODO: Create public properties
        // Id -> string
        public string Id { get; set; }
        // Title -> string
        public string Title { get; set; }
        // Author -> string (Optional)
        public string Author { get; set; }
        // Price -> int
        public int Price { get; set; }
        // Stock -> int
        public int Stock { get; set; }

        // Example:
        // public string Id { get; set; }
    }
}
