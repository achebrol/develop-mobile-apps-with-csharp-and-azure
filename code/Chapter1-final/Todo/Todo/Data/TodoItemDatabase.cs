using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Todo.Data
{
    public class TodoItemDatabase
    {
        private static readonly Uri siteUri = new Uri("https://todobackend20190607091840.azurewebsites.net");
        private static readonly string endpoint = "api/todoitems";
        private readonly HttpClient httpClient = new HttpClient();

        #region Singleton
        private static TodoItemDatabase instance;

        public static TodoItemDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TodoItemDatabase(siteUri);
                }
                return instance;
            }
        }
        #endregion

        private TodoItemDatabase(Uri siteUri)
        {
            httpClient.BaseAddress = siteUri;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<TodoItem>> GetAllItemsAsync()
        {
            var response = await httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            List<TodoItem> result = JsonConvert.DeserializeObject<List<TodoItem>>(jsonString);
            return result;
        }

        public async Task<TodoItem> GetItemAsync(string id)
        {
            var response = await httpClient.GetAsync($"{endpoint}/{id}");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            TodoItem result = JsonConvert.DeserializeObject<TodoItem>(jsonString);
            return result;
        }

        public async Task<TodoItem> SaveItemAsync(TodoItem item)
        {
            HttpResponseMessage response;
            bool usePost = false;

            if (item.ID == null)
            {
                item.ID = Guid.NewGuid().ToString();
                usePost = true;
            }

            var content = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(item)));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            if (usePost)
            {
                response = await httpClient.PostAsync(endpoint, content);
            }
            else
            {
                response = await httpClient.PutAsync($"{endpoint}/{item.ID}", content);
            }

            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            TodoItem result = JsonConvert.DeserializeObject<TodoItem>(jsonString);
            return result;
        }

        public async Task DeleteItemAsync(TodoItem item)
        {
            var response = await httpClient.DeleteAsync($"{endpoint}/{item.ID}");
            response.EnsureSuccessStatusCode();
        }
    }
}
