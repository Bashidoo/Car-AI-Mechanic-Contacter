using Application.Interfaces.CarInterface;
using Application.Interfaces.CarIssueInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CarDealership.Infrastructure.Persistence;
using CarDealership.Infrastructure.Repositories;
using CarDealership.Infrastructure.Security;
using Application.Interfaces.IAppDbContext;
using CarDealership.Application.Interfaces.Userinterface;
using Infrastructure.Repositories; 

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Registrera DbContext
            services.AddDbContext<CarDealershipDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Repository för användare
            services.AddScoped<IUserRepository, UserRepository>();

            // JWT-tjänst
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            // Interface till DbContext för testbarhet
            services.AddScoped<IAppDbContext, CarDealershipDbContext>();

            // 🔧 Repository för MediatR-handlers
            services.AddScoped<ICarIssueRepository, CarIssueRepository>();
            
            services.AddScoped<ICarRepository, CarRepository>(); 


            return services;
        }
    }
}