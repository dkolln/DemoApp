using System;
using Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin;
using DemoApp.Web.Security;

namespace DemoApp.Web.Hosting
{
    public class DemoAppAuthenticationStartup
    {
        public void Initialize(IAppBuilder app)
        {
            var OAuthOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new DemoAppAuthorizationServerProvider(new DemoAppUserManager(new DemoAppUserStore()))
            };

            app.UseOAuthBearerTokens(OAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}