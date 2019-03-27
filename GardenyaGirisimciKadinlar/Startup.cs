using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GardenyaGirisimciKadinlar.Startup))]
namespace GardenyaGirisimciKadinlar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
