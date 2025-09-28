using Library.Shared.AppSettings;
using Library.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Library.Infraestructure.AppDbContext;
using Library.Infraestructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Library.IoC.Infraestructure;

public static class InfraestructureIoC
{
    public static IServiceCollection AddInfraestruucture(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddDataContext();
        return services;
    }

    public static IServiceCollection AddDataContext(this IServiceCollection services)
    {
        services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(CustomConfiguration.ConnectionStrings.DefaultConnection,
                    x => x.MigrationsAssembly(typeof(DataContext).Assembly.FullName)));

        return services;
    }
}