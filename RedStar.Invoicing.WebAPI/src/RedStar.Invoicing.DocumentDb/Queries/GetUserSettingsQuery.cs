using System;
using System.Threading.Tasks;
using RedStar.Invoicing.Domain;
using RedStar.Invoicing.Queries;

namespace RedStar.Invoicing.DocumentDb.Queries
{
    public class GetUserSettingsQuery : IGetUserSettingsQuery
    {
        public Task<Optional<UserSettings>> Execute(string userId)
        {
            var documentDBUrl = Configuration["DocumentDb:Endpoint"];
            var authorizationKey = Configuration.AppSettings["DocumentDb:AuthorizationKey"];

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
