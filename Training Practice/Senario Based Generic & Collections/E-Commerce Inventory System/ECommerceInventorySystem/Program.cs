// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using System;
using System.Collections.Generic;
using System.Linq;

// Base product interface
public interface IProduct
{
    int Id { get; }
    string Name { get; }
    decimal Price { get; set; }
    Category Category { get; }
}

public enum Category { Electronics, Clothing, Books, Groceries }

// 1. Create a generic repository for products
public class ProductRepository<T> where T : class, IProduct
{
    private List<T> _products = new List<T>();
    
    // TODO: Implement method to add product with validation
    public void AddProduct(T product)
    {
        if (product == null)
            throw new ArgumentNullException("Product cannot be null");

        // Rule: Product ID must be unique
        if (_products.Any(p => p.Id == product.Id))
            throw new Exception("Product ID must be unique");

        // Rule: Price must be positive
        if (product.Price <= 0)
            throw new Exception("Price must be greater than zero");

        // Rule: Name cannot be null or empty
        if (string.IsNullOrWhiteSpace(product.Name))
            throw new Exception("Product name cannot be empty");

        // Add to collection if validation passes
        _products.Add(product);
    }
    
    // TODO: Create method to find products by predicate
    public IEnumerable<T> FindProducts(Func<T, bool> predicate)
    {
        return _products.Where(predicate);
    }
    
    // TODO: Calculate total inventory value
    public decimal CalculateTotalValue()
    {
        return _products.Sum(p => p.Price);
    }

    public List<T> GetAll()
    {
        return _products;
    }
}

// 2. Specialized electronic product
public class ElectronicProduct : IProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public Category Category => Category.Electronics;
    public int WarrantyMonths { get; set; }
    public string Brand { get; set; }
}

// Simple Clothing Product
public class ClothingProduct : IProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public Category Category => Category.Clothing;
    public string Size { get; set; }
}

// Simple Book Product
public class BookProduct : IProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public Category Category => Category.Books;
    public string Author { get; set; }
}

// 3. Create a discounted product wrapper
public class DiscountedProduct<T> where T : IProduct
{
    private T _product;
    private decimal _discountPercentage;
    
    public DiscountedProduct(T product, decimal discountPercentage)
    {
        if (product == null)
            throw new ArgumentNullException("Product cannot be null");

        // Discount must be between 0 and 100
        if (discountPercentage < 0 || discountPercentage > 100)
            throw new Exception("Discount must be between 0 and 100");

        _product = product;
        _discountPercentage = discountPercentage;
    }
    
    // TODO: Implement calculated price with discount
    public decimal DiscountedPrice => _product.Price * (1 - _discountPercentage / 100);
    
    // TODO: Override ToString to show discount details
    public override string ToString()
    {
        return $"{_product.Name} | Original: {_product.Price:C} | Discount: {_discountPercentage}% | Final: {DiscountedPrice:C}";
    }
}

// 4. Inventory manager with constraints
public class InventoryManager
{
    // TODO: Create method that accepts any IProduct collection
    public void ProcessProducts<T>(IEnumerable<T> products) where T : IProduct
    {
        Console.WriteLine("\nAll Products:");
        foreach (var p in products)
        {
            // a) Print all product names and prices
            Console.WriteLine($"{p.Name} - {p.Price:C}");
        }

        // b) Find the most expensive product
        var expensive = products.OrderByDescending(p => p.Price).FirstOrDefault();
        if (expensive != null)
            Console.WriteLine($"\nMost Expensive: {expensive.Name} - {expensive.Price:C}");

        // c) Group products by category
        Console.WriteLine("\nGrouped By Category:");
        var grouped = products.GroupBy(p => p.Category);
        foreach (var group in grouped)
        {
            Console.WriteLine(group.Key);
            foreach (var item in group)
            {
                Console.WriteLine($"  {item.Name}");
            }
        }

        // d) Apply 10% discount to Electronics over $500
        Console.WriteLine("\nElectronics over $500 (10% discount):");
        foreach (var p in products.Where(p => p.Category == Category.Electronics && p.Price > 500))
        {
            var discounted = new DiscountedProduct<IProduct>(p, 10);
            Console.WriteLine(discounted);
        }
    }
    
    // TODO: Implement bulk price update with delegate
    public void UpdatePrices<T>(List<T> products, Func<T, decimal> priceAdjuster) 
        where T : IProduct
    {
        foreach (var p in products)
        {
            try
            {
                p.Price = priceAdjuster(p);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating price for {p.Name}: {ex.Message}");
            }
        }
    }
}

// 5. TEST SCENARIO
class Program
{
    static void Main()
    {
        var electronicsRepo = new ProductRepository<ElectronicProduct>();
        var manager = new InventoryManager();

        try
        {
            // b) Create a sample inventory with at least 5 products
            electronicsRepo.AddProduct(new ElectronicProduct { Id = 1, Name = "Laptop", Price = 1200, Brand = "Dell", WarrantyMonths = 24 });
            electronicsRepo.AddProduct(new ElectronicProduct { Id = 2, Name = "Smartphone", Price = 800, Brand = "Samsung", WarrantyMonths = 12 });
            electronicsRepo.AddProduct(new ElectronicProduct { Id = 3, Name = "Headphones", Price = 150, Brand = "Sony", WarrantyMonths = 6 });

            Console.WriteLine("Products added successfully.\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        // Finding products by brand (for electronics)
        var samsungProducts = electronicsRepo.FindProducts(p => p.Brand == "Samsung");
        Console.WriteLine("Samsung Products:");
        foreach (var item in samsungProducts)
        {
            Console.WriteLine(item.Name);
        }

        // Calculating total value before discount
        var totalBefore = electronicsRepo.CalculateTotalValue();
        Console.WriteLine($"\nTotal Inventory Value Before Discount: {totalBefore:C}");

        // Apply bulk price increase of 5%
        manager.UpdatePrices(electronicsRepo.GetAll(), p => p.Price * 1.05m);

        var totalAfter = electronicsRepo.CalculateTotalValue();
        Console.WriteLine($"Total Inventory Value After 5% Increase: {totalAfter:C}");

        // Mixed collection
        var mixedProducts = new List<IProduct>
        {
            new ElectronicProduct { Id = 4, Name = "TV", Price = 900, Brand = "LG", WarrantyMonths = 24 },
            new ClothingProduct { Id = 5, Name = "T-Shirt", Price = 25, Size = "M" },
            new BookProduct { Id = 6, Name = "C# Basics", Price = 40, Author = "John Doe" }
        };

        // Handling mixed collection
        manager.ProcessProducts(mixedProducts);

        Console.WriteLine("\nProgram Finished.");
    }
}
