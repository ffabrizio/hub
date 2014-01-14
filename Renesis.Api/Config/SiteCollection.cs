using System.Configuration;

namespace Renesis.Api.Config
{
    public class SiteCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new Site();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Site)element).CultureCode;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return "site"; }
        }
         
    }
}