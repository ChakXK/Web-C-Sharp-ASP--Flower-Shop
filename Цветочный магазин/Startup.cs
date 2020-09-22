using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Цветочный_магазин.Startup))]
namespace Цветочный_магазин
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
