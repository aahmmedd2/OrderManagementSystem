using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Enums;

namespace DomainLayer.Models
{
    public class Order : BaseEntity<Guid>
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = default!;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = [];
        public PaymentMethod PaymentMethod { get; set; }
        public OrderStatus Status { get; set; }
        public ICollection<Invoice> Invoices { get; set; } = [];
    }
}
