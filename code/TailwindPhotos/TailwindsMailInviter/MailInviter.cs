using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace TailwindsMailInviter
{
    public static class MailInviter
    {
        [FunctionName("MailInviter")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation("Request Headers: ");
            foreach (var header in req.Headers)
            {
                log.LogInformation($"Header {header.Key} = '{header.Value}'");
            }
            return new OkResult();
        }
    }
}
