using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using MongoDB.Driver.Linq;
using Renesis.Api.Models;
using Renesis.Api.Services;
using Renesis.Models.Content;
using Renesis.Models.Utils;

namespace Renesis.Services
{
	public class QueryService
	{
		public static ResultWrapper GetResults(ContentService svc, IQueryable<ContentItem> items, NameValueCollection query, int pagecount = 10)
		{
			var wrapper = new ResultWrapper();

			var all = query["active"];
			var q = query["q"];
			var types = query["types"];
			var categories = query["categories"];
			var tags = query["tags"];
			var page = query["page"];
			var id = query["id"];

			if (!string.IsNullOrEmpty(all) && !all.Contains("true"))
			{
				items = items.Where(i => !i.IsActive);
			}
			else if (!string.IsNullOrEmpty(all) && !all.Contains("false"))
			{
				items = items.Where(i => i.IsActive);
			}

			if (!string.IsNullOrEmpty(id))
			{
				var itemid = id.ToLowerInvariant().Split(',');
				items = items.Where(i => i.Id.In(itemid));
			}

			if (!string.IsNullOrEmpty(q))
			{
				var sq = q.ToLowerInvariant();
				items = items.Where(i => 
					i.Name.ToLowerInvariant().Contains(sq) ||
					i.Intro.ToLowerInvariant().Contains(sq) || i.Id == q);
			}

			if (!string.IsNullOrEmpty(categories))
			{
				var catsArray = categories.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
				items = items.Where(i => i.Category.In(catsArray));
			}

			if (!string.IsNullOrEmpty(tags))
			{
				var tagsArray = tags.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
				items = items.Where(i => i.Tags.ContainsAny(tagsArray));
			}

			wrapper.Items = items.OrderByDescending(i => i.CreationDate).ToList();

			if (!string.IsNullOrEmpty(types))
			{
				var typeNames = types.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
				wrapper.Items = new List<ContentItem>(wrapper.Items.Where(i => typeNames.Contains(i.ContentType)));
			}

			if (svc.IsLocalInstance)
			{
				wrapper.Items.InsertRange(0, svc.GetMasters());
			}

			int pagenumber = 0;
			var itemsCount = wrapper.Items.Count;
			 
			if (!string.IsNullOrEmpty(page) && int.TryParse(page, out pagenumber))
			{
				wrapper.Items = wrapper.Items.Skip(pagenumber * pagecount).Take(pagecount).ToList();
			}

			wrapper.ItemsLeft = itemsCount - pagecount - (pagenumber * pagecount);
			if (wrapper.ItemsLeft < 0)
			{
				wrapper.ItemsLeft = 0;
			}

			foreach (var item in wrapper.Items)
			{
				var groupContent = item as GroupContent;
				if (groupContent != null)
				{
					groupContent.ContentItems = groupContent.GetContentItems(svc);
				}
			}

			return wrapper;
		}
	}
}