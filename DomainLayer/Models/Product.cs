using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Product : BaseEntity<int>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = [];
    }
}
