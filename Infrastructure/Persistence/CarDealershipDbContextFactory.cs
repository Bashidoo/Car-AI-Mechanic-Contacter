using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CarDealership.Infrastructure.Persistence
{
    public class CarDealershipDbContextFactory
        : IDesignTimeDbContextFactory<CarDealershipDbContext>
    {
        public CarDealershipDbContext CreateDbContext(string[] args)
        {
            // Ensure the base path is the Infrastructure folder
            var config = new ConfigurationBuilder()
                
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var builder = new DbContextOptionsBuilder<CarDealershipDbContext>();
            builder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            return new CarDealershipDbContext(builder.Options);
        }
    }
}
