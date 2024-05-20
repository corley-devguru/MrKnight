using MrKnight.Core.Models;

namespace MrKnight.Core.Repositories.Interfaces;

public interface IKnightPathRequestRepository : IBaseRepository<KnightPathRequest, string>
{
    Task DeleteAsync(string operationId);
    Task<IEnumerable<KnightPathRequest>> GetAllAsync();
}