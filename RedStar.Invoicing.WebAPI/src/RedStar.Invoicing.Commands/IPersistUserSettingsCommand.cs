using RedStar.Invoicing.Domain;
using System.Threading.Tasks;

namespace RedStar.Invoicing.Commands
{
    public interface IPersistUserSettingsCommand
    {
        Task Execute(UserSettings userSettings);
    }
}