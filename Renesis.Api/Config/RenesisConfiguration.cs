using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Renesis.Api.Models;

namespace Renesis.Api.Config
{
    public class RenesisConfiguration : ConfigurationSection
    {
        public static readonly RenesisConfiguration Current = (RenesisConfiguration)ConfigurationManager.GetSection("renesis");

        [ConfigurationProperty("corsOrigins", IsRequired = false)]
        public NameValueConfigurationCollection CorsOrigins
        {
            get
            {
                return (NameValueConfigurationCollection)base["corsOrigins"];
            }
        }

        [ConfigurationProperty("sites")]
        public SiteCollection Sites
        {
            get { return (SiteCollection)base["sites"]; }
            set { base["sites"] = value; }
        }

        public Site GetSite(string culture)
        {
            return Sites.OfType<Site>()
                .FirstOrDefault(s => String.Equals(s.CultureCode, culture, StringComparison.InvariantCultureIgnoreCase));
        }

        public List<Campaign> GetCampaigns()
        {
            var campaigns = new List<Campaign>();
            foreach (Site site in Sites)
            {
                foreach (Campaign campaign in site.Campaigns)
                {
                    campaign.Culture = site.CultureCode.ToLowerInvariant();
                    campaigns.Add(campaign);
                }
            }

            return campaigns;
        }

        public List<ContentType> GetContentTypes()
        {
            var types = new List<ContentType>();
            foreach (var contentType in GetCampaigns()
                .SelectMany(campaign => campaign.GetContentTypes()
                    .Where(contentType => types.All(c => c.TypeName != contentType.TypeName))))
            {
                types.Add(contentType);
            }

            return types;
        }

        public ContentType GetContentType(string contentType)
        {
            return GetContentTypes().FirstOrDefault(c => String.Equals(c.Name, contentType, StringComparison.InvariantCultureIgnoreCase));
        }

        public ContentItem GetNewContentType(string contentType)
        {
            var type = GetContentType(contentType);
            if (type != null && !string.IsNullOrEmpty(type.TypeName))
            {
                var t = Type.GetType(type.TypeName);
                if (t != null)
                {
                    var o = Activator.CreateInstance(t) as ContentItem;
                    if (o != null)
                    {
                        o.CreationDate = DateTime.UtcNow;
                        return o;
                    }
                }
            }

            return null;
        }
    }
}