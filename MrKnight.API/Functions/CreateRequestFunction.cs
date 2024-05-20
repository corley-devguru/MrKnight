using System.Collections.Specialized;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using MrKnight.Core.Services;

namespace MrKnight.API.Functions
{
    public class CreateRequestFunction
    {
        private readonly ILogger<CreateRequestFunction> _logger;
        private readonly IKnightPathService _service;

        public CreateRequestFunction(IKnightPathService service, ILogger<CreateRequestFunction> logger)
        {
            _service = service;
            _logger = logger;
        }

        [Function("CreateRequestFunction")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "knightpath")] HttpRequestData req)
        {
            NameValueCollection? queryParams = System.Web.HttpUtility.ParseQueryString(req.Url.Query);

            string? source = queryParams["source"];
            string? target = queryParams["target"];

            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(target))
            {
                HttpResponseData badRequestResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badRequestResponse.WriteStringAsync("Please provide both source and target positions.");
                return badRequestResponse;
            }

            string operationId = await _service.CreateRequestAsync(source, target);

            HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync($"Operation Id {operationId} was created. Please query it to find your results.");
            return response;
        }
    }
}