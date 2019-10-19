using Newtonsoft.Json;

namespace Keycloak.Protection.Net
{
    public class ResourceCreated
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("user_access_policy_uri")]
        public string UserAccessPolicyUri { get; set; }
    }
}
