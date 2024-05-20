using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using MrKnight.Core.Services;
using System.Net;
using System.Web;

namespace MrKnight.API.Functions;

public class ReturnResultsFunction
{
    private readonly IKnightPathService _service;
    private readonly ILogger<ReturnResultsFunction> _logger;

    public ReturnResultsFunction(IKnightPathService service, ILogger<ReturnResultsFunction> logger)
    {
        _service = service;
        _logger = logger;
    }

    [Function("ReturnResultsFunction")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "knightpath")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _logger.LogInformation("Returning knight path results.");

        var queryParams = HttpUtility.ParseQueryString(req.Url.Query);
        string? operationId = queryParams["operationId"];

        if (string.IsNullOrEmpty(operationId))
        {
            var badRequestResponse = req.CreateResponse(HttpStatusCode.BadRequest);
            await badRequestResponse.WriteStringAsync("Please provide an operationId.");
            return badRequestResponse;
        }

        var result = await _service.GetResultAsync(operationId);

        if (result == null)
        {
            var notFoundResponse = req.CreateResponse(HttpStatusCode.NotFound);
            await notFoundResponse.WriteStringAsync("Result not found.");
            return notFoundResponse;
        }

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result);
        return response;
    }
}