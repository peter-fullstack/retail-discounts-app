using NVPlay.Assessment.Retail.Domain.Interfaces;
using NVPlay.Assessment.Retail.Domain.Models;

namespace NVPlay.Assessment.Retail.Domain
{
    public class OrderProcessor
    {
        private List<ICalculateDiscountCommand> _discountCommands;
        private List<ICalculateFeesCommand> _feesCommands;

        public OrderProcessor()
        {
            _discountCommands = new List<ICalculateDiscountCommand>();
            _feesCommands = new List<ICalculateFeesCommand>();
        }

        public void AddDiscountCommand(ICalculateDiscountCommand calculateDiscountCommand)
        {
            _discountCommands.Add(calculateDiscountCommand);
        }

        public void AddFeesCommand(ICalculateFeesCommand calculateFeesCommand)
        {
            _feesCommands.Add(calculateFeesCommand);
        }

        public decimal CalculateOrderTotal(OrderModel order)
        {
            decimal total = 0;

            foreach (var command in _discountCommands)
            {
                command.CalculateDiscount(order);
            }

            return order.OrderTotal;
        }
    }
}