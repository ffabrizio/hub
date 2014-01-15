using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MongoRepository;

namespace Renesis.Api.Models
{
    [KnownType("DiscoverKnownTypes")]
    public abstract class ContentItem : IEntity
    {
        public string Id { get; set; }
        public string MasterId { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [DisplayName("Introduction")]
        [MaxLength(255)]
        public string Intro { get; set; }

        [DisplayName("Creation Date")]
        public DateTime CreationDate { get; set; }

        [MaxLength(255)]
        [DisplayName("Thumbnail URL")]
        public string ThumbnailUrl { get; set; }

        [MaxLength(255)]
        [DisplayName("External URL")]
        public string ExternalUrl { get; set; }

        public List<string> Tags { get; set; }
        public string Category { get; set; }
        public string CultureCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }
        public string CampaignId { get; set; }

        public abstract string ContentType { get; }

        public virtual void SetProperties(NameValueCollection collection)
        {
            IsActive = collection["IsActive"].Contains("true");
            IsFeatured = collection["IsFeatured"].Contains("true");
            Name = collection["Name"];
            Intro = collection["Intro"];
            Category = collection["Category"];
            MasterId = collection["MasterId"];
            CultureCode = collection["CultureCode"];
            CampaignId = collection["CampaignId"];

            DateTime creationDate;
            CreationDate = DateTime.TryParse(collection["CreationDate"], out creationDate) ? creationDate : DateTime.UtcNow;
            Tags = new List<string>();

            if (!string.IsNullOrEmpty(collection["Tags"]))
            {
                foreach (var tag in collection["Tags"].Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    Tags.Add(tag.Trim());
                }
            }
        }

        internal static Type[] DiscoverKnownTypes()
        {
            return KnowTypeDiscovery.GetKnownTypes();
        }
    }
}