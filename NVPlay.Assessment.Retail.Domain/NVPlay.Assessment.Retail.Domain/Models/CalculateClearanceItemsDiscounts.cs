using NVPlay.Assessment.Retail.Domain.Interfaces;

namespace NVPlay.Assessment.Retail.Domain.Models
{
    public class CalculateClearanceItemsDiscounts: ICalculateDiscountCommand
    {
        private decimal _discount;

        public CalculateClearanceItemsDiscounts(decimal discount)
        {
            _discount = discount;
        }

        public decimal CalculateDiscount(OrderModel orderModel)
        {
            var totalDiscount = 0.0m;
            foreach (var line in orderModel.OrderItemLines)
            {
                if (line.Item.IsClearance)
                {
                    totalDiscount += line.LineTotal * (decimal)(_discount/100);
                }
            }

            return totalDiscount;
        }
    }
}
