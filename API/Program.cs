using Infrastructure;
using MediatR;
using Application;
using Application.Cars.Handlers.QueryHandler;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CarDealership.Domain.Entities;
using CarDealership.Infrastructure.Persistence;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add config from appsettings.json
            var configuration = builder.Configuration;

            // Add DbContext
            builder.Services.AddDbContext<CarDealershipDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Add Identity-hasher
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            // Add custom dependencies
            builder.Services.AddInfrastructure(configuration);

            // Add MediatR
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(GetAllCarsQueryHandler).Assembly);
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();

            // Run DB seeder (Bogus)
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<CarDealershipDbContext>();
                DataSeederCars.SeedAsync(dbContext).GetAwaiter().GetResult();
            }

            app.Run();
        }
    }
}