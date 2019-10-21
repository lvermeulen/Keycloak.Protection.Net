using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Keycloak.Protection.Net.Common;

namespace Keycloak.Protection.Net
{
    public partial class KeycloakProtectionClient
    {
        private IFlurlRequest GetPolicyUrl(string realm, string pat) => new Url(_url)
            .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
            .WithOAuthBearerToken(pat)
            .AppendPathSegment($"/auth/realms/{realm}/authz/protection/uma-policy");

        public async Task<Policy> CreatePolicyAsync(string realm, string pat, string resourceId, Policy policy) => await GetPolicyUrl(realm, pat)
            .AppendPathSegment($"/{resourceId}")
            .AllowAnyHttpStatus()
            .WithHeader("Cache-Control", "no-cache")
            .PostJsonAsync(policy)
            .HandleErrorsAsync()
            .ReceiveJson<Policy>()
            .ConfigureAwait(false);

        public async Task<IEnumerable<Policy>> GetPoliciesAsync(string realm, string pat, 
            string resource = null, string name = null, string scope = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                [nameof(resource)] = resource,
                [nameof(name)] = name,
                [nameof(scope)] = scope
            };

            return await GetPolicyUrl(realm, pat)
                .SetQueryParams(queryParamValues)
                .GetJsonAsync<IEnumerable<Policy>>()
                .ConfigureAwait(false);
        }

        public async Task<bool> UpdatePolicyAsync(string realm, string pat, string policyId, Policy policy)
        {
            var response = await GetPolicyUrl(realm, pat)
                .AppendPathSegment(policyId)
                .AllowAnyHttpStatus()
                .PutJsonAsync(policy)
                .HandleErrorsAsync()
                .ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeletePolicyAsync(string realm, string pat, string policyId)
        {
            var response = await GetPolicyUrl(realm, pat)
                .AppendPathSegment($"/{policyId}")
                .AllowAnyHttpStatus()
                .DeleteAsync()
                .HandleErrorsAsync()
                .ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }
    }
}
