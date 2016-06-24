using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BugReportAssist.Startup))]
namespace BugReportAssist
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
