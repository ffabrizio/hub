using System.Text.RegularExpressions;
using System.Web;

namespace Renesis.Models.Extensions
{
	public static class StringExtensions
	{
		public static string ToPlainText(this string text)
		{
			return Regex.Replace(HttpUtility.HtmlDecode(text) ?? string.Empty, "<(.|\n)*?>", string.Empty);
		}
	}
}