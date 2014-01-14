using System;
using MongoDB.Bson.Serialization;
using Renesis.Api.Config;

namespace Renesis.Startup
{
	public class ContentTypesConfig
	{
		public static void RegisterContentTypes()
		{
			var contentTypes = RenesisConfiguration.Current.GetContentTypes();
			foreach (var contentType in contentTypes)
			{
				var type = Type.GetType(contentType.TypeName);
				BsonClassMap.LookupClassMap(type);
			}
		}
	}
}