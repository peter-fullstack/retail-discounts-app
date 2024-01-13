using NVPlay.Assessment.Retail.Domain.Models;

namespace NVPlay.Assessment.Domain.Tests.UnitTests
{
    [TestClass]
    public class OrdersOverDiscountUnitTests
    {
        [TestMethod]
        public void DiscountAppliedForOver500Dollars()
        {
            var orderOverDiscount = new CalculateOrdersOverDiscounts(5.0m);

            var order = CreateOrderModelWithTotalOf600();

            Assert.AreEqual(order.CurrentDiscount, 0.0m);
            Assert.AreEqual(order.OrderTotal, 600.0m);

            var discount = orderOverDiscount.CalculateDiscount(order);

            Assert.AreEqual(discount, 30.0m);
        }

        [TestMethod]
        public void NoDiscountAppliedForOrderLessThan500()
        {
            var orderOverDiscount = new CalculateOrdersOverDiscounts(5.0m);

            var order = CreateOrderModelWithTotalOf400();

            Assert.AreEqual(order.CurrentDiscount, 0.0m);
            Assert.AreEqual(order.OrderTotal, 400.0m);

            var discount = orderOverDiscount.CalculateDiscount(order);

            Assert.AreEqual(discount, 0.0m);
        }

        [TestMethod]
        public void DiscountAppliedForOrderOf550WithExistingDiscountOf10Percent()
        {
            var orderOverDiscount = new CalculateOrdersOverDiscounts(5.0m);

            var order = CreateOrderModelWithTotalOf550AndExistingDiscountOf10Percent();

            Assert.AreEqual(order.CurrentDiscount, 55.0m);
            Assert.AreEqual(order.OrderTotal, 550.0m);

            var discount = orderOverDiscount.CalculateDiscount(order);

            Assert.AreEqual(discount, 24.75m);
        }


        private OrderModel CreateOrderModelWithTotalOf600()
        {
            var order = new OrderModel(new CustomerModel(true), DateTime.Now);

            order.AddOrderItems(3, new OrderItemModel("test 1", "non clearance item", 200, false));

            return order;
        }

        private OrderModel CreateOrderModelWithTotalOf550AndExistingDiscountOf10Percent()
        {
            var order = new OrderModel(new CustomerModel(false), DateTime.Now);

            order.AddDiscount(55);

            order.AddOrderItems(1, new OrderItemModel("test 2", "non clearance item", 550, false));

            return order;
        }

        private OrderModel CreateOrderModelWithTotalOf400()
        {
            var order = new OrderModel(new CustomerModel(true), DateTime.Now);

           order.AddOrderItems(4, new OrderItemModel("test 3", "non clearance item", 100, true));

            return order;
        }
    }
}