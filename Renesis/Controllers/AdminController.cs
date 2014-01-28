using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Mvc;
using Renesis.Api.Config;
using Renesis.Api.Models;
using Renesis.Api.Services;
using Renesis.Models.Utils;

namespace Renesis.Controllers
{
    //[Authorize]
	public class AdminController : Controller
	{
		public ActionResult Index()
		{
			var contentService = GetService();
			if (contentService == null)
			{
                return RedirectToRoute(new
                {
                    controller = "home",
                    action = "index",
                    culture = RouteData.Values["culture"]
                });
			}

            ViewBag.Country = RouteData.Values["culture"];
			return View();
		}

		public ActionResult Details(string id)
		{
			var contentService = GetService();
			var item = contentService.GetAllContentItems().SingleOrDefault(i => i.Id == id);
			if (item != null)
			{
				return View(item);
			}

			return new HttpNotFoundResult();
		}


		public ActionResult Create(string id)
		{
			GetService();
			var item = RenesisConfiguration.Current.GetNewContentType(id);
			if (item != null)
			{
				return View(item);
			}

			return new HttpNotFoundResult();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		[ValidateInput(false)]
		public ActionResult Create(FormCollection collection)
		{
			var contentService = GetService();
			if (contentService == null)
			{
				return new HttpNotFoundResult();
			}

			try
			{
				var id = Guid.NewGuid().ToString().ToLowerInvariant();
				var item = CreateFromForm(id, collection);

				if (ModelState.IsValid)
				{
					contentService.Add(item);
				}

				return RedirectToAction("index");
			}
			catch
			{
				return new HttpStatusCodeResult(500);
			}
		}

		public ActionResult Clone(string id)
		{
			var contentService = GetService();
			if (contentService == null)
			{
				return new HttpNotFoundResult();
			}
			var item = contentService.GetMasters().SingleOrDefault(i => i.Id == id);
			if (item != null)
			{
				item.Id = Guid.NewGuid().ToString();
				item.IsActive = false;
				item.MasterId = id;

				return View(item);
			}

			return new HttpNotFoundResult();
		}

		public ActionResult Edit(string id)
		{
			var contentService = GetService();
			if (contentService == null)
			{
				return new HttpNotFoundResult();
			}
			var item = contentService.GetAllContentItems().SingleOrDefault(i => i.Id == id);
			if (item != null)
			{
				return View(item);
			}

			return new HttpNotFoundResult();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		[ValidateInput(false)]
		public ActionResult Edit(string id, FormCollection collection)
		{
			var contentService = GetService();
			if (contentService == null)
			{
				return new HttpNotFoundResult();
			}
			try
			{
				var itemId = id;
				var item = CreateFromForm(itemId, collection);
				if (ModelState.IsValid)
				{
					itemId = contentService.Update(item);
				}

				return RedirectToAction("details", new { id = itemId });
			}
			catch
			{
				return new HttpStatusCodeResult(500);
			}
		}

		public ActionResult Delete(string id)
		{
			var contentService = GetService();
			if (contentService == null)
			{
				return new HttpNotFoundResult();
			}
			var item = contentService.GetAllContentItems().SingleOrDefault(i => i.Id == id);
			if (item != null)
			{
				return View(item);
			}

			return new HttpNotFoundResult();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(string id, FormCollection collection)
		{
			var contentService = GetService();
			if (contentService == null)
			{
				return new HttpNotFoundResult();
			}
			try
			{
				contentService.Delete(id);
				return RedirectToAction("index");
			}
			catch
			{
				return new HttpStatusCodeResult(500);
			}
		}

	    public ActionResult Import()
	    {
            var contentService = GetService();
            if (contentService == null)
            {
                return new HttpNotFoundResult();
            }

            ResultWrapper data;
            var path = Server.MapPath("~/App_Data/") + "content.xml";
            var serializer = new DataContractSerializer(typeof(ResultWrapper));

	        using (var reader = new StreamReader(path))
	        {
	            data = (ResultWrapper)serializer.ReadObject(reader.BaseStream);
	            reader.Close();
	        }

	        foreach (var item in data.Items)
	        {
	            contentService.Add(item);
	        }

	        return Content("IMPORT OK");
	    }

	    private ContentItem CreateFromForm(string id, FormCollection collection)
		{
			var contentType = collection["ContentType"];
			var item = RenesisConfiguration.Current.GetNewContentType(contentType);
			if (item != null)
			{
				item.Id = id;
				item.SetProperties(collection);
				return item;
			}

			return null;
		}

		private ContentService GetService()
		{
			var culture = RouteData.Values["culture"] ?? string.Empty;
            var campaign = RouteData.Values["campaign"] ?? string.Empty;
            var svc = ContentService.GetInstance(culture.ToString(), campaign.ToString());

			ViewBag.Service = svc;
			return svc;
		}
	}
}
