using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using RedStar.Invoicing.Commands;
using RedStar.Invoicing.Domain;
using RedStar.Invoicing.WebAPI.DataContracts;
using RedStar.Invoicing.Queries;

namespace RedStar.Invoicing.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class SettingsController : Controller
    {
        private readonly IGetUserSettingsQuery _getUserSettingsQuery;
        private readonly IPersistUserSettingsCommand _persistUserSettingsCommand;

        public SettingsController(IGetUserSettingsQuery getUserSettingsQuery, IPersistUserSettingsCommand persistUserSettingsCommand)
        {
            _getUserSettingsQuery = getUserSettingsQuery;
            _persistUserSettingsCommand = persistUserSettingsCommand;
        }

        // GET: api/values
        [HttpGet]
        public async Task<Settings> Get()
        {
            var userSettings = await _getUserSettingsQuery.Execute("1");
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
        public async Task<IActionResult> Post([FromBody]Settings value)
        {
            if (value == null)
            {
                return this.HttpBadRequest();
            }

            var userSettings = new UserSettings
            {
                LogoUrl = value.LogoUrl,
                InvoiceTemplate = value.InvoiceTemplate
            };

            await _persistUserSettingsCommand.Execute(userSettings);

            return Ok();
        }
    }
}
