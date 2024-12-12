using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppECommerce.Startup))]
namespace AppECommerce
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
