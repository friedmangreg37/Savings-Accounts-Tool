using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Budgeting.Startup))]
namespace Budgeting
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
