using Newtonsoft.Json.Serialization;
using System.Web.Http;
using System.Xml;

namespace SocialNetwork
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			var settings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
			settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			settings.Formatting = (Newtonsoft.Json.Formatting)Formatting.Indented;


			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
