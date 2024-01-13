using NVPlay.Assessment.Retail.Domain.Interfaces;

namespace NVPlay.Assessment.Retail.Domain.Models
{
    public class CalculateMonthOfYearDiscounts: ICalculateDiscountCommand
    {
        private decimal _discount;
        private int _discountMonth;
        

        public CalculateMonthOfYearDiscounts(decimal discount, int month)
        {
            _discount = discount;
            _discountMonth = month;
        }

        public decimal CalculateDiscount(OrderModel orderModel)
        {
            var totalDiscount = 0.0m;

            if(orderModel.OrderDate.Month == _discountMonth)
            {
                totalDiscount = (orderModel.OrderTotal - orderModel.CurrentDiscount) * (_discount / 100);
            }

            return totalDiscount;
        }
    }
}
