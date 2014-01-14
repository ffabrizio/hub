using System.Web.Mvc;
using System.Web.Routing;

namespace Renesis.Startup
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.MapRoute(
				"Home",
                "home/{action}/{id}",
                new { controller = "Home", action = "Index", culture = UrlParameter.Optional }
			);
			routes.MapRoute(
				"Admin",
                "{culture}/{campaign}/{controller}/{action}/{id}",
                new { controller = "Admin", action = "Index", culture = UrlParameter.Optional, campaign = UrlParameter.Optional, id = UrlParameter.Optional }
			);
		}
	}
}