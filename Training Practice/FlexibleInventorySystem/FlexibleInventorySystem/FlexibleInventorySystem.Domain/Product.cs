using System;

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

        protected Product()
        {
            // TODO: Initialize common properties
        }

        public abstract string GetCategory();
        public abstract void Validate();

        public virtual void UpdateStock(int quantity)
        {
            // TODO: Implement stock update logic
        }
    }
}