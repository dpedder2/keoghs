using System;
using KeoghsCheckout.Core;
using NUnit.Framework;

namespace KeoghsCheckout.Tests
{
    [TestFixture]
    public class BasketTests
    {
        private Item _itemA;
        private Item _itemB;
        private Item _itemC;
        private Item _itemD;
        private Basket _basket;
        private Promotion _promotionThreeForFortySKUB;
        private Promotion _promotionTwentyFivePercentOffEveryTwoSKUD;

        [SetUp]
        public void Setup()
        {
            _basket = new Basket();
            _itemA = new Item("A", 10);
            _itemB = new Item("B", 15);
            _itemC = new Item("C", 40);
            _itemD = new Item("D", 55);
            
            _promotionThreeForFortySKUB = new Promotion("B", "3 for 40", (quantity, unitPrice) =>
            {
                var total = 0.0m;
                while (quantity >= 3)
                {
                    total += 40;
                    quantity -= 3;
                }

                total += quantity * unitPrice;
                return total;
            });

            _promotionTwentyFivePercentOffEveryTwoSKUD = new Promotion("D", "25% off for every 2 purchased together",
                (quantity, unitPrice) =>
                {
                    var total = 0.0m;
                    while (quantity >= 2)
                    {
                        total += ((2 * unitPrice) * 0.75m);
                        quantity -= 2;
                    }

                    total += quantity * unitPrice;
                    return total;
                });
        }
        
        [Test]
        public void BasketAddItem_Item_AddsItem()
        {
            _basket.AddItem(_itemA);
            
            var item = _basket.GetItem("A");
            
            Assert.IsNotNull(item);
            Assert.AreEqual("A", item.SKU);
        }

        [Test]
        public void BasketAddItem_ExistingItem_IncrementLineQuantity()
        {
            _basket.AddItem(_itemA);
            _basket.AddItem(_itemA);
            
            var lineItem = _basket.GetLineItem("A");
            
            Assert.AreEqual(2, lineItem.Quantity);
        }

        [Test]
        public void BasketAddPromotion_Promotion_AddPromotion()
        {
            _basket.AddPromotion(_promotionThreeForFortySKUB);

            var promo = _basket.GetPromotion("B");
            
            Assert.IsNotNull(promo);
            Assert.AreEqual(promo.SKU, "B");
            Assert.AreEqual(promo.Description, "3 for 40");
        }

        [Test]
        public void BasketAddPromotion_ExistingPromotion_ThrowException()
        {
            _basket.AddPromotion(_promotionThreeForFortySKUB);

            Assert.Throws<ArgumentException>(() =>
            {
                _basket.AddPromotion(_promotionThreeForFortySKUB);
            });
        }

        [Test]
        public void BasketTotalCost_ItemsAddedNoPromotion_CorrectTotalCost()
        {
            _basket.AddItem(_itemA);
            _basket.AddItem(_itemB);
            _basket.AddItem(_itemC);
            _basket.AddItem(_itemD);
            
            Assert.AreEqual(120m, _basket.GetTotalCost());
        }

        [Test]
        public void BasketTotalCost_ItemsAddedWithPromotion_CorrectTotalCost()
        {
            _basket.AddItem(_itemB);
            _basket.AddItem(_itemB);
            _basket.AddItem(_itemB);
            _basket.AddItem(_itemB);
            _basket.AddItem(_itemB);
            _basket.AddItem(_itemD);
            _basket.AddItem(_itemD);
            _basket.AddItem(_itemD);
            
            _basket.AddPromotion(_promotionThreeForFortySKUB);
            _basket.AddPromotion(_promotionTwentyFivePercentOffEveryTwoSKUD);
            
            Assert.AreEqual(207.5m, _basket.GetTotalCost());
        }

        [Test]
        public void BasketTotalCost_ItemsAddedWithAndWithoutPromotions_CorrectTotalCost()
        {
            _basket.AddItem(_itemA);
            _basket.AddItem(_itemA);
            _basket.AddItem(_itemB);
            _basket.AddItem(_itemB);
            _basket.AddItem(_itemB);
            _basket.AddItem(_itemB);
            _basket.AddItem(_itemB);
            _basket.AddItem(_itemB);
            _basket.AddItem(_itemB);
            _basket.AddItem(_itemC);
            _basket.AddItem(_itemC);
            _basket.AddItem(_itemD);
            _basket.AddItem(_itemD);
            _basket.AddItem(_itemD);
            _basket.AddItem(_itemD);
            _basket.AddItem(_itemD);
            
            _basket.AddPromotion(_promotionThreeForFortySKUB);
            _basket.AddPromotion(_promotionTwentyFivePercentOffEveryTwoSKUD);
            
            Assert.AreEqual(415m, _basket.GetTotalCost());
        }
    }
}