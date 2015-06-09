using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using RedStar.Invoicing.Models;
using Newtonsoft.Json;
using RedStar.Invoicing.Commands;
using System.Web.Http;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RedStar.Invoicing.Controllers
{
    [Route("api/[controller]")]
    public class SettingsController : Controller
    {
        private CommandsDbContext _commandsDbContext;

        public SettingsController(CommandsDbContext commandsDbContext)
        {
            _commandsDbContext = commandsDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SettingsDTO value)
        {
            // TODO: validate invoice template for javascript and other fishy stuff

            var command = new Command
            {
                Name = "SaveSettings",
                Data = JsonConvert.SerializeObject(value)
            };

            _commandsDbContext.Commands.Add(command);
            _commandsDbContext.SaveChanges();

            throw new Exception("test");
        }
    }
}
