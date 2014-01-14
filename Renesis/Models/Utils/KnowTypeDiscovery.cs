using System;
using System.Linq;
using System.Reflection;
using Renesis.Api.Models;

namespace Renesis.Models.Utils
{
	public class KnowTypeDiscovery
	{
		public static Type[] GetKnownTypes()
		{
			var asm = Assembly.GetExecutingAssembly();

			return asm.GetTypes()
				.Where(type => type.BaseType == typeof (ContentItem))
				.ToArray();
		}
	}
}