using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Enums;

namespace DomainLayer.Models
{
    public class User : BaseEntity<int>
    {
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; } = default!;
        [Required]
        public string PasswordHash { get; set; } = default!;
        public Roles Role { get; set; }
    }
}
