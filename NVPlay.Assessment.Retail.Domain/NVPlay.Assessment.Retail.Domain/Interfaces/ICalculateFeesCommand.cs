using NVPlay.Assessment.Retail.Domain.Models;
namespace NVPlay.Assessment.Retail.Domain.Interfaces
{
    public interface ICalculateFeesCommand
    {
        decimal CalculateFees(ICustomer customer, List<OrderItemLineModel> orderItemLines);
    }
}
