using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace Keycloak.Protection.Net
{
    public partial class KeycloakProtectionClient
    {
        private IFlurlRequest GetPermissionUrl(string realm, string pat) => new Url(_url)
            .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
            .WithOAuthBearerToken(pat)
            .AppendPathSegment($"/auth/realms/{realm}/authz/protection/permission");

        private IFlurlRequest GetPermissionTicketUrl(string realm, string pat) => GetPermissionUrl(realm, pat)
            .AppendPathSegment("/ticket");

        public async Task<PermissionTicket> CreatePermissionTicketAsync(string realm, string pat, PermissionTicketRequest permissionTicketRequest) => await GetPermissionTicketUrl(realm, pat)
            .PostJsonAsync(permissionTicketRequest)
            .ReceiveJson<PermissionTicket>()
            .ConfigureAwait(false);

        public async Task<IEnumerable<PermissionTicket>> GetPermissionTicketsAsync(string realm, string pat, 
            string scopeId = null, string resourceId = null, string owner = null, string requester = null, bool? granted = null, 
            string returnNames = null, int? first = null, int? max = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                [nameof(scopeId)] = scopeId,
                [nameof(resourceId)] = resourceId,
                [nameof(owner)] = owner,
                [nameof(requester)] = requester,
                [nameof(granted)] = granted,
                [nameof(returnNames)] = returnNames,
                [nameof(first)] = first,
                [nameof(max)] = max
            };

            return await GetPermissionTicketUrl(realm, pat)
                .SetQueryParams(queryParamValues)
                .GetJsonAsync<IEnumerable<PermissionTicket>>()
                .ConfigureAwait(false);
        }

        public async Task<bool> UpdatePermissionTicketAsync(string realm, string pat, PermissionTicketRequest permissionTicketRequest)
        {
            var response = await GetPermissionTicketUrl(realm, pat)
                .PutJsonAsync(permissionTicketRequest)
                .ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeletePermissionTicketAsync(string realm, string pat, string permissionTicketId)
        {
            var response = await GetPermissionTicketUrl(realm, pat)
                .AppendPathSegment($"/{permissionTicketId}")
                .AllowAnyHttpStatus()
                .DeleteAsync()
                .ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }
    }
}
