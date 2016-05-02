using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.OptionsModel;
using Newtonsoft.Json;
using RedStar.Invoicing.Commands;
using RedStar.Invoicing.Domain;

namespace RedStar.Invoicing.DocumentDb.Commands
{
    public class PersistUserSettingsCommand : IPersistUserSettingsCommand
    {
        private readonly IOptions<DocumentDbSettings> _documentDbSettings;
        private const string DatabaseId = "RedStarInvoicing";
        private const string CollectionId = "UserSettings";

        // TODO: temporary
        private const string DocumentId = "4552d6c3-8f56-4c1e-b975-245c2adcebab";

        public PersistUserSettingsCommand(IOptions<DocumentDbSettings> documentDbSettings)
        {
            _documentDbSettings = documentDbSettings;
        }

        public async Task Execute(UserSettings userSettings)
        {
            var documentDbUrl = _documentDbSettings.Value.Endpoint;
            var authorizationKey = _documentDbSettings.Value.AuthorizationKey;

            using (var httpClient = new HttpClient())
            {
                var utcNow = DateTime.UtcNow;
                httpClient.DefaultRequestHeaders.Add("x-ms-date", utcNow.ToString("r"));
                httpClient.DefaultRequestHeaders.Add("x-ms-version", "2015-12-16");
                httpClient.DefaultRequestHeaders.Add("x-ms-documentdb-is-upsert", "true");

                var resourceLink = string.Format("dbs/{0}/colls/{1}", DatabaseId, CollectionId);
                var baseUrl = new Uri(documentDbUrl);

                var masterKeyAuthorizationSignatureGenerator = new MasterKeyAuthorizationSignatureGenerator();
                var authHeader = masterKeyAuthorizationSignatureGenerator.Generate("POST", resourceLink, "docs", authorizationKey, "master", "1.0", utcNow);
                httpClient.DefaultRequestHeaders.Add("authorization", authHeader);

                var response = await httpClient.PostAsJsonAsync(new Uri(baseUrl, resourceLink + "/docs"), userSettings);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    // handle error
                }
                else
                {
                    //return success?
                }
            }
        }
    }
}