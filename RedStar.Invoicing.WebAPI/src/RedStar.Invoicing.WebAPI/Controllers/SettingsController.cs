using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using RedStar.Invoicing.WebAPI.DataContracts;
using RedStar.Invoicing.Queries;

namespace RedStar.Invoicing.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class SettingsController : Controller
    {
        private readonly IUserSettingsQuery _userSettingsQuery;

        public SettingsController(IUserSettingsQuery userSettingsQuery)
        {
            _userSettingsQuery = userSettingsQuery;
        }

        // GET: api/values
        [HttpGet]
        public async Task<Settings> Get()
        {
            var userSettings = await _userSettingsQuery.Execute("1");
            if (!userSettings.HasValue)
            {
                return new Settings
                {
                    InvoiceTemplate = "",
                    LogoUrl = ""
                };
            }

            return new Settings
            {
                InvoiceTemplate = userSettings.Value.InvoiceTemplate,
                LogoUrl = userSettings.Value.LogoUrl
            };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}
