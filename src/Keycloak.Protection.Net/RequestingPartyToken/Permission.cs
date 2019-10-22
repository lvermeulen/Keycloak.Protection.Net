using Newtonsoft.Json;

namespace Keycloak.Protection.Net
{
    public class Permission
    {
        [JsonProperty("resource_id")]
        public string ResourceId { get; set; }
        [JsonProperty("resource_name")]
        public string ResourceName { get; set; }
    }
}