using Microsoft.AspNet.Identity;
using RedStar.Invoicing.Web._4._6.DataAccess;
using RedStar.Invoicing.Web._4._6.Models;
using System.Net.Http;
using System.Web.Http;

namespace RedStar.Invoicing.Web._4._6.Controllers
{
    [RoutePrefix("api/invoicegenerator")]
    [Authorize]
    public class InvoiceGeneratorController : ApiController
    {
        public async System.Threading.Tasks.Task<InvoiceGeneratorDTO> Get()
        {
            var query = new UserSettingsQuery();
            var userSettings = await query.Execute(User.Identity.GetUserId());

            if (!userSettings.HasValue)
            {
                throw new HttpResponseException(new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest));
            }

            return new InvoiceGeneratorDTO
            {
                InvoiceTemplate = userSettings.Value.InvoiceTemplate,
                LogoUrl = userSettings.Value.LogoUrl
            };
        }
    }
}