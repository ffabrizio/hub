using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using MongoDB.Driver.Linq;
using MongoRepository;
using Renesis.Api.Config;
using Renesis.Api.Models;

namespace Renesis.Api.Services
{
    public class ContentService
    {
        private const string connstringName = "MongoServerSettings";
        private readonly IRepository<ContentItem> repository;
        private readonly static RenesisConfiguration config = RenesisConfiguration.Current;
        private static readonly Dictionary<string, ContentService> instances =
            new Dictionary<string, ContentService>();

        public Campaign Campaign { get; private set; }
        public bool IsLocalInstance { get; private set; }

        private ContentService(Campaign campaign, bool isLocalInstance)
        {
            Campaign = campaign;
            IsLocalInstance = isLocalInstance;

            var connstring = ConfigurationManager.ConnectionStrings[connstringName].ConnectionString;
            repository = new MongoRepository<ContentItem>(connstring);
        }

        public static ContentService GetInstance(string cultureCode, string campaignId)
        {
            const string svcIdFormat = "{0}::{1}";
            if (instances.Count == 0)
            {
                foreach (var campaign in config.GetCampaigns())
                {
                    var isLocalInstance = !string.Equals(campaign.Culture, campaign.MasterCulture, StringComparison.InvariantCultureIgnoreCase);
                    instances.Add(string.Format(svcIdFormat, campaign.Culture, campaign.CampaignId),
                        new ContentService(campaign, isLocalInstance));
                }
            }

            return instances
                .FirstOrDefault(i => string.Equals(i.Key, string.Format(svcIdFormat, cultureCode.ToLowerInvariant(), campaignId.ToLowerInvariant()), StringComparison.InvariantCultureIgnoreCase))
                .Value;
        }

        public IQueryable<ContentItem> GetAllContentItems()
        {
            if (!IsLocalInstance)
            {
                return repository.Where(i => i.CultureCode == Campaign.MasterCulture && (i.CampaignId == Campaign.CampaignId || i.CampaignId == config.SharedCampaignValue));
            }

            return repository.Where(i => i.CultureCode == Campaign.Culture && (i.CampaignId == Campaign.CampaignId || i.CampaignId == config.SharedCampaignValue));
        }

        public IQueryable<ContentItem> GetMasters()
        {
            if (!IsLocalInstance)
            {
                return new List<ContentItem>().AsQueryable();
            }

            var masterIDs = GetAllContentItems().Select(i => i.MasterId).ToList();
            return repository.Where(i => i.IsActive && i.CultureCode == Campaign.MasterCulture && !(i.Id.In(masterIDs)));
        }


        public IQueryable<ContentItem> GetPublished()
        {
            return GetAllContentItems().Where(c => c.IsActive);
        }

        public string Add(ContentItem item)
        {
            item.CultureCode = !IsLocalInstance ?
                Campaign.MasterCulture : Campaign.Culture;

            if (repository.Add(item) != null)
            {
                return item.Id;
            }

            return string.Empty;
        }

        public string Update(ContentItem item)
        {
            if (item.CultureCode == Campaign.MasterCulture && IsLocalInstance)
            {
                item.MasterId = item.Id;
                item.Id = Guid.NewGuid().ToString();
                item.CultureCode = Campaign.Culture;

                if (repository.Add(item) != null)
                {
                    return item.Id;
                }
            }

            if (repository.Update(item) != null)
            {
                return item.Id;
            }

            return string.Empty;
        }

        public void Delete(ContentItem item)
        {
            repository.Delete(item.Id);
        }

        public void Delete(string id)
        {
            repository.Delete(id);
        }

        //#region Configurable Filters

        //public List<SelectListItem> GetContentTypes()
        //{
        //    var list = CampaignConfig.GetContentTypes();
        //    return list.Select(key =>
        //            new SelectListItem { Selected = false, Value = key.Name, Text = key.DisplayName })
        //            .ToList();
        //}

        //public string GetContentType(string key)
        //{
        //    var content = GetContentTypes().FirstOrDefault(i => i.Value == key);
        //    if (content != null)
        //    {
        //        return content.Text;
        //    }

        //    return string.Empty;
        //}

        //public List<SelectListItem> GetCategories()
        //{
        //    var list = CampaignConfig.Categories;
        //    return list.AllKeys.Select(key =>
        //            new SelectListItem { Selected = false, Value = list[key].Name, Text = list[key].Value })
        //            .ToList();
        //}

        //public string GetCategory(string key)
        //{
        //    var content = GetCategories().FirstOrDefault(i => i.Value == key);
        //    if (content != null)
        //    {
        //        return content.Text;
        //    }

        //    return string.Empty;
        //}

        //public List<SelectListItem> GetTags()
        //{
        //    var list = CampaignConfig.Tags;
        //    return list.AllKeys.Select(key =>
        //            new SelectListItem { Selected = false, Value = list[key].Name, Text = list[key].Value })
        //            .ToList();
        //}

        //public string GetTags(List<string> tags)
        //{
        //    return string.Join(", ",
        //        GetTags()
        //            .Where(t => tags.Contains(t.Value))
        //            .Select(t => t.Text)
        //            .ToArray());
        //}

        //#endregion
    }
}