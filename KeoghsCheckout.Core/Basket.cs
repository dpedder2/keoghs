using System.Collections.Generic;
using System.Linq;

namespace KeoghsCheckout.Core
{
    public class Basket
    {
        private List<LineItem> _lineItems = new List<LineItem>();

        public void AddItem(Item item)
        {
            _lineItems.Add(new LineItem(item));
        }

        public Item GetItem(string sku)
        {
            return _lineItems.FirstOrDefault(i => i.Item.SKU == sku)?.Item;
        }
    }
}