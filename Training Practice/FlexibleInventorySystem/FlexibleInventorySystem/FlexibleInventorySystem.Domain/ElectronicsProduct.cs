namespace FlexibleInventorySystem.Domain
{
    public class ElectronicsProduct : Product
    {
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public int WarrantyPeriodMonths { get; private set; }
        public int PowerUsageWatts { get; private set; }

        public ElectronicsProduct()
        {
            // TODO: Initialize electronics properties
        }

        public override string GetCategory()
        {
            // TODO: Return category name
            return string.Empty;
        }

        public override void Validate()
        {
            // TODO: Implement validation rules
        }
    }
}