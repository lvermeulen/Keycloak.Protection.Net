using System.Collections.Generic;
using Newtonsoft.Json;

namespace Keycloak.Protection.Net
{
    public class RequestingPartyTokenIntrospection
    {
        [JsonProperty("permissions")]
        public IEnumerable<Permission> Permissions { get; set; }
        [JsonProperty("exp")]
        public long ExpirationTime { get; set; }
        [JsonProperty("nbf")]
        public long NotBeforeTime { get; set; }
        [JsonProperty("iat")]
        public long IssuedAtTime { get; set; }
        [JsonProperty("aud")]
        public string Audience { get; set; }
        [JsonProperty("active")]
        public bool Active { get; set; }
    }
}
