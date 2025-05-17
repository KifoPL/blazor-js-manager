using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BlazorJsManager;

/// <summary>
/// Extension methods for registering the JS module resolver.
/// </summary>
public static class JsModuleResolverExtensions
{
    /// <summary>
    /// Registers the <see cref="IJsModuleResolver"/> service and configures <see cref="JsModuleResolverOptions"/>.
    /// </summary>
    /// <param name="services">The service collection to add the resolver to.</param>
    /// <param name="optionsPath">The configuration section path for <see cref="JsModuleResolverOptions"/>. If null, uses the default path.</param>
    /// <param name="postConfigure">An optional delegate to further configure <see cref="JsModuleResolverOptions"/> after binding.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddJsModuleResolver(this IServiceCollection services, string? optionsPath = null,
        Action<JsModuleResolverOptions>? postConfigure = null)
    {
        services.AddOptions<JsModuleResolverOptions>()
            .BindConfiguration(optionsPath ?? JsModuleResolverOptions.DefaultOptionsPath)
            .PostConfigure(postConfigure ?? (_ => { }));

        services.TryAddScoped<IJsModuleResolver, JsModuleResolver>();

        return services;
    }
}
