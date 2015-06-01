using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using RedStar.Invoicing.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RedStar.Invoicing.Controllers
{
    [Route("api/[controller]")]
    public class SettingsController : Controller
    {
        [HttpPost]
        public void Post([FromBody]SettingsDTO value)
        {
        }
    }
}
