using System.Web.Http;

namespace Renesis.Startup
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.EnableCors();
			config.Routes.MapHttpRoute(
				"ContentApi", 
				"{culture}/api/{campaign}/{controller}/{id}",
                new { culture = RouteParameter.Optional, campaign = RouteParameter.Optional, id = RouteParameter.Optional }
			);
		}
	}
}
