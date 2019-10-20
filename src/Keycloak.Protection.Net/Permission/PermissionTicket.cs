using Newtonsoft.Json;

namespace Keycloak.Protection.Net
{
    public class PermissionTicket
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("owner")]
        public string Owner { get; set; }
        [JsonProperty("resource")]
        public string Resource { get; set; }
        [JsonProperty("scope")]
        public string Scope { get; set; }
        [JsonProperty("granted")]
        public bool Granted { get; set; }
        [JsonProperty("requester")]
        public string Requester { get; set; }
    }
}
