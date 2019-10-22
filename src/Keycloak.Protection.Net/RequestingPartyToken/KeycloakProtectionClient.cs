using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Keycloak.Protection.Net.Common;

namespace Keycloak.Protection.Net
{
    public partial class KeycloakProtectionClient
    {
        private IFlurlRequest GetRequestingPartyTokenUrl(string realm, string authorizationHeaderValue) => new Url(_url)
            .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
            .WithHeader("Authorization", authorizationHeaderValue)
            .AppendPathSegment($"/auth/realms/{realm}/protocol/openid-connect/token/introspect");

        public async Task<RequestingPartyTokenIntrospection> IntrospectRequestingPartyTokenAsync(string realm, string authorizationHeaderValue, string requestingPartyToken) => await GetRequestingPartyTokenUrl(realm, authorizationHeaderValue)
            .AllowAnyHttpStatus()
            .PostUrlEncodedAsync(new
            {
                token_type_hint = "requesting_party_token",
                token = requestingPartyToken
            })
            .HandleErrorsAsync()
            .ReceiveJson<RequestingPartyTokenIntrospection>()
            .ConfigureAwait(false);
    }
}
