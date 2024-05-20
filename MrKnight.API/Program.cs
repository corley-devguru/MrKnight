using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MrKnight.Core.Repositories.Interfaces;
using MrKnight.Core.Services;
using MrKnight.Infrastructure.Data;
using MrKnight.Infrastructure.Repositories;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDbContext<KnightPathDbContext>(options =>
                options.UseInMemoryDatabase("KnightPathDb"));

        services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
        services.AddScoped<IKnightPathRequestRepository, KnightPathRequestRepository>();
        services.AddScoped<IKnightPathResultRepository, KnightPathResultRepository>();

        services.AddScoped<IKnightPathService, KnightPathService>();
    })
    .Build();

host.Run();