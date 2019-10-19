using System.IO;
using Microsoft.Extensions.Configuration;

namespace Keycloak.Protection.Net.Tests
{
    public partial class KeycloakProtectionClientShould
    {
        private readonly KeycloakProtectionClient _client;

        public KeycloakProtectionClientShould()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            string url = configuration["url"];
            string userName = configuration["userName"];
            string password = configuration["password"];

            _client = new KeycloakProtectionClient(url, userName, password);
        }
    }
}
