using System.Collections.Specialized;
using Renesis.Api.Models;

namespace Renesis.Models.Content
{
	public class HtmlContent : ContentItem
	{
		public string Body { get; set; }

		public override string ContentType
		{
			get { return "Html"; }
		}

		public override void SetProperties(NameValueCollection collection)
		{
			base.SetProperties(collection);
			Body = collection["Body"];
		}
	}
}