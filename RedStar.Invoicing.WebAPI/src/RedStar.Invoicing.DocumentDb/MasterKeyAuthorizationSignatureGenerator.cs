using System;
using System.Net;

namespace RedStar.Invoicing.DocumentDb
{
    public class MasterKeyAuthorizationSignatureGenerator
    {
        public string Generate(string verb, string resourceId, string resourceType, string key, string keyType, string tokenVersion, DateTime requestDateTime)
        {
            var hmacSha256 = new System.Security.Cryptography.HMACSHA256 { Key = Convert.FromBase64String(key) };

            var payLoad = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}\n{1}\n{2}\n{3}\n{4}\n",
                    verb.ToLowerInvariant(),
                    resourceType.ToLowerInvariant(),
                    resourceId,
                    requestDateTime.ToString("r").ToLowerInvariant(),
                    ""
            );

            var hashPayLoad = hmacSha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(payLoad));
            var signature = Convert.ToBase64String(hashPayLoad);

            return WebUtility.UrlEncode(string.Format(System.Globalization.CultureInfo.InvariantCulture, "type={0}&ver={1}&sig={2}",
                keyType,
                tokenVersion,
                signature));
        }
    }
}