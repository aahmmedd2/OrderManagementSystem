using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTO_S;

namespace ServiceAbstraction
{
    public interface IInvoiceService
    {
        Task<InvoiceToReturnDto> GetInvoiceByIdAsync(int invoiceId);
        Task<IEnumerable<InvoiceToReturnDto>> GetAllInvoicesAsync();
    }
}
