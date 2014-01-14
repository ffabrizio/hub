using System;
using System.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http.Cors;

namespace Renesis.Api.Config
{
    public class CorsPolicyAttribute : Attribute, ICorsPolicyProvider
    {
        private readonly CorsPolicy policy;

        public CorsPolicyAttribute()
        {
            policy = new CorsPolicy
            {
                AllowAnyHeader = true,
                AllowAnyMethod = false
            };

            policy.Methods.Add("GET");
            foreach (NameValueConfigurationElement origin in RenesisConfiguration.Current.CorsOrigins)
            {
                policy.Origins.Add(origin.Value);
            }
        }

        public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(policy);
        }
    }
}