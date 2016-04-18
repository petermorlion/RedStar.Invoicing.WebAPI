using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.OptionsModel;
using Newtonsoft.Json;
using RedStar.Invoicing.Domain;
using RedStar.Invoicing.Queries;

namespace RedStar.Invoicing.DocumentDb.Queries
{
    public class GetUserSettingsQuery : IGetUserSettingsQuery
    {
        private const string DatabaseId = "RedStarInvoicing";

        private readonly IOptions<DocumentDbSettings> _documentDbSettings;

        public GetUserSettingsQuery(IOptions<DocumentDbSettings> documentDbSettings)
        {
            _documentDbSettings = documentDbSettings;
        }

        public async Task<Optional<UserSettings>> Execute(string userId)
        {
            var documentDbUrl = _documentDbSettings.Value.Endpoint;
            var authorizationKey = _documentDbSettings.Value.AuthorizationKey;

            using (var httpClient = new HttpClient())
            {
                var utcNow = DateTime.UtcNow;
                httpClient.DefaultRequestHeaders.Add("x-ms-date", utcNow.ToString("r"));
                httpClient.DefaultRequestHeaders.Add("x-ms-version", "2015-08-06");

                var documentId = "4552d6c3-8f56-4c1e-b975-245c2adcebab";
                var collectionUrl = string.Format("dbs/{0}/colls/{1}/docs/{2}", "RedStarInvoicing", "UserSettings", documentId);
                var baseUrl = new Uri(documentDbUrl);

                var masterKeyAuthorizationSignatureGenerator = new MasterKeyAuthorizationSignatureGenerator();
                var authHeader = masterKeyAuthorizationSignatureGenerator.Generate("GET", collectionUrl, "docs", authorizationKey, "master", "1.0", utcNow);
                httpClient.DefaultRequestHeaders.Add("authorization", authHeader);

                var response = await httpClient.GetAsync(new Uri(baseUrl, collectionUrl));

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return new Optional<UserSettings>(null);
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var userSettings = JsonConvert.DeserializeObject<UserSettings>(json);
                    return new Optional<UserSettings>(userSettings);
                }
            }
        }
    }
}
