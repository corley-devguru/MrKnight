using Microsoft.EntityFrameworkCore;
using MrKnight.Core.Models;
using MrKnight.Core.Repositories.Interfaces;
using MrKnight.Infrastructure.Data;

namespace MrKnight.Infrastructure.Repositories;

public class KnightPathRequestRepository : BaseRepository<KnightPathRequest, string>, IKnightPathRequestRepository
{
    public KnightPathRequestRepository(KnightPathDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<KnightPathRequest>> GetAllAsync()
    {
        return await _context.KnightPathRequests.ToListAsync();
    }

    public async Task DeleteAsync(string operationId)
    {
        var request = await _context.KnightPathRequests.FindAsync(operationId);
        if (request != null)
        {
            _context.KnightPathRequests.Remove(request);
            await _context.SaveChangesAsync();
        }
    }
}