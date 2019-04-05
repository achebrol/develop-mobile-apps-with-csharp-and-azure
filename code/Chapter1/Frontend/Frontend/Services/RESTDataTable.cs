using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Services
{
    class RESTDataTable<T> : IDataTable<T> where T : TableData
    {
        private HttpClient client = new HttpClient();
        private string tablePath;

        public RESTDataTable(Uri endpoint, string path) {
            client.BaseAddress = endpoint;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.tablePath = path;
        }

        public async Task<T> CreateItemAsync(T item)
        {
            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8);
            var response = await client.PostAsync(tablePath, content);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task DeleteItemAsync(T item)
        {
            var response = await client.DeleteAsync($"{tablePath}/{item.Id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<ICollection<T>> ReadAllItemsAsync()
        {
            var response = await client.GetAsync(tablePath);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<List<T>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<T> ReadItemAsync(string id)
        {
            var response = await client.GetAsync($"{tablePath}/{id}");
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task<T> UpdateItemAsync(T item)
        {
            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8);
            var response = await client.PutAsync($"{tablePath}/{item.Id}", content);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
    }
}
