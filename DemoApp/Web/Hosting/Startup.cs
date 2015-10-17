using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DemoApp.Web.Hosting
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Initialize Authentication
            new DemoAppAuthenticationStartup().Initialize(appBuilder);

            // Initialize WebApi
            new DemoAppWebApiStartup().Initialize(appBuilder);

            // Initialize the FileSystem
            var path = "client";
            new DemoAppFileServerStartup().Initialize(appBuilder, path);
        }
    }
}
