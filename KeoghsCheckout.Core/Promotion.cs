using System;

namespace KeoghsCheckout.Core
{
    public class Promotion
    {
        private readonly Func<int, int, decimal> _offer;
        public string SKU { get; }
        public string Description { get; }

        public Promotion(string sku, string description, Func<int, int, decimal> offer)
        {
            _offer = offer;
            SKU = sku;
            Description = description;
        }

        public decimal Apply(int lineItemQuantity, int itemUnitPrice) => _offer(lineItemQuantity, itemUnitPrice);
    }
}