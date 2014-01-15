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
        public ActionResult Welcome()
        {
            ViewBag.Title = "Welcome";
            return View();
        }

        public ActionResult Index(string culture)
		{
            if (string.IsNullOrEmpty(culture))
            {
                return View("welcome");
            }
            var campaigns = RenesisConfiguration.Current.GetCampaigns().Where(c => string.Equals(c.Culture, culture, StringComparison.InvariantCultureIgnoreCase));

            ViewBag.Title = "Select campaign";
            return View(campaigns.ToList());
		}

		[AllowAnonymous]
		public ActionResult Login(string returnUrl)
		{
            ViewBag.Title = "Login";
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

            ViewBag.Title = "Login";
			return View(model);
		}
	}
}
