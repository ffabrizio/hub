using System.Web.Optimization;

namespace Renesis.Startup
{
	public class BundleConfig
	{
		public static void ConfigureBundles()
		{
			BundleTable.Bundles.Add(new ScriptBundle("~/scripts/lib.js")
				.Include("~/scripts/bootstrap.js")
				.Include("~/scripts/jquery-2.0.3.js")
				.Include("~/scripts/app/app.js")
				.Include("~/scripts/app/router.js")
				.Include("~/scripts/app/main.js")
				.IncludeDirectory("~/scripts/app/views/", "*.js")
				.IncludeDirectory("~/scripts/app/cms/", "*.js")
				.Include("~/scripts/app/config.js"));

			BundleTable.Bundles.Add(new StyleBundle("~/css/style.css")
				.Include("~/content/bootstrap.css")
				.Include("~/content/bootstrap-theme.css")
				.Include("~/content/main.css"));
		}
	}
}