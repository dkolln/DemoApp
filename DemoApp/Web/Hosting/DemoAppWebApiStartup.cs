using System;
using System.Web.Http;
using Owin;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json.Serialization;

namespace DemoApp.Web.Hosting
{
    public class DemoAppWebApiStartup
    {
        public void Initialize(IAppBuilder appBuilder)
        {
            // Configuration for the web server.
            var webApi = new HttpConfiguration();

            var jsonSettings = new JsonSerializerSettings();

            // Converts Enum's to strings instead of their number-equivalents.
            jsonSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            // Makes the C# upper-cased property names lower-case when it's send to the client.
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            webApi.Formatters.JsonFormatter.SerializerSettings = jsonSettings;
            
            // Only do JSON
            webApi.Formatters.Remove(webApi.Formatters.XmlFormatter);

            // TODO Remove if you want better security
            appBuilder.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            // Enable the [Route] and [RoutePrefix] attributes.
            webApi.MapHttpAttributeRoutes();

            appBuilder.UseWebApi(webApi);
        }
    }
}