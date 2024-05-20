using Microsoft.Azure.Functions.Worker;
using MrKnight.Core.Services;
using Microsoft.Extensions.Logging;

namespace MrKnight.API.Functions;

public class ProcessRequestFunction
{
    private readonly ILogger<ProcessRequestFunction> _logger;
    private readonly IKnightPathService _knightPathService;

    public ProcessRequestFunction(IKnightPathService knightPathService, ILogger<ProcessRequestFunction> logger)
    {
        _knightPathService = knightPathService;
        _logger = logger;
    }

    [Function("ProcessRequestFunction")]
    public async Task Run([TimerTrigger("*/30 * * * * *")] TimerInfo myTimer, FunctionContext context)
    {
        _logger.LogInformation("Processing pending knight path requests.");
        await _knightPathService.ProcessRequestsAsync();
    }
}