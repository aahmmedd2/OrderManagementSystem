using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Enums
{
    public enum OrderStatus
    {
        Pending = 1,
        PaymentReceived = 2,
        PaymentFailed = 3,
    }
}
