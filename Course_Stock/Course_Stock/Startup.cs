using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Course_Stock.Startup))]
namespace Course_Stock
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
