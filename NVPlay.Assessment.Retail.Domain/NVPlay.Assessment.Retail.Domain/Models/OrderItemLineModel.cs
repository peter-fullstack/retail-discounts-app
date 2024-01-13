namespace NVPlay.Assessment.Retail.Domain.Models
{
    public class OrderItemLineModel
    {
        public int Quantity { get; set; }

        public OrderItemModel Item { get; set; }

        private List<decimal> _discounts = new List<decimal>();

        public void AddDiscount(decimal discount)
        {
            _discounts.Add(discount);
        }
        public decimal LineTotal 
        {
            get
            { 
                var total = Item.UnitPrice * Quantity;

                return total - DiscountAmount();
            }
        }

        public decimal DiscountAmount()
        {
            var lineTotal = Item.UnitPrice * Quantity;

            var discountAmount = 0.0m;
            foreach(var discount in  _discounts)
            {
                discountAmount += lineTotal * discount;
            }

            return discountAmount;
        }
    }
}
