using Microsoft.AspNet.Mvc;
using System.IO;
using RedStar.Invoicing.Models;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNet.Authorization;
using System.Threading.Tasks;

namespace RedStar.Invoicing.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class InvoiceController : Controller
    {
        private InvoicesDbContext _invoicesDbContext;

        public InvoiceController(InvoicesDbContext invoicesDbContext)
        {
            _invoicesDbContext = invoicesDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]InvoiceDTO invoice)
        {
            var generatedInvoice = new Invoice
            {
                UserId = Context.User.GetUserId(),
                Html = invoice.Html
            };

            _invoicesDbContext.Invoices.Add(generatedInvoice);
            await _invoicesDbContext.SaveChangesAsync();

            return new HttpStatusCodeResult(200);
        }
    }
}
