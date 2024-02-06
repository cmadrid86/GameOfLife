using DomainObjects.ValueObjects;
using GameOfLifeApi.Filters;
using Hellang.Middleware.ProblemDetails.Mvc;
using Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Repositories;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameOfLifeApi.Extensions;

public static class CompositionExtensions
{
    public static IServiceCollection ConfigureMvcOptions(this IServiceCollection services)
    {
        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
        });

        services.AddControllers(config =>
        {
            config.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
            config.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
            config.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));

            config.ReturnHttpNotAcceptable = true;

            config.OutputFormatters
                .OfType<SystemTextJsonOutputFormatter>()
                .FirstOrDefault()?
                .SupportedMediaTypes
                .Remove("text/json");

            config.Filters.Add<UnhandledExceptionFilter>();
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        })
        .AddProblemDetailsConventions();

        return services;
    }

    public static IServiceCollection ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection("Settings");
        services.Configure<Settings>(settings);

        return services;
    }

    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(CompositionExtensions));

        // Register default implementations
        services.RegisterDefaultManagers();
        services.RegisterDefaultRepositories(configuration);

        return services;
    }
}
