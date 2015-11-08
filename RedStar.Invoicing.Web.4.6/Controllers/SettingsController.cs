using RedStar.Invoicing.Web._4._6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.AspNet.Identity;

namespace RedStar.Invoicing.Web._4._6.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class SettingsController : ApiController
    {
        public SettingsController()
        {

        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<HttpResponseMessage> Post([FromBody]SettingsDTO settingsDto)
        {
            // TODO: validate invoice template for javascript and other fishy stuff
            // TODO: image type and size
            var imageBytes = Convert.FromBase64String(settingsDto.Logo.Substring(settingsDto.Logo.IndexOf(",") + 1));
            var imageExtension = settingsDto.LogoName.Substring(settingsDto.LogoName.LastIndexOf("."));

            var storageAccount = CloudStorageAccount.Parse("");
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("icons");

            // TODO: only do once?
            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            var userId = User.Identity.GetUserId();

            var blockBlob = container.GetBlockBlobReference(userId.Replace("-", "") + imageExtension);

            await blockBlob.UploadFromByteArrayAsync(imageBytes, 0, imageBytes.Length);

            //var userSetting = _invoicesDbContext.UserSettings.SingleOrDefault(x => x.UserId.ToString() == userId);

            //if (userSetting == null)
            //{
            //    userSetting = new UserSettings { UserId = userId };
            //    _invoicesDbContext.Add(userSetting);
            //}

            //userSetting.InvoiceTemplate = settingsDto.InvoiceTemplate;
            //userSetting.LogoUrl = blockBlob.Uri.AbsoluteUri;

            //await _invoicesDbContext.SaveChangesAsync();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
