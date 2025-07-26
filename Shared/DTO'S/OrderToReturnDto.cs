using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO_S
{
    public class OrderToReturnDto
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime OrderDate { get; set; }
        public string PaymentMethod { get; set; } = default!;
        public string Status { get; set; } = default!;
        public decimal TotalAmount { get; set; }
        public List<OrderItemDto> Items { get; set; } = [];
    }
}
