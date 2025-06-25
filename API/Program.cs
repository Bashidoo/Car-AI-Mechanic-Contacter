using Infrastructure;
using Application;
using CarDealership.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using CarDealership.Domain.Entities;
using Infrastructure.Data;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;

            // Identity-hasher
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            // 🔧 Registrera alla dependencies
            builder.Services.AddInfrastructure(configuration);
            builder.Services.AddApplication();

            // Swagger, controllers, endpoints
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            // Seed testdata
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<CarDealershipDbContext>();
                DataSeederCars.SeedAsync(dbContext).GetAwaiter().GetResult();
            }

            app.Run();
        }
    }
}