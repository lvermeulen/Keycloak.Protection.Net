using Newtonsoft.Json;

namespace Keycloak.Protection.Net.Common
{
    public class ErrorEntity
    {
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }
    }
}
