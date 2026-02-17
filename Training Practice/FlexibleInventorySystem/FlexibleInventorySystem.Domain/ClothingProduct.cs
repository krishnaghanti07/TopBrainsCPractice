using System;
using FlexibleInventorySystem.Services;
using FlexibleInventorySystem.Repositories;


namespace FlexibleInventorySystem.Domain
{
    public class ClothingProduct : Product
    {
        public string Size { get; private set; }
        public string FabricType { get; private set; }
        public string Gender { get; private set; }
        public string Color { get; private set; }

        public ClothingProduct(
            string name,
            string sku,
            decimal price,
            int quantity,
            string size,
            string fabric,
            string gender,
            string color)
            : base(name, sku, price, quantity)
        {
            Size = size;
            FabricType = fabric;
            Gender = gender;
            Color = color;

            Validate();
        }

        public override string GetCategory() => "Clothing";

        public override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Size))
                throw new ArgumentException("Size required.");

            if (string.IsNullOrWhiteSpace(FabricType))
                throw new ArgumentException("Fabric required.");
        }
    }
}
