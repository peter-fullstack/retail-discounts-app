using NVPlay.Assessment.Retail.Domain.Models;

namespace NVPlay.Assessment.Domain.Tests.UnitTests
{
    [TestClass]
    public class ClubMemberDiscountUnitTests
    {
        [TestMethod]
        public void DiscountAppliedForClubMemberOnNonClearanceItems()
        {
            var clubMemberDiscount = new CalculateClubMemberDiscounts(10.0m);

            var order = CreateOrderModelForClubMemberAndNonClearanceItems();

            Assert.AreEqual(order.CurrentDiscount, 0.0m);
            Assert.AreEqual(order.OrderTotal, 200.0m);
            Assert.AreEqual(order.NonClearanceItemsOrderTotal, 200.0m);

            var discount = clubMemberDiscount.CalculateDiscount(order);

            Assert.AreEqual(discount, 20.0m);
        }

        [TestMethod]
        public void NoDiscountAppliedForClubMemberOnClearanceItems()
        {
            var clubMemberDiscount = new CalculateClubMemberDiscounts(5.0m);

            var order = CreateOrderModelForClubMemberAndClearanceItems();

            Assert.AreEqual(order.CurrentDiscount, 0.0m);
            Assert.AreEqual(order.OrderTotal, 200.0m);
            Assert.AreEqual(order.ClearanceItemsOrderTotal, 200.0m);

            var discount = clubMemberDiscount.CalculateDiscount(order);

            Assert.AreEqual(discount, 0.0m);
        }

        [TestMethod]
        public void NoDiscountAppliedForNonClubMemberAndClearanceItems()
        {
            var clearanceItemsDiscount = new CalculateClubMemberDiscounts(5.0m);

            var order = CreateOrderModelForNonClubMemberAndClearanceItems();

            Assert.AreEqual(order.CurrentDiscount, 0.0m);
            Assert.AreEqual(order.OrderTotal, 200.0m);
            Assert.AreEqual(order.ClearanceItemsOrderTotal, 200.0m);

            var discount = clearanceItemsDiscount.CalculateDiscount(order);

            Assert.AreEqual(discount, 0.0m);
        }


        [TestMethod]
        public void DiscountAppliedForClubMemberOnClearanceItemsOnly()
        {
            var clearanceItemsDiscount = new CalculateClubMemberDiscounts(10.0m);

            var order = CreateOrderModelForClubMemberWithClearanceAndNonClearanceItems();

            Assert.AreEqual(order.OrderTotal, 300.0m);
            Assert.AreEqual(order.NonClearanceItemsOrderTotal, 100.0m);

            var discount = clearanceItemsDiscount.CalculateDiscount(order);

            Assert.AreEqual(discount, 10.0m);
        }

        [TestMethod]
        public void ExistingDiscountAppliedForNonClubMember()
        {
            var clearanceItemsDiscount = new CalculateClubMemberDiscounts(5.0m);

            var order = CreateOrderModelWithExistingDiscountlForNonClubMember();

            Assert.AreEqual(order.CurrentDiscount, 25.0m);
            Assert.AreEqual(order.OrderTotal, 100.0m);

            var discount = clearanceItemsDiscount.CalculateDiscount(order);

            Assert.AreEqual(discount, 25.0m);
        }

        private OrderModel CreateOrderModelForClubMemberAndNonClearanceItems()
        {
            var order = new OrderModel(new CustomerModel(true), DateTime.Now);

            order.AddOrderItems(2, new OrderItemModel("test 1", "non clearance item", 100, false));

            return order;
        }

        private OrderModel CreateOrderModelForNonClubMemberAndClearanceItems()
        {
            var order = new OrderModel(new CustomerModel(false), DateTime.Now);

            order.AddOrderItems(2, new OrderItemModel("test 2", "clearance item", 100, true));

            return order;
        }

        private OrderModel CreateOrderModelForClubMemberAndClearanceItems()
        {
            var order = new OrderModel(new CustomerModel(true), DateTime.Now);

           order.AddOrderItems(2, new OrderItemModel("test 3", "clearance item", 100, true));

            return order;
        }

        private OrderModel CreateOrderModelWithExistingDiscountlForClubMember()
        {
            var order = new OrderModel(new CustomerModel(true), DateTime.Now);

            order.AddDiscount(25.0m);

            order.AddOrderItems(1, new OrderItemModel("test 4", "non clearance item", 125, false));

            return order;
        }

        private OrderModel CreateOrderModelWithExistingDiscountlForNonClubMember()
        {
            var order = new OrderModel(new CustomerModel(true), DateTime.Now);

            order.AddDiscount(25.0m);

            order.AddOrderItems(1, new OrderItemModel("test 5", "non clearance item", 100, false));

            return order;
        }

        private OrderModel CreateOrderModelForClubMemberWithClearanceAndNonClearanceItems()
        {
            var order = new OrderModel(new CustomerModel(true), DateTime.Now);

            order.AddOrderItems(1, new OrderItemModel("test 6", "non clearance item", 100, false));

            order.AddOrderItems(2, new OrderItemModel("test 7", "clearance item", 100, true));

            return order;
        }
    }
}