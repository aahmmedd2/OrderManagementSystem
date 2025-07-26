using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTO_S;

namespace Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[Controller]")]
    public class InvoicesController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet] // GET /api/invoices
        public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetAllInvoices()
        {
            var Invoices = await _serviceManager.InvoiceService.GetAllInvoicesAsync();
            
            return Ok(Invoices);
        }

        [HttpGet("{id}")] // GET /api/invoices/{invoiceId}
        public async Task<ActionResult<InvoiceDto>> GetInvoiceById(int id)
        {
            var invoice = await _serviceManager.InvoiceService.GetInvoiceByIdAsync(id);
            
            return invoice is null ? NotFound("Invoice not found.") : Ok(invoice);
        }
    }
}
