using NVPlay.Assessment.Retail.Domain.Interfaces;

namespace NVPlay.Assessment.Retail.Domain.Models
{
    public class CalculateOrdersOverDiscounts: ICalculateDiscountCommand
    {
        private decimal _discount;

        public CalculateOrdersOverDiscounts(decimal discount)
        {
            _discount = discount;
        }

        public decimal CalculateDiscount(OrderModel orderModel)
        {
            var totalDiscount = 0.0m;

            if (orderModel.OrderTotal > 500)
            {
                totalDiscount = (orderModel.OrderTotal - orderModel.CurrentDiscount) * (_discount / 100);
            }
            return totalDiscount;
        }
    }
}
