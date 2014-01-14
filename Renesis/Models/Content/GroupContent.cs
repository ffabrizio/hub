using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Renesis.Api.Models;
using Renesis.Api.Services;
using Renesis.Services;

namespace Renesis.Models.Content
{
	public class GroupContent : ContentItem
	{
		List<ContentItem> contentItems;

		[DisplayName("Content Items")]
		public string ContentItemsReferences
		{
			get;
			set;
		}

		public List<ContentItem> ContentItems { get; set; }

		public List<ContentItem> GetContentItems(ContentService svc)
		{
			if (contentItems != null)
			{
				return contentItems;
			}

			contentItems = new List<ContentItem>();
			if (ContentItemsReferences == null || ContentItemsReferences.Length <= 0)
			{
				return contentItems;
			}

			foreach (var item in ContentItemsReferences.Split(',')
                .Select(id => svc.GetAllContentItems()
					.FirstOrDefault(c => c.Id == id)).Where(item => item != null))
			{
				contentItems.Add(item);
			}

			return contentItems;
		}

		[DisplayName("Group Type")]
		public string GroupType { get; set; }

		[DisplayName("Action Text")]
		public string ActionText { get; set; }

		[DisplayName("Action Link")]
		public string ActionLink { get; set; }

		public override string ContentType
		{
			get { return "Group"; }
		}

		public override void SetProperties(NameValueCollection collection)
		{
			base.SetProperties(collection);
			var list = (collection["ContentItemsReferences"].Split(',')
				.Where(id => !string.IsNullOrEmpty(id))
				.Select(id => id.Trim())).ToList();

			ContentItemsReferences = string.Join(",", list);

			GroupType = collection["GroupType"];
			ActionLink = collection["ActionLink"];
			ActionText = collection["ActionText"];
		}
	}
}