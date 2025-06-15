using Microsoft.Extensions.DependencyInjection;

using MediatR;

using Application.Common.Validator;

using FluentValidation;



namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            var assembly = typeof(DependencyInjection).Assembly;

            services.AddAutoMapper(assembly);

            services.AddValidatorsFromAssembly(assembly);

            services.AddTransient(                              // This runs validator with MediaTR
              typeof(IPipelineBehavior<,>),
              typeof(ValidatorBehavior<,>)
          );




       
            services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(assembly));
            services.AddAutoMapper(assembly);
            return services;
        }
    }
}
