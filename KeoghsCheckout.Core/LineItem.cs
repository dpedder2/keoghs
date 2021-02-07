namespace KeoghsCheckout.Core
{
    public class LineItem
    {
        public Item Item { get; }
        private int _quantity = 0; 

        public LineItem(Item item)
        {
            Item = item;
        }
    }
}