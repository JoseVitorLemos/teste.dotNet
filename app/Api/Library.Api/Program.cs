using Library.IoC.Application;
using Library.IoC.CrossCutting;
using Library.IoC.Infraestructure;
using Library.IoC.Shared;
using Library.Shared.Middlewares;
using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        IServiceCollection services = builder.Services;

        services.AddApplication();
        services.AddCrossCutting();
        services.AddInfraestruucture();
        services.AddShared();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        builder.Host.UseSerilog();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<GlobalExceptionHandling>();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}