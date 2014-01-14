using System.Configuration;

namespace Renesis.Api.Config
{
    public class ContentType : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name { get { return this["name"] as string; } }

        [ConfigurationProperty("typeName", IsRequired = true)]
        public string TypeName { get { return this["typeName"] as string; } }

        [ConfigurationProperty("displayName", IsRequired = true)]
        public string DisplayName { get { return this["displayName"] as string; } }

        [ConfigurationProperty("icon", IsRequired = true)]
        public string Icon { get { return this["icon"] as string; } }
    }

    public class ContentTypes : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ContentType();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ContentType)element).Name;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return "contentType"; }
        }
    }
}