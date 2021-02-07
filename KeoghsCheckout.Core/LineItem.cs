namespace KeoghsCheckout.Core
{
    public class LineItem
    {
        public Item Item { get; }
        public int Quantity { get; private set; }

        public LineItem(Item item)
        {
            Item = item;
            IncrementQuantity();
        }

        public void IncrementQuantity()
        {
            Quantity++;
        }
    }
}