using Newtonsoft.Json;

namespace Keycloak.Protection.Net
{
    public class Scope
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
