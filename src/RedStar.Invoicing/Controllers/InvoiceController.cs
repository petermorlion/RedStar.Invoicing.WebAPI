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

        [HttpGet]
        public InvoiceGenerationDTO Get()
        {
            var userId = Context.User.GetUserId();
            var userSetting = _invoicesDbContext.UserSettings.SingleOrDefault(x => x.UserId.ToString() == userId);

            return new InvoiceGenerationDTO
            {
                InvoiceTemplate = userSetting.InvoiceTemplate,
                LogoUrl = userSetting.LogoUrl
            };
        }

        [HttpPost]
        public void Post([FromBody]InvoiceDTO invoice)
        {
            
        }
    }
}
