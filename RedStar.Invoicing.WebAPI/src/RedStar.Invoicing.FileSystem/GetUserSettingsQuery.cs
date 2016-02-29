using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RedStar.Invoicing.Domain;
using RedStar.Invoicing.Queries;

namespace RedStar.Invoicing.FileSystem
{
    public class GetUserSettingsQuery : IGetUserSettingsQuery
    {
        private const string UserSettingsFile = "./userSettings.json";

        public async Task<Optional<UserSettings>> Execute(string userId)
        {
            if (!File.Exists(UserSettingsFile))
            {
                return new Optional<UserSettings>(null);
            }

            var json = File.ReadAllText("./userSettings.json");
            var userSettings = JsonConvert.DeserializeObject<UserSettings>(json);
            
            return new Optional<UserSettings>(userSettings);
        }
    }
}
