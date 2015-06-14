using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Security.Claims;
using RedStar.Invoicing.Models;
using Microsoft.AspNet.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RedStar.Invoicing.Controllers
{
    public class InvoiceGeneratorController : Controller
    {
        private InvoicesDbContext _invoicesDbContext;

        public InvoiceGeneratorController(InvoicesDbContext invoicesDbContext)
        {
            _invoicesDbContext = invoicesDbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var userId = Context.User.GetUserId();
            var userSetting = _invoicesDbContext.UserSettings.SingleOrDefault(x => x.UserId.ToString() == userId);

            var viewModel = new InvoiceGeneratorViewModel();
            viewModel.InvoiceTemplate = new HtmlString(userSetting.InvoiceTemplate);
            viewModel.LogoUrl = userSetting.LogoUrl;

            return View(viewModel);
        }

        public string LogoUrl()
        {
            var userId = Context.User.GetUserId();
            var userSetting = _invoicesDbContext.UserSettings.SingleOrDefault(x => x.UserId.ToString() == userId);

            return userSetting.LogoUrl;
        }
    }
}
