using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AnimeANdGameMusic.Startup))]
namespace AnimeANdGameMusic
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
