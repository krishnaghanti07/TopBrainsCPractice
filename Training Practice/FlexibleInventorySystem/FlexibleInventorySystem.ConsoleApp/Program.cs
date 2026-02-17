using System;
using FlexibleInventorySystem.Domain;
using FlexibleInventorySystem.Services;
using FlexibleInventorySystem.Repositories;

namespace FlexibleInventorySystem.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IInventoryService inventoryService = new InventoryService();

            Console.WriteLine("Flexible Inventory System");
            Console.WriteLine("--------------------------");

            var laptop = new ElectronicsProduct(
                "Gaming Laptop",
                "ELEC-001",
                85000,
                10,
                "Dell",
                "G15",
                24,
                180);

            var rice = new GroceryProduct(
                "Basmati Rice",
                "GROC-001",
                1200,
                50,
                DateTime.UtcNow.AddMonths(6),
                5,
                true);

            var shirt = new ClothingProduct(
                "Casual Shirt",
                "CLOTH-001",
                1999,
                25,
                "L",
                "Cotton",
                "Men",
                "Blue");

            inventoryService.AddProduct(laptop);
            inventoryService.AddProduct(rice);
            inventoryService.AddProduct(shirt);

            Console.WriteLine("\nElectronics:");
            foreach (var item in inventoryService.GetProductsByCategory<ElectronicsProduct>())
                Console.WriteLine($"{item.Name} - {item.Brand}");

            Console.WriteLine("\nGrocery:");
            foreach (var item in inventoryService.GetProductsByCategory<GroceryProduct>())
                Console.WriteLine($"{item.Name} - Expires: {item.ExpiryDate:d}");

            Console.WriteLine("\nClothing:");
            foreach (var item in inventoryService.GetProductsByCategory<ClothingProduct>())
                Console.WriteLine($"{item.Name} - {item.Size}");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
