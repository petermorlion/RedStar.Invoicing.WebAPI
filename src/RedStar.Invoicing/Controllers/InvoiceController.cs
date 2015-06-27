using Microsoft.AspNet.Mvc;
using RedStar.Invoicing.Models;
using System.Security.Claims;
using Microsoft.AspNet.Authorization;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using System.Net.Http;

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
        public async Task<IActionResult> Post([FromBody]InvoiceDTO invoiceDto)
        {
            var generatedInvoice = new Invoice
            {
                UserId = Context.User.GetUserId(),
                Html = invoiceDto.Html
            };

            _invoicesDbContext.Invoices.Add(generatedInvoice);
            await _invoicesDbContext.SaveChangesAsync();

            //var httpWebrequest = new HttpClient();

            var id = generatedInvoice.Id;

            return new HttpStatusCodeResult(200);
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            var invoice = _invoicesDbContext.Invoices.Single(x => x.Id == id);
            return new ContentResult { Content = invoice.Html };
        }
    }
}
