using System.Web.Mvc;
using Renesis.Api.Config;

namespace Renesis.Models.Extensions
{
	public static class HtmlExtensions
	{
		public static MvcHtmlString ContentTypeIcon(this HtmlHelper helper, string contentTypeName)
		{
			return new MvcHtmlString(string.Format("<i class=\"{0}\"></i>",
			  RenesisConfiguration.Current.GetContentType(contentTypeName).Icon));
		}
	}
}