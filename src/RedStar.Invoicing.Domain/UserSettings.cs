using Newtonsoft.Json;

namespace RedStar.Invoicing.Domain
{
    public class UserSettings
    {
        [JsonProperty("id")]
        public string UserId { get; set; }
        public string LogoUrl { get; set; }
        public string InvoiceTemplate { get; set; }
    }
}
