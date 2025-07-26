using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO_S
{
    public class InvoiceToReturnDto
    {
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string OrderStatus { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public ICollection<InvoiceOrderItemDto> Items { get; set; } = [];
        public decimal TotalAmount { get; set; }
    }
}
