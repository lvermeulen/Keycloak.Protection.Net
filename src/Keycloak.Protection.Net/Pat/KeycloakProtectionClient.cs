using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace Keycloak.Protection.Net
{
    public partial class KeycloakProtectionClient
    {
        private async Task<AccessTokenResponse> GetAccessTokenResponseAsync(string realm, object data) => await new Url(_url)
            .AppendPathSegment($"/auth/realms/{realm}/protocol/openid-connect/token")
            .PostUrlEncodedAsync(data)
            .ReceiveJson<AccessTokenResponse>()
            .ConfigureAwait(false);

        public async Task<string> GetPatAsync(string realm, object data)
        {
            var accessTokenResponse = await GetAccessTokenResponseAsync(realm, data).ConfigureAwait(false);
            return accessTokenResponse.AccessToken;
        }
    }
}
