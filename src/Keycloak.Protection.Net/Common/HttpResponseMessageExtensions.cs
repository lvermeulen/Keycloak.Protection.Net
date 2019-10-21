using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Keycloak.Protection.Net.Common
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<HttpResponseMessage> HandleErrorsAsync(this Task<HttpResponseMessage> responseMessageTask)
        {
            var response = await responseMessageTask;
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<ErrorEntity>(content);
                throw new InvalidOperationException($"Error {error.Error}: {error.ErrorDescription}");
            }

            return response;
        }
    }
}
