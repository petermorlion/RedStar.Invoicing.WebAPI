using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RedStar.Invoicing.Domain;
using RedStar.Invoicing.Queries;

namespace RedStar.Invoicing.FileSystem.Queries
{
    public class GetUserSettingsQuery : IGetUserSettingsQuery
    {
        public async Task<Optional<UserSettings>> Execute(string userId)
        {
            if (!File.Exists(Locations.UserSettingsFile))
            {
                return new Optional<UserSettings>(null);
            }

            var json = File.ReadAllText(Locations.UserSettingsFile);
            var userSettings = JsonConvert.DeserializeObject<UserSettings>(json);
            
            return new Optional<UserSettings>(userSettings);
        }
    }
}
