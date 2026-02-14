namespace FlexibleInventorySystem.Domain
{
    public class ClothingProduct : Product
    {
        public string Size { get; private set; }
        public string FabricType { get; private set; }
        public string Gender { get; private set; }
        public string Color { get; private set; }

        public ClothingProduct()
        {
            // TODO: Initialize clothing properties
        }

        public override string GetCategory()
        {
            // TODO: Return category name
            return string.Empty;
        }

        public override void Validate()
        {
            // TODO: Implement clothing validation rules
        }
    }
}