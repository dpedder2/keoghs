namespace KeoghsCheckout.Core
{
    public class Item
    {
        public string SKU { get; }
        public int UnitPrice { get; }

        public Item(string sku, int unitPrice)
        {
            SKU = sku;
            UnitPrice = unitPrice;
        }
    }
}