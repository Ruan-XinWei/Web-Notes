using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FirstAspNet.Startup))]
namespace FirstAspNet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
