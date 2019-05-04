using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Online_Food_Corner.Startup))]
namespace Online_Food_Corner
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
