using Newtonsoft.Json;

namespace Keycloak.Protection.Net
{
    public class Owner
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
