using System;
using System.Threading.Tasks;
using RedStar.Invoicing.Domain;
using RedStar.Invoicing.Queries;

namespace RedStar.Invoicing.DocumentDb
{
    public class UserSettingsQuery : IUserSettingsQuery
    {
        public UserSettingsQuery()
        {
        }

        public Task<Optional<UserSettings>> Execute(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
