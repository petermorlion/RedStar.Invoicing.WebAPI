using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using RedStar.Invoicing.WebAPI.DataContracts;
using RedStar.Invoicing.WebAPI.Queries;

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
        public Settings Get()
        {
            //var query = new UserSettingsQuery();
            //var userSettings = await query.Execute(User.Identity.GetUserId());

            //if (!userSettings.HasValue)
            //{
            //    throw new HttpResponseException(new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest));
            //}

            //return new InvoiceGeneratorDTO
            //{
            //    InvoiceTemplate = userSettings.Value.InvoiceTemplate,
            //    LogoUrl = userSettings.Value.LogoUrl
            //};

            throw new NotImplementedException();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
