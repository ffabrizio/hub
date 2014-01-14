using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Renesis.Api.Config
{
    public class Campaign : ConfigurationElement
    {
        public string Culture
        {
            get;
            internal set;
        }

        [ConfigurationProperty("masterCulture")]
        public string MasterCulture
        {
            get { return (string)base["masterCulture"]; }
            set { base["masterCulture"] = value; }
        }

        [ConfigurationProperty("campaignId")]
        public string CampaignId
        {
            get { return (string)base["campaignId"]; }
            set { base["campaignId"] = value; }
        }

        [ConfigurationProperty("campaignFriendlyName")]
        public string CampaignFriendlyName
        {
            get { return (string)base["campaignFriendlyName"]; }
            set { base["campaignFriendlyName"] = value; }
        }

        [ConfigurationProperty("apiPageSize")]
        public string ApiPageSize
        {
            get { return (string)base["apiPageSize"]; }
            set { base["apiPageSize"] = value; }
        }

        [ConfigurationProperty("tags")]
        public NameValueConfigurationCollection Tags
        {
            get
            {
                return (NameValueConfigurationCollection)base["tags"];
            }
        }

        [ConfigurationProperty("contentTypes")]
        public ContentTypes ContentTypes
        {
            get
            {
                return this["contentTypes"] as ContentTypes;
            }
        }

        [ConfigurationProperty("categories")]
        public NameValueConfigurationCollection Categories
        {
            get
            {
                return (NameValueConfigurationCollection)base["categories"];
            }
        }

        public List<ContentType> GetContentTypes()
        {
            return ContentTypes.OfType<ContentType>().ToList();
        }
    }
}