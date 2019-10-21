using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Keycloak.Protection.Net.Common;

namespace Keycloak.Protection.Net
{
    public partial class KeycloakProtectionClient
    {
        private IFlurlRequest GetResourceUrl(string realm, string pat) => new Url(_url)
            .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
            .WithOAuthBearerToken(pat)
            .AppendPathSegment($"/auth/realms/{realm}/authz/protection/resource_set");

        public async Task<ResourceCreated> CreateResourceAsync(string realm, string pat, ResourceRequest resourceRequest) => await GetResourceUrl(realm, pat)
            .PostJsonAsync(resourceRequest)
            .HandleErrorsAsync()
            .ReceiveJson<ResourceCreated>()
            .ConfigureAwait(false);

        public async Task<Resource> GetResourceAsync(string realm, string pat, string resourceId) => await GetResourceUrl(realm, pat)
            .AppendPathSegment($"/{resourceId}")
            .GetJsonAsync<Resource>()
            .ConfigureAwait(false);

        public async Task<bool> UpdateResourceAsync(string realm, string pat, string resourceId, ResourceRequest resourceRequest)
        {
            var response = await GetResourceUrl(realm, pat)
                .AppendPathSegment($"/{resourceId}")
                .PutJsonAsync(resourceRequest)
                .HandleErrorsAsync()
                .ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteResourceAsync(string realm, string pat, string resourceId)
        {
            var response = await GetResourceUrl(realm, pat)
                .AppendPathSegment($"/{resourceId}")
                .DeleteAsync()
                .HandleErrorsAsync()
                .ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<string>> GetResourcesAsync(string realm, string pat) => await GetResourceUrl(realm, pat)
            .GetJsonAsync<IEnumerable<string>>()
            .ConfigureAwait(false);
    }
}
