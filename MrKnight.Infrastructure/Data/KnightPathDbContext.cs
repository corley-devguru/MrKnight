using Microsoft.EntityFrameworkCore;
using MrKnight.Core.Models;

namespace MrKnight.Infrastructure.Data;

public class KnightPathDbContext : DbContext
{
    public KnightPathDbContext(DbContextOptions<KnightPathDbContext> options) : base(options) { }

    public DbSet<KnightPathRequest> KnightPathRequests { get; set; }
    public DbSet<KnightPathResult> KnightPathResults { get; set; }
}