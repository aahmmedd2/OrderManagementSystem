using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO_S
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal SubTotal { get; set; }
    }
}
