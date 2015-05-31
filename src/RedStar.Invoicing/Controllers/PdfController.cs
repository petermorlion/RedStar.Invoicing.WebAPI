using Microsoft.AspNet.Mvc;

namespace RedStar.Invoicing.Controllers
{
    [Route("api/[controller]")]
    public class PdfController : Controller
    {
        // POST api/values
        [HttpPost]
        public string Post([FromBody]Invoice invoice)
        {
            return "works!";
        }
    }

    public class Invoice
    {
        public string Html { get; set; }
    }
}
