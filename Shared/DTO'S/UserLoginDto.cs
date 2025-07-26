using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO_S
{
    public class UserLoginDto
    {
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
