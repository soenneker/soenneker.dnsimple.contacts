using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.DNSimple.Contacts.Abstract;
using Soenneker.DNSimple.OpenApiClientUtil.Registrars;

namespace Soenneker.DNSimple.Contacts.Registrars;

/// <summary>
/// An async thread-safe singleton for DNSimple OpenApiClient's Contacts API
/// </summary>
public static class DNSimpleContactsUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="IDNSimpleContactsUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddDNSimpleContactsUtilAsSingleton(this IServiceCollection services)
    {
        services.AddDNSimpleOpenApiClientUtilAsSingleton().TryAddSingleton<IDNSimpleContactsUtil, DNSimpleContactsUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IDNSimpleContactsUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddDNSimpleContactsUtilAsScoped(this IServiceCollection services)
    {
        services.AddDNSimpleOpenApiClientUtilAsSingleton().TryAddScoped<IDNSimpleContactsUtil, DNSimpleContactsUtil>();

        return services;
    }
}