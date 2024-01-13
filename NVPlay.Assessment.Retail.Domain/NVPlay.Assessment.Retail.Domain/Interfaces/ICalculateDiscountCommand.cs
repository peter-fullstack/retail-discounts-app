using NVPlay.Assessment.Retail.Domain.Models;
namespace NVPlay.Assessment.Retail.Domain.Interfaces
{
    public interface ICalculateDiscountCommand
    {
        decimal CalculateDiscount(OrderModel orderModel);
    }
}
