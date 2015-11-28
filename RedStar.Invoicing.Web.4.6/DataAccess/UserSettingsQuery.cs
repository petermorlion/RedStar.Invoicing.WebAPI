using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using RedStar.Invoicing.Web._4._6.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace RedStar.Invoicing.Web._4._6.DataAccess
{
    public class UserSettingsQuery
    {
        private const string DatabaseId = "RedStarInvoicing";
        private const string DocumentCollectionId = "UserSettings";

        public async Task<Optional<UserSettings>> Execute(string userId)
        {
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
                if (documentCollection == null)
                {
                    documentCollection = await client.CreateDocumentCollectionAsync(databaseLink, new DocumentCollection { Id = DocumentCollectionId });
                }

                var documentCollectionLink = string.Format("dbs/{0}/colls/{1}", DatabaseId, DocumentCollectionId);

                var document = client.CreateDocumentQuery<UserSettings>(documentCollectionLink).Where(d => d.UserId == userId).AsEnumerable().FirstOrDefault();

                if (document == null)
                {
                    return new Optional<UserSettings>(null);
                }
                else
                {
                    return new Optional<UserSettings>(new UserSettings
                    {
                        UserId = document.UserId,
                        LogoUrl = document.LogoUrl,
                        InvoiceTemplate = document.InvoiceTemplate
                    });
                }
            }
        }
    }
}
