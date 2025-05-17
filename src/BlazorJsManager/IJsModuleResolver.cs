using Microsoft.JSInterop;

namespace BlazorJsManager;

/// <summary>
/// Contains the interface definition for resolving JavaScript modules in Blazor applications.
/// </summary>
internal interface IJsModuleResolver
{
    /// <summary>
    /// Resolves a JavaScript module by name, importing it if not already loaded.
    /// Ensures thread safety and prevents duplicate imports using a semaphore.
    /// </summary>
    /// <param name="moduleName">The name of the JavaScript module to resolve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An <see cref="IJSObjectReference"/> representing the imported module.</returns>
    Task<IJSObjectReference> ResolveModuleAsync(string moduleName,
        CancellationToken cancellationToken = default);
}