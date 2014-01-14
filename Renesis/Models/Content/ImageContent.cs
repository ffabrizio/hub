using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Renesis.Api.Models;

namespace Renesis.Models.Content
{
	public class ImageContent : ContentItem
	{
		[Required]
		[MaxLength(255)]
		[DisplayName("Image URL")]
		public string ImageUrl { get; set; }

		public override string ContentType
		{
			get { return "Image"; }
		}

		public override void SetProperties(NameValueCollection collection)
		{
			base.SetProperties(collection);
			ImageUrl = collection["ImageUrl"];
		}
	}
}