using System.Threading.Tasks;
using RedStar.Invoicing.Commands;
using RedStar.Invoicing.Domain;

namespace RedStar.Invoicing.FileSystem.Commands
{
    public class PersistUserSettingsCommand : IPersistUserSettingsCommand
    {
        public Task Execute(UserSettings userSettings)
        {
            throw new System.NotImplementedException();
        }
    }
}