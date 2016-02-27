using System;
using System.Threading.Tasks;
using RedStar.Invoicing.Domain;
using RedStar.Invoicing.WebAPI.Controllers;
using RedStar.Invoicing.WebAPI.DataContracts;
using RedStar.Invoicing.WebAPI.Queries;
using RedStar.Invoicing.WebAPI.Tests.Mocks;
using Xunit;

namespace RedStar.Invoicing.WebAPI.Tests.Controllers.SettingsControllerTests
{
    public class When_getting_settings_for_user
    {
        private SettingsController _sut;
        private Settings _result;

        [Fact]
        public void Then_return_query_result()
        {
            var query = new UserSettingsQueryMock();
            _sut = new SettingsController(query);

            _result = _sut.Get();

            Assert.NotNull(_result);
        }
    }
}