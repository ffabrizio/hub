using System.Collections.Generic;
using Renesis.Api.Models;

namespace Renesis.Models.Utils
{
	public class ResultWrapper
	{
		public int ItemsLeft { get; set; }
		public List<ContentItem> Items { get; set; }
	}
}