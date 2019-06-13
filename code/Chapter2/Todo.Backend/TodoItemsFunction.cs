using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Todo.Backend
{
    public static class TodoItemsFunction
    {
        [FunctionName("TodoItems_GetAll")]
        public static async Task<IActionResult> GetAll(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "todoitems")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"Processing HTTP request {req.Method} {req.Path}");
            var db = TodoItemsDatabase.Instance;

            return new OkObjectResult(await db.GetAllItemsAsync());
        }

        [FunctionName("TodoItems_GetOne")]
        public static async Task<IActionResult> GetOne(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "todoitems/{id}")] HttpRequest req,
            ILogger log,
            string id)
        {
            log.LogInformation($"Processing HTTP request {req.Method} {req.Path}");
            var db = TodoItemsDatabase.Instance;
            var item = await db.GetItemAsync(id);
            if (item == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(item);
        }

        [FunctionName("TodoItems_Create")]
        public static async Task<IActionResult> Create(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "todoitems")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"Processing HTTP request {req.Method} {req.Path}");
            var db = TodoItemsDatabase.Instance;

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            TodoItem item = JsonConvert.DeserializeObject<TodoItem>(requestBody);
            var result = await db.SaveItemAsync(item);
            return new OkObjectResult(result);
        }

        [FunctionName("TodoItems_Update")]
        public static async Task<IActionResult> Update(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "todoitems/{id}")] HttpRequest req,
            ILogger log,
            string id)
        {
            log.LogInformation($"Processing HTTP request {req.Method} {req.Path}");
            var db = TodoItemsDatabase.Instance;
            var item = await db.GetItemAsync(id);
            if (item == null)
            {
                return new NotFoundResult();
            }

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            TodoItem request = JsonConvert.DeserializeObject<TodoItem>(requestBody);
            if (request.ID != id)
            {
                return new BadRequestResult();
            }
            var result = await db.SaveItemAsync(request);
            return new OkObjectResult(result);
        }

        [FunctionName("TodoItems_Delete")]
        public static async Task<IActionResult> Delete(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "todoitems/{id}")] HttpRequest req,
            ILogger log,
            string id)
        {
            log.LogInformation($"Processing HTTP request {req.Method} {req.Path}");
            var db = TodoItemsDatabase.Instance;
            var item = await db.GetItemAsync(id);
            if (item == null)
            {
                return new NotFoundResult();
            }
            await db.DeleteItemAsync(id);
            return new OkObjectResult(item);
        }
    }
}
