// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleEcomApp
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }

        public Product(int id, string name, double price, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsBlacklisted { get; set; }

        public Customer(int id, string name, bool blacklisted = false)
        {
            Id = id;
            Name = name;
            IsBlacklisted = blacklisted;
        }
    }

    public class OrderItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public OrderItem(Product p, int qty)
        {
            Product = p;
            Quantity = qty;
        }

        public double TotalPrice()
        {
            return Product.Price * Quantity;
        }
    }

    public interface IDiscountStrategy
    {
        double ApplyDiscount(double amount);
    }

    public class PercentageDiscount : IDiscountStrategy
    {
        public double Percentage { get; set; }

        public PercentageDiscount(double percent)
        {
            Percentage = percent;
        }

        public double ApplyDiscount(double amount)
        {
            return amount - (amount * Percentage / 100);
        }
    }

    public class FlatDiscount : IDiscountStrategy
    {
        public double FlatAmount { get; set; }

        public FlatDiscount(double amt)
        {
            FlatAmount = amt;
        }

        public double ApplyDiscount(double amount)
        {
            return amount - FlatAmount;
        }
    }

    public class FestivalDiscount : IDiscountStrategy
    {
        public double ApplyDiscount(double amount)
        {
            return amount - (amount * 15 / 100);
        }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public Customer Customer { get; set; }
        public List<OrderItem> Items { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public IDiscountStrategy DiscountStrategy { get; set; }

        public Order(int id, Customer cust)
        {
            OrderId = id;
            Customer = cust;
            Items = new List<OrderItem>();
            OrderDate = DateTime.Now;
            OrderStatus = "Placed";
        }

        public void AddItem(Product p, int qty)
        {
            if (p.Stock < qty)
                throw new OutOfStockException("Stock not available");

            p.Stock -= qty;
            Items.Add(new OrderItem(p, qty));
        }

        public double GetTotal()
        {
            double total = Items.Sum(i => i.TotalPrice());
            if (DiscountStrategy != null)
                total = DiscountStrategy.ApplyDiscount(total);
            return total;
        }

        public void Cancel()
        {
            if (OrderStatus == "Shipped")
                throw new OrderAlreadyShippedException("Order already shipped");

            OrderStatus = "Cancelled";
        }
    }

    public class OutOfStockException : Exception
    {
        public OutOfStockException(string msg) : base(msg) { }
    }

    public class OrderAlreadyShippedException : Exception
    {
        public OrderAlreadyShippedException(string msg) : base(msg) { }
    }

    public class CustomerBlacklistedException : Exception
    {
        public CustomerBlacklistedException(string msg) : base(msg) { }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Product> products = new List<Product>();
            List<Customer> customers = new List<Customer>();
            List<Order> orders = new List<Order>();
            Dictionary<int, Product> productMap = new Dictionary<int, Product>();

            products.Add(new Product(1, "Laptop", 60000, 15));
            products.Add(new Product(2, "Mobile", 20000, 8));
            products.Add(new Product(3, "Headphones", 2000, 25));
            products.Add(new Product(4, "Keyboard", 1500, 5));

            foreach (var p in products)
                productMap[p.Id] = p;

            customers.Add(new Customer(1, "Rahul"));
            customers.Add(new Customer(2, "Ritika"));
            customers.Add(new Customer(3, "Amit", true));
            customers.Add(new Customer(4, "Sneha"));

            try
            {
                var cust = customers.First(c => c.Id == 1);
                if (cust.IsBlacklisted)
                    throw new CustomerBlacklistedException("Customer is blacklisted");

                Order order1 = new Order(101, cust);
                order1.AddItem(productMap[1], 1);
                order1.AddItem(productMap[3], 2);
                order1.DiscountStrategy = new PercentageDiscount(10);
                orders.Add(order1);

                Order order2 = new Order(102, customers[1]);
                order2.AddItem(productMap[2], 1);
                order2.DiscountStrategy = new FlatDiscount(500);
                orders.Add(order2);

                Order order3 = new Order(103, customers[3]);
                order3.AddItem(productMap[4], 2);
                order3.DiscountStrategy = new FestivalDiscount();
                orders.Add(order3);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Orders in last 7 days:");
            var recent = orders.Where(o => o.OrderDate >= DateTime.Now.AddDays(-7));
            foreach (var o in recent)
                Console.WriteLine(o.OrderId);

            Console.WriteLine("Total Revenue:");
            Console.WriteLine(orders.Sum(o => o.GetTotal()));

            Console.WriteLine("Most Sold Product:");
            var mostSold = orders
                .SelectMany(o => o.Items)
                .GroupBy(i => i.Product.Name)
                .OrderByDescending(g => g.Sum(i => i.Quantity))
                .FirstOrDefault();
            if (mostSold != null)
                Console.WriteLine(mostSold.Key);

            Console.WriteLine("Top 5 Customers:");
            var topCustomers = orders
                .GroupBy(o => o.Customer.Name)
                .Select(g => new { Name = g.Key, Total = g.Sum(o => o.GetTotal()) })
                .OrderByDescending(x => x.Total)
                .Take(5);
            foreach (var c in topCustomers)
                Console.WriteLine(c.Name + " - " + c.Total);

            Console.WriteLine("Orders Grouped By Status:");
            var grouped = orders.GroupBy(o => o.OrderStatus);
            foreach (var g in grouped)
            {
                Console.WriteLine(g.Key + " - " + g.Count());
            }

            Console.WriteLine("Low Stock Products:");
            var lowStock = products.Where(p => p.Stock < 10);
            foreach (var p in lowStock)
                Console.WriteLine(p.Name + " - " + p.Stock);
        }
    }
}
