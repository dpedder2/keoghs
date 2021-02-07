using System.Transactions;
using KeoghsCheckout.Core;
using NUnit.Framework;

namespace KeoghsCheckout.Tests
{
    [TestFixture]
    public class BasketTests
    {
        /*
         * BasketAddPromotion_ExistingPromotion_ThrowException
         * BasketTotalCost_ItemsAddedNoPromotion_CorrectTotalCost
         * BasketTotalCost_ItemsAddedWithPromotion_CorrectTotalCost
         * BasketTotalCost_ItemsAddedWithAndWithoutPromotions_CorrectTotalCost
         */
        
        [Test]
        public void BasketAddItem_Item_AddsItem()
        {
            Basket basket = new Basket();
            basket.AddItem(new Item("A", 10));
            Item item = basket.GetItem("A");
            
            Assert.IsNotNull(item);
            Assert.AreEqual("A", item.SKU);
        }

        [Test]
        public void BasketAddItem_ExistingItem_IncrementLineQuantity()
        {
            Basket basket = new Basket();
            basket.AddItem(new Item("A", 10));
            basket.AddItem(new Item("A", 10));
            
            LineItem lineItem = basket.GetLineItem("A");
            
            Assert.AreEqual(2, lineItem.Quantity);
        }

        [Test]
        public void BasketAddPromotion_Promotion_AddPromotion()
        {
            Basket basket = new Basket();
            basket.AddPromotion(new Promotion("A", "3 for 40", (quantity, unitPrice) =>
            {
                var total = 0.0m;
                while (quantity >= 3)
                {
                    total += 40;
                    quantity -= 3;
                }

                total += quantity * unitPrice;
                return total;
            }));

            Promotion promo = basket.GetPromotion("A");
            
            Assert.IsNotNull(promo);
            Assert.AreEqual(promo.SKU, "A");
            Assert.AreEqual(promo.Description, "3 for 40");
        }
    }
}