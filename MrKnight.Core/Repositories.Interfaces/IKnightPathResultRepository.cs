using MrKnight.Core.Models;

namespace MrKnight.Core.Repositories.Interfaces;

public interface IKnightPathResultRepository
{
    Task AddAsync(KnightPathResult result);
    Task<KnightPathResult?> GetAsync(string operationId);
    Task<KnightPathResult?> GetByStartAndEndPointsAsync(string start, string end);
}