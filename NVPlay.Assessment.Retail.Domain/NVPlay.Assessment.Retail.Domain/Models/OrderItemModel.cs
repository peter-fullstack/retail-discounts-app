using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVPlay.Assessment.Retail.Domain.Models
{
    public class OrderItemModel
    {
        public OrderItemModel(
            string name,
            string description,
            decimal price,
            bool clearance) 
        { 
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            UnitPrice = price;
            IsClearance = clearance;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsClearance { get; set; } = false;
    }
}
