using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Frontend.Services
{
    class RESTDataTable<T> : IDataTable<T> where T : TableData
    {
        private HttpClient client = new HttpClient(new LoggingHandler(new HttpClientHandler()));
        private string tablePath;

        public RESTDataTable(Uri endpoint, string path) {
            client.BaseAddress = endpoint;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            tablePath = path;
            Debug.WriteLine($"new RESTDataTable {tablePath}");
        }

        public async Task<T> CreateItemAsync(T item)
        {
            Debug.WriteLine("CreateItemAsync");
            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8);
            var response = await client.PostAsync(tablePath, content);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task DeleteItemAsync(T item)
        {
            Debug.WriteLine("DeleteItemAsync");
            var response = await client.DeleteAsync($"{tablePath}/{item.Id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<ICollection<T>> ReadAllItemsAsync()
        {
            Debug.WriteLine("ReadAllItemsAsync");
            var response = await client.GetAsync(tablePath);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<List<T>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<T> ReadItemAsync(string id)
        {
            Debug.WriteLine("ReadItemAsync");
            var response = await client.GetAsync($"{tablePath}/{id}");
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task<T> UpdateItemAsync(T item)
        {
            Debug.WriteLine("UpdateItemAsync");
            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8);
            var response = await client.PutAsync($"{tablePath}/{item.Id}", content);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
    }

    class LoggingHandler : DelegatingHandler
    {
        public LoggingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"----> REQUEST {request.Method} {request.RequestUri}");
            Debug.WriteLine(request.ToString());
            if (request.Content != null)
            {
                Debug.WriteLine(await request.Content.ReadAsStringAsync());
            }
            Debug.WriteLine("----> END OF REQUEST");

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            Debug.WriteLine($"----> RESPONSE {response.StatusCode}");
            Debug.WriteLine(response.ToString());
            if (response.Content != null)
            {
                Debug.WriteLine(await response.Content.ReadAsStringAsync());
            }
            Debug.WriteLine("----> END OF RESPONSE");

            return response;
        }
    }
}
