using System;
using System.Collections.Generic;
using System.Linq;

namespace KeoghsCheckout.Core
{
    public class Basket
    {
        private List<LineItem> _lineItems = new List<LineItem>();
        private List<Promotion> _promotions = new List<Promotion>();

        public void AddItem(Item item)
        {
            var lineItem = GetLineItem(item.SKU);
            if (lineItem == null)
            {
                _lineItems.Add(new LineItem(item));
            }
            else
            {
                lineItem.IncrementQuantity();
            }
        }

        public Item GetItem(string sku)
        {
            return _lineItems.FirstOrDefault(i => i.Item.SKU == sku)?.Item;
        }

        public LineItem GetLineItem(string sku)
        {
            return _lineItems.FirstOrDefault(i => i.Item.SKU == sku);
        }

        public void AddPromotion(Promotion promotion)
        {
            var promo = _promotions.FirstOrDefault(i => i.SKU == promotion.SKU);
            if (promo != null)
            {
                throw new ArgumentException();
            }
            
            _promotions.Add(promotion);
        }

        public Promotion GetPromotion(string sku)
        {
            return _promotions.FirstOrDefault(i => i.SKU == sku);
        }
    }
}