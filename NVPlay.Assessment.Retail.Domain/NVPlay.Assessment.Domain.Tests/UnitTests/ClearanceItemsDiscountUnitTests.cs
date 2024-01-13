using NVPlay.Assessment.Retail.Domain.Models;

namespace NVPlay.Assessment.Domain.Tests.UnitTests
{
    [TestClass]
    public class ClearanceItemsDiscountUnitTests
    {
        [TestMethod]
        public void DiscountAppliedToClearanceItems()
        {
            var clearanceItemsDiscount = new CalculateClearanceItemsDiscounts(10.0m);

            var order = CreateOrderModelWithClearancetems();

            Assert.AreEqual(order.OrderTotal, 200.0m);
            Assert.AreEqual(order.NonClearanceItemsOrderTotal, 0.0m);
            Assert.AreEqual(order.ClearanceItemsOrderTotal, 200.0m);

            var discount = clearanceItemsDiscount.CalculateDiscount(order);

            Assert.AreEqual(discount, 20.0m);
        }

        [TestMethod]
        public void NoDiscountAppliedForNonClearanceItems()
        {
            var clearanceItemsDiscount = new CalculateClearanceItemsDiscounts(10.0m);

            var order = CreateOrderModelWithNonClearancetems();

            Assert.AreEqual(order.OrderTotal, 200.0m);
            Assert.AreEqual(order.NonClearanceItemsOrderTotal, 200.0m);
            Assert.AreEqual(order.ClearanceItemsOrderTotal, 0.0m);

            var discount = clearanceItemsDiscount.CalculateDiscount(order);

            Assert.AreEqual(discount, 0.0m);
        }

        [TestMethod]
        public void DiscountAppliedForMixOfClearanceAndNonClearanceItems()
        {
            var clearanceItemsDiscount = new CalculateClearanceItemsDiscounts(10.0m);

            var order = CreateOrderModelWithClearanceAndNonClearancetems();

            Assert.AreEqual(order.OrderTotal, 190.0m);
            Assert.AreEqual(order.NonClearanceItemsOrderTotal, 40.0m);
            Assert.AreEqual(order.ClearanceItemsOrderTotal, 150.0m);

            var discount = clearanceItemsDiscount.CalculateDiscount(order);

            Assert.AreEqual(discount, 15.0m);
        }

        private OrderModel CreateOrderModelWithClearancetems()
        {
            var order = new OrderModel(new CustomerModel(false), DateTime.Now);

            order.AddOrderItems(2, new OrderItemModel("test 1", "clearance item", 100, true));

            return order;
        }

        private OrderModel CreateOrderModelWithNonClearancetems()
        {
            var order = new OrderModel(new CustomerModel(false), DateTime.Now);

            order.AddOrderItems(2, new OrderItemModel("test 1", "non clearance item", 100, false));

            return order;
        }

        private OrderModel CreateOrderModelWithClearanceAndNonClearancetems()
        {
            var order = new OrderModel(new CustomerModel(false), DateTime.Now);

            order.AddOrderItems(2, new OrderItemModel("test 1", "non clearance item", 20, false));

            order.AddOrderItems(3, new OrderItemModel("test 2", "clearance item", 50, true));

            return order;
        }
    }
}