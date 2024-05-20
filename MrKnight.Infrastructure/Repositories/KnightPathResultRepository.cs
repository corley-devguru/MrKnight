using Microsoft.EntityFrameworkCore;
using MrKnight.Core.Models;
using MrKnight.Core.Repositories.Interfaces;
using MrKnight.Infrastructure.Data;

namespace MrKnight.Infrastructure.Repositories;

public class KnightPathResultRepository : BaseRepository<KnightPathResult, string>, IKnightPathResultRepository
{
    public KnightPathResultRepository(KnightPathDbContext context) : base(context)
    {
    }

    public async Task<KnightPathResult?> GetByStartAndEndPointsAsync(string start, string end)
    {
        return await _context.KnightPathResults.Where(k => k.Starting == start && k.Ending == end).FirstOrDefaultAsync();
    }
}