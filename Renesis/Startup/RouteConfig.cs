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
                "home/{action}/{culture}",
                new
                {
                    controller = "Home",
                    action = "Welcome",
                    culture = UrlParameter.Optional
                },
                new
                {
                    controller = "Home"
                }
            );
			routes.MapRoute(
				"Admin",
                "{culture}/{campaign}/{controller}/{action}/{id}",
                new
                {
                    controller = "Admin", 
                    action = "Index",
                    culture = "", 
                    campaign = "", 
                    id = UrlParameter.Optional
                }
			);

            
		}
	}
}