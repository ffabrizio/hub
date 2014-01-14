using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Renesis.Api.Models;

namespace Renesis.Models.Content
{
	public class MazdaVideoContent : ContentItem
	{
		[Required]
		[MaxLength(10)]
		[DisplayName("Account")]
		public string AccountId { get; set; }

		[Required]
		[MaxLength(255)]
		[DisplayName("Video ID")]
		public string VideoId { get; set; }

		public override string ContentType
		{
			get { return "MazdaVideo"; }
		}

		public override void SetProperties(NameValueCollection collection)
		{
			base.SetProperties(collection);
			VideoId = collection["MazdaId"];
		}
	}
}