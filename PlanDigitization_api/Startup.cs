using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlanDigitization_api.Startup))]
namespace PlanDigitization_api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseCors(CorsOptions.AllowAll);
            //ConfigureAuth(app);
           // app.MapSignalR(new HubConfiguration { EnableJSONP = true });
            //app.MapSignalR();
        }
    }
}