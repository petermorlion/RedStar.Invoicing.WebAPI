using System;
using System.Threading.Tasks;
using RedStar.Invoicing.Domain;
using RedStar.Invoicing.Queries;

namespace RedStar.Invoicing.DocumentDb
{
    public class GetUserSettingsQuery : IGetUserSettingsQuery
    {
        public GetUserSettingsQuery()
        {
        }

        public Task<Optional<UserSettings>> Execute(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
