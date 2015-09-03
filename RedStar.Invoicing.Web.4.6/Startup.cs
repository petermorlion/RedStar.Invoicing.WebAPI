using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RedStar.Invoicing.Web._4._6.Startup))]
namespace RedStar.Invoicing.Web._4._6
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
