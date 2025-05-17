using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace BlazorJsManager;

/// <summary>
/// Resolves and manages JavaScript module references for Blazor applications.
/// </summary>
internal class JsModuleResolver(
    IJSRuntime jsRuntime,
    ILogger<JsModuleResolver> logger,
    IOptions<JsModuleResolverOptions> options)
    : IJsModuleResolver, IAsyncDisposable
{
    private readonly ConcurrentDictionary<string, SemaphoreSlim> _locks = new();
    private readonly ConcurrentDictionary<string, IJSObjectReference> _modules = new();
    private readonly JsModuleResolverOptions _options = options.Value;

    public async ValueTask DisposeAsync()
    {
        await Parallel.ForEachAsync(_modules.Values.AsEnumerable(),
            async (module, _) => await module.DisposeAsync());

        _modules.Clear();
        _locks.Clear();

        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Resolves a JavaScript module by name, importing it if not already loaded.
    /// Ensures thread safety and prevents duplicate imports using a semaphore.
    /// </summary>
    /// <param name="moduleName">The name of the JavaScript module to resolve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An <see cref="IJSObjectReference"/> representing the imported module.</returns>
    public async Task<IJSObjectReference> ResolveModuleAsync(string moduleName,
        CancellationToken cancellationToken = default)
    {
        if (!_locks.TryGetValue(moduleName, out var semaphoreSlim))
        {
            semaphoreSlim = new SemaphoreSlim(1, 1);
            _locks.TryAdd(moduleName, semaphoreSlim);
        }

        try
        {
            await semaphoreSlim.WaitAsync(cancellationToken);

            if (_modules.TryGetValue(moduleName, out var reference)) return reference;

            logger.LogDebug("Importing module {ModuleName}", moduleName);

            reference = await jsRuntime.InvokeAsync<IJSObjectReference>("import", cancellationToken,
                $"{_options.ModulePath}{moduleName}{_options.FileType}");
            _modules.TryAdd(moduleName, reference);
            return reference;
        }
        finally
        {
            if (semaphoreSlim.CurrentCount == 1) _locks.TryRemove(moduleName, out _);
            semaphoreSlim.Release();
        }
    }
}