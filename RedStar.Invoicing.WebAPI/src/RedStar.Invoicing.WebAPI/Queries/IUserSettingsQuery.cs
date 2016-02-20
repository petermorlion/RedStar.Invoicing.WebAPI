using RedStar.Invoicing.Domain;
using System.Threading.Tasks;

namespace RedStar.Invoicing.WebAPI.Queries
{
    public interface IUserSettingsQuery
    {
        Task<Optional<UserSettings>> Execute(string userId);
    }
}
