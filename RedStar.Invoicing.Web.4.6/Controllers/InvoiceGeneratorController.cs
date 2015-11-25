using RedStar.Invoicing.Web._4._6.Models;
using System.Web;
using System.Web.Http;

namespace RedStar.Invoicing.Web._4._6.Controllers
{
    [RoutePrefix("api/invoicegenerator")]
    [Authorize]
    public class InvoiceGeneratorController : ApiController
    {
        public InvoiceGeneratorDTO Get()
        {
            return new InvoiceGeneratorDTO
            {
                InvoiceTemplate = "<strong>strong!</strong>",
                LogoUrl = "test.png"
            };
        }

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}