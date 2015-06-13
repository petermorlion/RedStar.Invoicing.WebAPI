using Microsoft.AspNet.Mvc;
using System.IO;
using RedStar.Invoicing.Models;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNet.Authorization;

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
        public void Post([FromBody]InvoiceDTO invoice)
        {
            
        }
    }
}
