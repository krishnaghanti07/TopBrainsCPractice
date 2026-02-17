using System;
using FlexibleInventorySystem.Services;
using FlexibleInventorySystem.Repositories;


namespace FlexibleInventorySystem.Domain
{
    public class GroceryProduct : Product
    {
        public DateTime ExpiryDate { get; private set; }
        public double WeightKg { get; private set; }
        public bool IsOrganic { get; private set; }

        public GroceryProduct(
            string name,
            string sku,
            decimal price,
            int quantity,
            DateTime expiry,
            double weight,
            bool isOrganic)
            : base(name, sku, price, quantity)
        {
            ExpiryDate = expiry;
            WeightKg = weight;
            IsOrganic = isOrganic;

            Validate();
        }

        public override string GetCategory() => "Grocery";

        public override void Validate()
        {
            if (ExpiryDate <= DateTime.UtcNow)
                throw new ArgumentException("Product already expired.");

            if (WeightKg <= 0)
                throw new ArgumentException("Weight must be positive.");
        }
    }
}
