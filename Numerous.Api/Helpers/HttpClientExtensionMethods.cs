using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Numerous.Api.Helpers
{
    internal static class HttpClientExtensionMethods
    {
        public static async Task<HttpResponseMessage> PostAsyncWithQuota(this HttpClient client, string uri, Image.Edit image)
        {
            HttpResponseMessage response;
            using (var content = GetMultipartFormDataContent(image))
                response = await client.PostAsync(uri, content);

            while (IsTemporaryError(response))
            {
                WaitReset(response);
                using (var content = GetMultipartFormDataContent(image))
                    response = await client.PostAsync(uri, content);
            }

            response.EnsureSuccessStatusCode();
            return response;
        }

        public static async Task<HttpResponseMessage> PostAsyncWithQuota(this HttpClient client, string uri, string jsonContent)
        {
            HttpResponseMessage response;
            using(var content = new StringContent(jsonContent, Encoding.UTF8, "application/json"))
                response = await client.PostAsync(uri, content);

            while (IsTemporaryError(response))
            {
                WaitReset(response);
                using (var content = new StringContent(jsonContent, Encoding.UTF8, "application/json"))
                    response = await client.PostAsync(uri, content);
            }

            response.EnsureSuccessStatusCode();
            return response;
        }

        public static async Task<HttpResponseMessage> PutAsyncWithQuota(this HttpClient client, string uri, string jsonContent)
        {
            HttpResponseMessage response;
            using (var content = new StringContent(jsonContent, Encoding.UTF8, "application/json"))
                response = await client.PutAsync(uri, content);

            while (IsTemporaryError(response))
            {
                WaitReset(response);
                using (var content = new StringContent(jsonContent, Encoding.UTF8, "application/json"))
                    response = await client.PutAsync(uri, content);
            }

            response.EnsureSuccessStatusCode();
            return response;
        }

        public static async Task<HttpResponseMessage> GetAsyncWithQuota(this HttpClient client, string uri)
        {
            var response = await client.GetAsync(uri);
            while (IsTemporaryError(response))
            {
                WaitReset(response);
                response = await client.GetAsync(uri);
            }

            response.EnsureSuccessStatusCode();
            return response;
        }

        public static async Task<HttpResponseMessage> DeleteAsyncWithQuota(this HttpClient client, string uri)
        {
            var response = await client.DeleteAsync(uri);
            while (IsTemporaryError(response))
            {
                WaitReset(response);
                response = await client.DeleteAsync(uri);
            }

            response.EnsureSuccessStatusCode();
            return response;
        }

        private static bool IsTemporaryError(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                // Error class 5xx is typically due to transient errors
                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.BadGateway:
                case HttpStatusCode.GatewayTimeout:
                case HttpStatusCode.ServiceUnavailable:

                case HttpStatusCode.RequestTimeout:
                case (HttpStatusCode)429:       // Quota exceeded
                    return true;
                default:
                    return false;
            }
        }

        private static void WaitReset(HttpResponseMessage response)
        {
            var reset = response.Headers.GetValues("X-Rate-Limit-Reset")
                .Select(v => int.Parse(v, CultureInfo.InvariantCulture) + 1)
                .DefaultIfEmpty(30)
                .First();

            Thread.Sleep(reset * 1000);
        }

        private static MultipartFormDataContent GetMultipartFormDataContent(Image.Edit image)
        {
            var content = new MultipartFormDataContent();
            var dataContent = new ByteArrayContent(image.Data);
            dataContent.Headers.ContentType = MediaTypeHeaderValue.Parse(image.MediaType);
            content.Add(dataContent, "image", "image.img");
            
            return content;
        }
    }
}