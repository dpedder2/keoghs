using System;
using System.Collections.Generic;
using System.Linq;

namespace KeoghsCheckout.Core
{
    public class Basket
    {
        private readonly List<LineItem> _lineItems = new List<LineItem>();
        private readonly List<Promotion> _promotions = new List<Promotion>();
        
        public decimal GetTotalCost()
        {
            var total = 0m;
            foreach (var lineItem in _lineItems)
            {
                var promo = GetPromotion(lineItem.Item.SKU);
                if (promo != null)
                {
                    total += promo.Apply(lineItem.Quantity, lineItem.Item.UnitPrice);
                }
                else
                {
                    total += lineItem.Item.UnitPrice * lineItem.Quantity;
                }
            }

            return total;
        }
        
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

        public void AddPromotion(Promotion promotion)
        {
            var promo = _promotions.FirstOrDefault(i => i.SKU == promotion.SKU);
            if (promo != null)
            {
                throw new ArgumentException();
            }
            
            _promotions.Add(promotion);
        }
        
        public Item GetItem(string sku) => _lineItems.FirstOrDefault(i => i.Item.SKU == sku)?.Item;

        public LineItem GetLineItem(string sku) => _lineItems.FirstOrDefault(i => i.Item.SKU == sku);

        public Promotion GetPromotion(string sku) => _promotions.FirstOrDefault(i => i.SKU == sku);
    }
}