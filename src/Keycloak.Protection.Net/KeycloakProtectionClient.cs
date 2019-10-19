using System;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Keycloak.Protection.Net.Common.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Keycloak.Protection.Net
{
    public partial class KeycloakProtectionClient
    {
        private static readonly ISerializer s_serializer = new NewtonsoftJsonSerializer(new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
        });

        static KeycloakProtectionClient()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
            };
        }

        private readonly Url _url;
        private readonly string _userName;
        private readonly string _password;
        private readonly Func<string> _getToken;

        private KeycloakProtectionClient(string url)
        {
            _url = url;
        }

        public KeycloakProtectionClient(string url, string userName, string password)
            : this(url)
        {
            _userName = userName;
            _password = password;
        }

        public KeycloakProtectionClient(string url, Func<string> getToken)
            : this(url)
        {
            _getToken = getToken;
        }

        public IFlurlRequest GetBaseUrl(string authenticationRealm) => new Url(_url)
            .AppendPathSegment("/auth")
            .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
            .WithAuthentication(_getToken, _url, authenticationRealm, _userName, _password);
    }
}
