using NVPlay.Assessment.Retail.Domain.Interfaces;

namespace NVPlay.Assessment.Retail.Domain.Models
{
    public class CalculateClubMemberDiscounts : ICalculateDiscountCommand
    {
        private decimal _discount;

        public CalculateClubMemberDiscounts(decimal discount)
        {
            _discount = discount;
        }

        public decimal CalculateDiscount(OrderModel orderModel)
        {
            var totalDiscount = 0.0m;

            if (orderModel.OrderCustomer.IsClubMember())
            {
                if (orderModel.NonClearanceItemsOrderTotal > 0)
                {
                    totalDiscount = orderModel.NonClearanceItemsOrderTotal * (_discount / 100);
                }
            }
            
            return totalDiscount;
        }
    }
}
