using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Application.Common.Validator;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            // Registrera AutoMapper-profiler i Application
            services.AddAutoMapper(assembly);

            // Registrera FluentValidation validators i Application
            services.AddValidatorsFromAssembly(assembly);

            // Kör validatorer via MediatR pipeline
            services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(ValidatorBehavior<,>)
            );

            // Registrera MediatR-handlers från Application
            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(assembly));

            return services;
        }
    }
}