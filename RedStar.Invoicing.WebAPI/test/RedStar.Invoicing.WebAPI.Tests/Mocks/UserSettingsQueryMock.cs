using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedStar.Invoicing.WebAPI.Tests.Mocks
{
    public class UserSettingsQueryMock : IUserSettingsQuery
    {
        Task<Optional<UserSettings>> IUserSettingsQuery.Execute(string userId)
        {
            return new Task<Optional<UserSettings>>(
                () => 
                {
                    return new Optional<UserSettings>();
                });
        }
    }
}
