using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Renesis.Api.Config;
using Renesis.Models;

namespace Renesis.Controllers
{
    [Authorize]
	public class HomeController : Controller
	{
		public ActionResult Index(string culture)
		{
			var campaigns = RenesisConfiguration.Current.GetCampaigns().Where(c => string.Equals(c.Culture, culture, StringComparison.InvariantCultureIgnoreCase));
			return View(campaigns.ToList());
		}

		[AllowAnonymous]
		public ActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginModel model, string returnUrl)
		{
			if (Membership.ValidateUser(model.UserName, model.Password))
			{
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                return RedirectToAction("index");
			}
			return View(model);
		}
	}
}
