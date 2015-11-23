using RedStar.Invoicing.Web._4._6.Models;
using System;
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
    [RoutePrefix("api/settings")]
    [Authorize]
    public class SettingsController : ApiController
    {
        private const string DatabaseId = "RedStarInvoicing";
        private const string DocumentCollectionId = "UserSettings";

        public SettingsController()
        {

        }

        [Route("")]
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
                var database = client.CreateDatabaseQuery().Where(x => x.Id == DatabaseId).AsEnumerable().FirstOrDefault();
                if (database == null)
                {
                    database = await client.CreateDatabaseAsync(new Database { Id = DatabaseId });
                }

                var databaseLink = string.Format("dbs/{0}", DatabaseId);
                DocumentCollection documentCollection = client.CreateDocumentCollectionQuery(databaseLink).Where(c => c.Id == DocumentCollectionId).ToArray().FirstOrDefault();
                if(documentCollection == null)
                {
                    documentCollection = await client.CreateDocumentCollectionAsync(databaseLink, new DocumentCollection { Id = DocumentCollectionId });
                }

                var documentCollectionLink = string.Format("dbs/{0}/colls/{1}", DatabaseId, DocumentCollectionId);

                var document = client.CreateDocumentQuery<UserSettings>(documentCollectionLink).Where(d => d.UserId == userId).AsEnumerable().FirstOrDefault();

                if (document == null)
                {
                    var userSettings = new UserSettings
                    {
                        UserId = userId,
                        LogoUrl = blockBlob.Uri.AbsoluteUri,
                        InvoiceTemplate = settingsDto.InvoiceTemplate
                    };

                    await client.CreateDocumentAsync(documentCollectionLink, userSettings);
                }
                else
                {
                    var documentLink = string.Format("dbs/{0}/colls/{1}/docs/{2}", DatabaseId, DocumentCollectionId, userId);
                    document.LogoUrl = blockBlob.Uri.AbsoluteUri;
                    document.InvoiceTemplate = settingsDto.InvoiceTemplate;
                    await client.ReplaceDocumentAsync(documentLink, document);
                }
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
