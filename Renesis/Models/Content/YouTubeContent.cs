using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Renesis.Api.Models;

namespace Renesis.Models.Content
{
	public class YouTubeContent : ContentItem
	{
		[Required]
		[MaxLength(10)]
		[DisplayName("YouTube ID")]
		public string YouTubeId { get; set; }

		[DisplayName("End Frame URL")]
		public string EndFrame { get; set; }

		public override string ContentType
		{
			get { return "YouTube"; }
		}

		public override void SetProperties(NameValueCollection collection)
		{
			base.SetProperties(collection);
			YouTubeId = collection["YouTubeId"];
			EndFrame = collection["EndFrame"];
		}
	}
}