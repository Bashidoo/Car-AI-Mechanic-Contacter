using Bogus;
using CarDealership.Infrastructure.Persistence;
using CarDealership.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealership.Infrastructure.Data
{
    public static class DataSeederCarIssues
    {
        public static async Task SeedAsync(CarDealershipDbContext context)
        {
            // Undvik duplicering
            if (await context.CarIssues.AnyAsync()) return;

            // Se till att det finns bilar att koppla till
            var cars = await context.Cars.ToListAsync();
            if (!cars.Any()) return;

            var issueFaker = new Faker<CarIssue>()
                .RuleFor(i => i.Description, f => f.Lorem.Sentence(5))
                .RuleFor(i => i.OptionalComment, f => f.Lorem.Sentence(3))
                .RuleFor(i => i.AIAnalysis, f => f.Lorem.Paragraph())
                .RuleFor(i => i.CreatedAt, f => f.Date.Past())
                .RuleFor(i => i.CarId, f => f.PickRandom(cars).CarId);

            var fakeIssues = issueFaker.Generate(20);

            await context.CarIssues.AddRangeAsync(fakeIssues);
            await context.SaveChangesAsync();
        }
    }
}