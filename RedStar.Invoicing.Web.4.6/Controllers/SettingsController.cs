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
using System.Configuration;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json;

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

            var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["CloudStorageConnection"].ConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("icons");

            // TODO: only do once?
            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            var userId = User.Identity.GetUserId();

            var blockBlob = container.GetBlockBlobReference(userId.Replace("-", "") + imageExtension);

            await blockBlob.UploadFromByteArrayAsync(imageBytes, 0, imageBytes.Length);

            var documentDBUrl = ConfigurationManager.AppSettings["DocumentDBEndpointUrl"];
            var authorizationKey = ConfigurationManager.AppSettings["DocumentDBAuthorizationKey"];
            using (var client = new DocumentClient(new Uri(documentDBUrl), authorizationKey))
            {
                var database = client.CreateDatabaseQuery().Where(x => x.Id == "RedStarInvoicing").First();
                DocumentCollection documentCollection = client.CreateDocumentCollectionQuery(database.SelfLink, "UserSettings").First();
                var document = client.CreateDocumentQuery(documentCollection.SelfLink).Where(d => d.Id == userId).FirstOrDefault();

                if (document == null)
                {
                    var userSettings = new UserSettings
                    {
                        UserId = userId,
                        LogoUrl = blockBlob.Uri.AbsoluteUri,
                        InvoiceTemplate = settingsDto.InvoiceTemplate
                    };

                    await client.CreateDocumentAsync(document.SelfLink, userSettings);
                }
                else
                {
                    UserSettings userSettings = JsonConvert.DeserializeObject<UserSettings>(document.ToString());
                    userSettings.LogoUrl = blockBlob.Uri.AbsoluteUri;
                    userSettings.InvoiceTemplate = settingsDto.InvoiceTemplate;
                    await client.ReplaceDocumentAsync(document.SelfLink, userSettings);
                }
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
