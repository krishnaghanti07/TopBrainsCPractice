using System;
using FlexibleInventorySystem.Services;
using FlexibleInventorySystem.Repositories;


namespace FlexibleInventorySystem.Domain
{
    public abstract class Product
    {
        public Guid ProductId { get; protected set; }
        public string Name { get; protected set; }
        public string SKU { get; protected set; }
        public decimal Price { get; protected set; }
        public int QuantityInStock { get; protected set; }
        public DateTime CreatedDate { get; protected set; }

        protected Product(string name, string sku, decimal price, int quantity)
        {
            ProductId = Guid.NewGuid();
            Name = name;
            SKU = sku;
            Price = price;
            QuantityInStock = quantity;
            CreatedDate = DateTime.UtcNow;
        }

        public abstract string GetCategory();
        public abstract void Validate();

        public virtual void UpdateStock(int quantity)
        {
            if (QuantityInStock + quantity < 0)
                throw new InvalidOperationException("Insufficient stock.");

            QuantityInStock += quantity;
        }
    }
}
