using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Renesis.Api.Config;
using Renesis.Api.Services;
using Renesis.Services;

namespace Renesis.Controllers
{
	[CorsPolicy]
	public class FeedController : ApiController
	{
        [AllowAnonymous]
		public async Task<HttpResponseMessage> Get()
		{
			return await Request.Content
			  .ReadAsStringAsync()
			  .ContinueWith(t =>
			  {
				  int pagecount;

				  var culture = Request.GetRouteData().Values["culture"] ?? string.Empty;
                  var campaign = Request.GetRouteData().Values["campaign"] ?? string.Empty;
				  var contentService = ContentService.GetInstance(culture.ToString(), campaign.ToString());
				  var items = contentService.GetPublished()
                          .Where(i => string.Equals(i.CultureCode, contentService.Campaign.Culture, StringComparison.InvariantCultureIgnoreCase));

				  if (!int.TryParse(contentService.Campaign.ApiPageSize, out pagecount))
				  {
					  pagecount = 10;
				  }

				  var query = Request.RequestUri.ParseQueryString();

                  var response = Request.CreateResponse(HttpStatusCode.OK, QueryService.GetResults(contentService, items, query, pagecount));
				  response.Headers.CacheControl = new CacheControlHeaderValue { MaxAge = new TimeSpan(0, 0, 60), Public = true};
				  
				  return response;
			  });

		}
	}
}