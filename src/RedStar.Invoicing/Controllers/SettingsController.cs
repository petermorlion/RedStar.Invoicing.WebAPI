using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using RedStar.Invoicing.Models;
using Newtonsoft.Json;
using System.Web.Http;
using System.Net.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Framework.Configuration;
using Microsoft.AspNet.Authorization;
using System.Security.Claims;
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RedStar.Invoicing.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class SettingsController : Controller
    {
        private IConfiguration _configuration;
        private InvoicesDbContext _invoicesDbContext;

        public SettingsController(InvoicesDbContext invoicesDbContext, IConfiguration configuration)
        {
            _invoicesDbContext = invoicesDbContext;
            _configuration = configuration;
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody]SettingsDTO settingsDto)
        {
            // TODO: validate invoice template for javascript and other fishy stuff
            // TODO: image type and size
            var imageBytes = Convert.FromBase64String(settingsDto.Logo.Substring(settingsDto.Logo.IndexOf(",") + 1));
            var imageExtension = settingsDto.LogoName.Substring(settingsDto.LogoName.LastIndexOf("."));

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_configuration.Get("StorageConnectionString"));
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("icons");

            // TODO: only do once?
            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            var userId = Context.User.GetUserId();

            var blockBlob = container.GetBlockBlobReference(userId.Replace("-", "") + imageExtension);

            await blockBlob.UploadFromByteArrayAsync(imageBytes, 0, imageBytes.Length);

            var userSetting = _invoicesDbContext.UserSettings.SingleOrDefault(x => x.UserId.ToString() == userId);

            if (userSetting == null)
            {
                userSetting = new UserSettings { UserId = userId } ;
                _invoicesDbContext.Add(userSetting);
            }

            userSetting.InvoiceTemplate = settingsDto.InvoiceTemplate;
            userSetting.LogoUrl = blockBlob.Uri.AbsoluteUri;

            await _invoicesDbContext.SaveChangesAsync();

            return new HttpStatusCodeResult(200);
        }
    }
}
