using KeoghsCheckout.Core;
using NUnit.Framework;

namespace KeoghsCheckout.Tests
{
    [TestFixture]
    public class BasketTests
    {
        /*
         * BasketAddPromotion_Promotion_AddPromotion
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
    }
}