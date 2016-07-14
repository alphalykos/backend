using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(projectUpload.Startup))]
namespace projectUpload
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
