using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using ServiceAbstraction;
using Shared.DTO_S;

namespace Services
{
    internal class InvoiceService(IUnitOfWork unitOfWork, IMapper mapper) : IInvoiceService
    {
        public async Task<IEnumerable<InvoiceToReturnDto>> GetAllInvoicesAsync()
        {
            var InvoiceRepo = await unitOfWork.GetRepository<Invoice, int>().GetAllAsync();

            return InvoiceRepo.Select(InvoiceRepo => mapper.Map<InvoiceToReturnDto>(InvoiceRepo));
        }

        public async Task<InvoiceToReturnDto> GetInvoiceByIdAsync(int invoiceId)
        {
            var InvoiceRepo = await unitOfWork.GetRepository<Invoice, int>().GetByIdAsync(invoiceId);

            if (InvoiceRepo == null)
                throw new Exception("Invoice not found");

            return mapper.Map<InvoiceToReturnDto>(InvoiceRepo);
        }
    }
}
