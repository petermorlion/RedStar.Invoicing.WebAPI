using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using RedStar.Invoicing.Models;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Framework.ConfigurationModel;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RedStar.Invoicing.Controllers
{
    [Route("api/[controller]")]
    public class SettingsController : Controller
    {
        [HttpPost]
        public async void Post([FromBody]SettingsDTO value)
        {
            // TODO: image type and size
            var imageBytes = Convert.FromBase64String(value.Logo.Substring(value.Logo.IndexOf(",") + 1));
            var imageExtension = value.LogoName.Substring(value.LogoName.LastIndexOf("."));

            var configuration = new Configuration().AddUserSecrets();
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(configuration.Get("StorageConnectionString"));
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("icons");

            // TODO: only do once?
            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            var blockBlob = container.GetBlockBlobReference(Guid.NewGuid().ToString().Replace("-", "") + imageExtension);

            await blockBlob.UploadFromByteArrayAsync(imageBytes, 0, imageBytes.Length);
        }
    }
}
