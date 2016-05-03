using System.Threading.Tasks;
using RedStar.Invoicing.Domain;

namespace RedStar.Invoicing.Queries
{
    public interface IGetUserSettingsQuery
    {
        Task<Optional<UserSettings>> Execute(string userId);
    }
}
