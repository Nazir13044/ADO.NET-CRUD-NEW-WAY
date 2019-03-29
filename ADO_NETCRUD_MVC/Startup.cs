using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ADO_NETCRUD_MVC.Startup))]
namespace ADO_NETCRUD_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
