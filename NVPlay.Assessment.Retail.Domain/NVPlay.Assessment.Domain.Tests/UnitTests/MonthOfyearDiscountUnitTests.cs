using NVPlay.Assessment.Retail.Domain.Models;

namespace NVPlay.Assessment.Domain.Tests.UnitTests
{
    [TestClass]
    public class MonthOfyearDiscountUnitTests
    {
        [TestMethod]
        public void DiscountAppliedForOverOrderInJanuary()
        {
            var mnthOfyearDiscount = new CalculateMonthOfYearDiscounts(2.0m, 1);

            var order = CreateOrderModelForJanuary();

            Assert.AreEqual(order.CurrentDiscount, 0.0m);
            Assert.AreEqual(order.OrderTotal, 300.0m);
            Assert.AreEqual(order.OrderDate.Month, 1);

            var discount = mnthOfyearDiscount.CalculateDiscount(order);

            Assert.AreEqual(discount, 6.0m);
        }

        [TestMethod]
        public void NoDiscountAppliedForOrderInApril()
        {
            var orderOverDiscount = new CalculateOrdersOverDiscounts(5.0m);

            var order = CreateOrderModelForApril();

            Assert.AreEqual(order.CurrentDiscount, 0.0m);
            Assert.AreEqual(order.OrderTotal, 300.0m);
            Assert.AreEqual(order.OrderDate.Month, 4);

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


        private OrderModel CreateOrderModelForJanuary()
        {
            var order = new OrderModel(new CustomerModel(true), new DateTime(2022, 1, 20));

            order.AddOrderItems(3, new OrderItemModel("test 1", "non clearance item", 100, false));

            return order;
        }

        private OrderModel CreateOrderModelForApril()
        {
            var order = new OrderModel(new CustomerModel(true), new DateTime(2022, 4, 10));

            order.AddOrderItems(3, new OrderItemModel("test 1", "non clearance item", 100, false));

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