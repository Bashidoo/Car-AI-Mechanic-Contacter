using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.IAppDbContext;
using CarDealership.Application.Interfaces.Userinterface;
using CarDealership.Infrastructure.Persistence;
using CarDealership.Infrastructure.Repositories;
using CarDealership.Infrastructure.Security;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CarDealershipDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            services.AddScoped<IAppDbContext, CarDealershipDbContext>();

            return services;
        }
    }
}
