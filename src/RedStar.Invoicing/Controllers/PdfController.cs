using Microsoft.AspNet.Mvc;

namespace RedStar.Invoicing.Controllers
{
    [Route("api/[controller]")]
    public class PdfController : Controller
    {
        // POST api/values
        [HttpPost]
        public string Post([FromBody]string value)
        {
            return "works!";
        }
    }
}
