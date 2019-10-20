using Newtonsoft.Json;

namespace Keycloak.Protection.Net
{
    public class PermissionTicketRequest
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("resource")]
        public string Resource { get; set; }
        [JsonProperty("requester")]
        public string Requester { get; set; }
        [JsonProperty("requesterName")]
        public string RequesterName { get; set; }
        [JsonProperty("granted")]
        public bool? Granted { get; set; }
        [JsonProperty("scopeName")]
        public string ScopeName { get; set; }
    }
}
