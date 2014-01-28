using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Renesis.Api.Services;
using Renesis.Services;

namespace Renesis.Controllers
{
    //[Authorize]
	public class ContentController : ApiController
	{
		public async Task<HttpResponseMessage> Get()
		{
			return await Request.Content
			  .ReadAsStringAsync()
			  .ContinueWith(t =>
			  {
                  var culture = Request.GetRouteData().Values["culture"] ?? string.Empty;
                  var campaign = Request.GetRouteData().Values["campaign"] ?? string.Empty;
                  var contentService = ContentService.GetInstance(culture.ToString(), campaign.ToString());
                  var items = contentService.GetAllContentItems();
				  var query = Request.RequestUri.ParseQueryString();

				  var response = Request.CreateResponse(HttpStatusCode.OK, QueryService.GetResults(contentService, items, query));

				  return response;
			  });
		}
	}
}
