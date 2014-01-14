using System;
using System.Configuration;
using System.Linq;

namespace Renesis.Api.Config
{
    public class Site : ConfigurationElement
    {
        [ConfigurationProperty("culture", IsRequired = false)]
        public string CultureCode
        {
            get { return (string)base["culture"]; }
            set { base["culture"] = value; }
        }

        [ConfigurationProperty("campaigns")]
        public CampaignCollection Campaigns
        {
            get { return (CampaignCollection)base["campaigns"]; }
            set { base["campaigns"] = value; }
        }

        public Campaign GetCampaign(string campaignId)
        {
            return Campaigns.OfType<Campaign>()
                .FirstOrDefault(s => String.Equals(s.CampaignId, campaignId, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}