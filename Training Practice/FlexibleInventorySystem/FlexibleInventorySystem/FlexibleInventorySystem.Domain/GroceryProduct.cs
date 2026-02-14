using System;

namespace FlexibleInventorySystem.Domain
{
    public class GroceryProduct : Product
    {
        public DateTime ExpiryDate { get; private set; }
        public double WeightKg { get; private set; }
        public bool IsOrganic { get; private set; }

        public GroceryProduct()
        {
            // TODO: Initialize grocery properties
        }

        public override string GetCategory()
        {
            // TODO: Return category name
            return string.Empty;
        }

        public override void Validate()
        {
            // TODO: Implement expiry validation
        }
    }
}