﻿using System;
using System.Linq;
using Renesis.Api.Models;

namespace Renesis.Models.Utils
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