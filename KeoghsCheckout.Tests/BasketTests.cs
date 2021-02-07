using System;
using System.Transactions;
using KeoghsCheckout.Core;
using NUnit.Framework;

namespace KeoghsCheckout.Tests
{
    [TestFixture]
    public class BasketTests
    {
        /*
         * 
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

        [Test]
        public void BasketAddPromotion_ExistingPromotion_ThrowException()
        {
            Basket basket = new Basket();
            basket.AddPromotion(new Promotion("A", "3 for 40", (quantity, unitPrice) => 0 ));

            Assert.Throws<ArgumentException>(() =>
            {
                basket.AddPromotion(new Promotion("A", "3 for 40", (quantity, unitPrice) => 0));
            });
        }

        [Test]
        public void BasketTotalCost_ItemsAddedNoPromotion_CorrectTotalCost()
        {
            Basket basket = new Basket();
            basket.AddItem(new Item("A", 10));
            basket.AddItem(new Item("B", 15));
            basket.AddItem(new Item("C", 40));
            basket.AddItem(new Item("D", 55));
            
            Assert.AreEqual(120m, basket.GetTotalCost());
        }

        [Test]
        public void BasketTotalCost_ItemsAddedWithPromotion_CorrectTotalCost()
        {
            Basket basket = new Basket();
            basket.AddItem(new Item("B", 15));
            basket.AddItem(new Item("B", 15));
            basket.AddItem(new Item("B", 15));
            basket.AddItem(new Item("B", 15));
            basket.AddItem(new Item("B", 15));
            basket.AddItem(new Item("D", 55));
            basket.AddItem(new Item("D", 55));
            basket.AddItem(new Item("D", 55));
            
            basket.AddPromotion(new Promotion("B", "3 for 40", (quantity, unitPrice) =>
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
            
            
            basket.AddPromotion(new Promotion("D", "25% off for every 2 purchased together", (quantity, unitPrice) =>
            {
                var total = 0.0m;
                while (quantity >= 2)
                {
                    total += ((2 * unitPrice) * 0.75m);
                    quantity -= 2;
                }
            
                total += quantity * unitPrice;
                return total;
            }));
            
            Assert.AreEqual(207.5m, basket.GetTotalCost());
        }
    }
}