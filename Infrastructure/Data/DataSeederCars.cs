using Bogus;
using CarDealership.Infrastructure.Persistence;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public static class DataSeederCars
{
    public static async Task SeedAsync(CarDealershipDbContext context)
    {
        if (await context.Cars.AnyAsync()) return; // Undvik duplicering

        var carFaker = new Faker<Car>()
            .RuleFor(c => c.Model, f => f.Vehicle.Model())
            .RuleFor(c => c.RegistrationNumber, f => f.Vehicle.Vin())
            .RuleFor(c => c.ImagePath, f => "placeholder.jpg"); // tillfälligt dummyvärde

        var fakeCars = carFaker.Generate(10);

        await context.Cars.AddRangeAsync(fakeCars);
        await context.SaveChangesAsync();
    }
}