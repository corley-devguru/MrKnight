using MrKnight.Core.Models;

namespace MrKnight.Core.Services;

public interface IKnightPathService
{
    Task<string> CreateRequestAsync(string source, string target);
    Task ProcessRequestsAsync();
    Task<KnightPathResult?> GetResultAsync(string operationId);
}