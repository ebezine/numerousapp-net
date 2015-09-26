using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Numerous.Api.Helpers
{
    internal static class NumerousContextExtensionMethods
    {
        public static async Task<HttpResponseMessage> PostWithRetry(this NumerousContext context, string uri, Image.Edit image)
        {
            return await WithRetry(context,
                async () => {
                    using (var content = GetMultipartFormDataContent(image))
                        return await context.Client.PostAsync(uri, content);
                });
        }

        public static async Task<HttpResponseMessage> PostWithRetry(this NumerousContext context, string uri, string jsonContent)
        {
            return await WithRetry(context,
                async () =>
                {
                    using (var content = new StringContent(jsonContent, Encoding.UTF8, "application/json"))
                        return await context.Client.PostAsync(uri, content);
                });
        }

        public static async Task<HttpResponseMessage> PutWithRetry(this NumerousContext context, string uri, string jsonContent)
        {
            return await WithRetry(context,
                async () =>
                {
                    using (var content = new StringContent(jsonContent, Encoding.UTF8, "application/json"))
                        return await context.Client.PutAsync(uri, content);
                });
        }

        public static async Task<HttpResponseMessage> GetWithRetry(this NumerousContext context, string uri)
        {
            return await WithRetry(context,
                () => context.Client.GetAsync(uri));
        }

        public static async Task<HttpResponseMessage> DeleteWithRetry(this NumerousContext context, string uri)
        {
            return await WithRetry(context,
                () => context.Client.DeleteAsync(uri));
        }

        #region Private Helpers

        private static async Task<HttpResponseMessage> WithRetry(this NumerousContext context, Func<Task<HttpResponseMessage>> evaluator)
        {
            // First attempt;
            var attempt = 0;
            var response = await evaluator();

            while (!response.IsSuccessStatusCode && context.AllowRetry(response, attempt))
            {
                context.WaitRetry(response);

                attempt++;
                response = await evaluator();
            }

            response.EnsureSuccessStatusCode();
            return response;
        }

        private static MultipartFormDataContent GetMultipartFormDataContent(Image.Edit image)
        {
            var content = new MultipartFormDataContent();
            var dataContent = new ByteArrayContent(image.Data);
            dataContent.Headers.ContentType = MediaTypeHeaderValue.Parse(image.MediaType);
            content.Add(dataContent, "image", "image.img");

            return content;
        }

        #endregion

    }
}