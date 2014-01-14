using System.Configuration;

namespace Renesis.Api.Config
{
    public class CampaignCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new Campaign();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Campaign)element).CampaignId;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return "campaign"; }
        }

    }
}