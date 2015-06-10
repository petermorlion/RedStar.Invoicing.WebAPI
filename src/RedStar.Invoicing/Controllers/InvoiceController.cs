using Microsoft.AspNet.Mvc;
using System.IO;
using RedStar.Invoicing.Models;

namespace RedStar.Invoicing.Controllers
{
    [Route("api/[controller]")]
    public class InvoiceController : Controller
    {
        // POST api/values
        [HttpPost]
        public void Post([FromBody]InvoiceDTO invoice)
        {
            
        }
    }
}
