using CSM.UseCases.Abstractions.Behaviours;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CSM.UseCases;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // To register MediaR handlers
        var assembly = typeof(DependencyInjection).Assembly;
        
        // Add Fluent Validation
        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);
        
        // Add pipeline behaviour for validation
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            
            config.AddOpenBehavior(typeof(ValidationPipelineBehaviour<,>));

        });
        
        return services;
    }
}