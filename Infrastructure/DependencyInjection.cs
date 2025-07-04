// Infrastructure/DependencyInjection.cs

using CarDealership.Application.Interfaces.IAppDbContext;          // IAppDbContext
using CarDealership.Application.Interfaces.CarIssueInterface;      // ICarIssueRepository
using CarDealership.Infrastructure.Persistence;      // CarDealershipDbContext
using CarDealership.Infrastructure.Repositories;    // UserRepository, CarIssueRepository
using CarDealership.Infrastructure.Security;        // JwtTokenService
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CarDealership.Application.Interfaces.Userinterface;

namespace CarDealership.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // 1) EF Core DbContext
            services.AddDbContext<CarDealershipDbContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // 2) Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICarIssueRepository, CarIssueRepository>();

            // 3) JWT token service
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            // 4) Expose the DbContext via an interface for testing
            services.AddScoped<IAppDbContext, CarDealershipDbContext>();

            return services;
        }
    }
}
