using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Jivy_Malik_Goods_Transport_Company.Startup))]
namespace Jivy_Malik_Goods_Transport_Company
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
