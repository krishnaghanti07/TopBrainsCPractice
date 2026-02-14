using System;
using FlexibleInventorySystem.Services;

namespace FlexibleInventorySystem.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IInventoryService inventoryService = new InventoryService();

            Console.WriteLine("Flexible Inventory System");
            Console.WriteLine("--------------------------");

            // TODO:
            // 1. Create Electronics Product
            // 2. Create Grocery Product
            // 3. Create Clothing Product
            // 4. Add them using InventoryService
            // 5. Retrieve and display

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}