using System;
using FlexibleInventorySystem.Services;
using FlexibleInventorySystem.Repositories;


namespace FlexibleInventorySystem.Domain
{
    public class ElectronicsProduct : Product
    {
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public int WarrantyPeriodMonths { get; private set; }
        public int PowerUsageWatts { get; private set; }

        public ElectronicsProduct(
            string name,
            string sku,
            decimal price,
            int quantity,
            string brand,
            string model,
            int warrantyMonths,
            int powerUsage)
            : base(name, sku, price, quantity)
        {
            Brand = brand;
            Model = model;
            WarrantyPeriodMonths = warrantyMonths;
            PowerUsageWatts = powerUsage;

            Validate();
        }

        public override string GetCategory() => "Electronics";

        public override void Validate()
        {
            if (WarrantyPeriodMonths < 0)
                throw new ArgumentException("Warranty must be positive.");

            if (PowerUsageWatts <= 0)
                throw new ArgumentException("Power usage must be greater than zero.");
        }
    }
}
