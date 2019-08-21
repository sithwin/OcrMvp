using ComputerVision.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ComputerVision.Api
{
    public class OcrClient
    {
        public async Task PostPolicyInfoAsync(PolicyInfo model)
        {
            var response = string.Empty;
            HttpClient client = new HttpClient();
            Uri uri = new Uri("https://localhost:5001/api/newbusiness");

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = uri,
                Content = content
            };

            HttpResponseMessage result = await client.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                response = result.StatusCode.ToString();
            }
        }
    }
}
