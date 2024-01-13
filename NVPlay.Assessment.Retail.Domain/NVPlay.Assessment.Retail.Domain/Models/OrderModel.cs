using NVPlay.Assessment.Retail.Domain.Interfaces;

namespace NVPlay.Assessment.Retail.Domain.Models
{
    public class OrderModel
    {
        private ICustomer _customer;

        private DateTime _orderDate;

        public OrderModel(ICustomer customer, DateTime date)
        {
            _customer = customer;
            _orderDate = date;
            _orderLineItems = new List<OrderItemLineModel>();
            _orderDiscounts = new List<decimal>();
        }

        private List<OrderItemLineModel> _orderLineItems;

        private List<decimal> _orderDiscounts;

        public void AddDiscount(decimal discount)
        {
            _orderDiscounts.Add(discount);
        }

        public DateTime OrderDate
        {
            get
            {
                return _orderDate;
            }
        }

        public decimal CurrentDiscount
        {
            get
            {
               return  _orderDiscounts.Sum();
            }
        }

        public ICustomer OrderCustomer 
        { 
            get 
            { 
                return _customer; 
            } 
        }

        public List<OrderItemLineModel> OrderItemLines
        {
            get 
            { 
                return _orderLineItems; 
            }
        }

        public decimal OrderTotal
        {
            get
            {
                var orderTotal = 0.0m;

                foreach (var item in _orderLineItems)
                {
                    orderTotal += item.LineTotal;
                }

                return orderTotal;
            }
        }

        public decimal ClearanceItemsOrderTotal
        {
            get
            {
                var orderTotal = 0.0m;

                foreach (var orderLineItem in _orderLineItems)
                {
                    if (orderLineItem.Item.IsClearance)
                    {
                        orderTotal += orderLineItem.LineTotal;
                    }
                }

                return orderTotal;
            }
        }

        public decimal NonClearanceItemsOrderTotal
        {
            get
            {
                var orderTotal = 0.0m;

                foreach (var orderLineItem in _orderLineItems)
                {
                    if (!orderLineItem.Item.IsClearance)
                    {
                        orderTotal += orderLineItem.LineTotal;
                    }
                }

                return orderTotal;
            }
        }

        public void AddOrderItems(int qty, OrderItemModel item)
        {
            if(qty <= 0)
            {
                return;
            }

            var addItems = _orderLineItems.Where(x => x.Item.Id == item.Id).FirstOrDefault();

            if (addItems != null)
            {
                addItems.Quantity += qty;
            }
            else
            {
                _orderLineItems.Add(new OrderItemLineModel
                {
                    Quantity = qty,
                    Item = item
                });
            }
        }

        public void RemoveOrderItem(int qty, OrderItemModel item)
        {
            if (qty <= 0)
            {
                return;
            }

            var removeItems = _orderLineItems.Where(x => x.Item.Id == item.Id).FirstOrDefault();

            if(removeItems != null)
            {
                if(qty >= removeItems.Quantity)
                {
                    removeItems.Quantity = 0;
                }
                else
                {
                    removeItems.Quantity -= qty;
                }
            } 
        }

       
    }
}
