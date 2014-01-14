using System;
using System.Linq;
using NUnit.Framework;
using Renesis.Api.Config;
using Renesis.Api.Services;

namespace Renesis.Api.UnitTests
{
    [TestFixture]
    public class ConfigurationTests
    {
        [Test]
        public void CheckSupportForMultipleCampaignsAndSites()
        {
            var config = RenesisConfiguration.Current;

            var uk = config.GetSite("en-GB");
            var campaign = uk.GetCampaign("m3vs");

            var svc = ContentService.GetInstance("en-US", "m3vs");
            var items = svc.GetAllContentItems();


            Console.WriteLine(campaign.CampaignFriendlyName);
            Console.WriteLine(items.Count());
        }
    }
}