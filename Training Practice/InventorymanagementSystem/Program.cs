// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System ;

public class NegativePriceException : Exception
{
    public NegativePriceException(string message) : base(message) {}
}
public class NegativeStockException : Exception
{
    public NegativeStockException(string message) : base(message) {}
}

public abstract class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductType { get; set; }
    public int Price { get; set; }
    public int Stock { get; set; }

    public virtual void Validate()
    {
        if (Price < 0) throw new NegativePriceException("Price is less than zero") ;
    }
}