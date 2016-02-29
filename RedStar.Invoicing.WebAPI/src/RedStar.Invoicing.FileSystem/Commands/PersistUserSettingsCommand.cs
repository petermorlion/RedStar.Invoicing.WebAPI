using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RedStar.Invoicing.Commands;
using RedStar.Invoicing.Domain;

namespace RedStar.Invoicing.FileSystem.Commands
{
    public class PersistUserSettingsCommand : IPersistUserSettingsCommand
    {
        public async Task Execute(UserSettings userSettings)
        {
            var json = JsonConvert.SerializeObject(userSettings);
            File.WriteAllText(Locations.UserSettingsFile, json);
        }
    }
}