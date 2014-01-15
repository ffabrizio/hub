using System;
using System.Linq;

namespace Renesis.Api.Models
{
	public class KnowTypeDiscovery
	{
		public static Type[] GetKnownTypes()
		{
            return AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(assembly => assembly.GetTypes())
                       .Where(type => type.IsSubclassOf(typeof(ContentItem)))
                       .ToArray();
		}
	}
}