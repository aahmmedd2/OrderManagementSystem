using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO_S
{
    public class OrderDto
    {
        public string PaymentMethod { get; set; } = default!;
        public string Status { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } = [];
    }
}
