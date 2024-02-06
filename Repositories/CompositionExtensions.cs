using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Microsoft.Extensions.DependencyInjection;
using Amazon.Runtime;
using DomainObjects.ValueObjects;
using Microsoft.Extensions.Configuration;

namespace Repositories;

public static class CompositionExtensions
{
    public static IServiceCollection RegisterDefaultRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(CompositionExtensions));

        // Register default implementations
        services.AddScoped<IGameRepository, GameRepository>();
        services.RegisterAwsDynamoDb(configuration);

        return services;
    }

    private static void RegisterAwsDynamoDb(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection("Settings").Get<Settings>();

        services.AddScoped<IAmazonDynamoDB>(sp =>
        {
            var credentials = new BasicAWSCredentials(settings.DynamoDbAccessKey, settings.DynamoDbSecretKey);
            var config = new AmazonDynamoDBConfig
            {
                ServiceURL = settings.DynamoDbUrl
            };
            return new AmazonDynamoDBClient(credentials, config);
        });

        services.AddScoped<IDynamoDBContext, DynamoDBContext>();
    }
}