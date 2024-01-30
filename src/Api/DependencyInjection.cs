using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
#if (UseApiOnly)
using NSwag;
using NSwag.Generation.Processors.Security;
#endif

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        //services.AddScoped<IUser, CurrentUser>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        //services.AddExceptionHandler<CustomExceptionHandler>();

        //services.AddRazorPages();

        //services.AddScoped(provider =>
        //{
        //    var validationRules = provider.GetService<IEnumerable<FluentValidationRule>>();
        //    var loggerFactory = provider.GetService<ILoggerFactory>();

        //    return new FluentValidationSchemaProcessor(provider, validationRules, loggerFactory);
        //});

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddEndpointsApiExplorer();


        return services;
    }

    //public static IServiceCollection AddKeyVaultIfConfigured(this IServiceCollection services, ConfigurationManager configuration)
    //{
    //    var keyVaultUri = configuration["KeyVaultUri"];
    //    if (!string.IsNullOrWhiteSpace(keyVaultUri))
    //    {
    //        configuration.AddAzureKeyVault(
    //            new Uri(keyVaultUri),
    //            new DefaultAzureCredential());
    //    }

    //    return services;
    //}
}
