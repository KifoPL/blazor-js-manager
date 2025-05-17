# blazor-js-manager

[![NuGet Version](https://img.shields.io/nuget/v/BlazorJsManager.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/BlazorJsManager)
[![Build Status](https://github.com/OpDev/blazor-js-manager/actions/workflows/ci.yml/badge.svg)](https://github.com/OpDev/blazor-js-manager/actions/workflows/ci.yml)

Small package that adds services that make managing and invoking JS scripts in Blazor a breeze.

## Features
- Simple JS module loading and invocation for Blazor (WASM & Server)
- Dependency injection for JS modules
- Type-safe JS interop
- Automatic module disposal

## Installation

Install via NuGet:

```shell
 dotnet add package BlazorJsManager
```

## Usage

1. **Register services** in your `Program.cs`:

```csharp
builder.Services.AddJsModuleResolver();
```

2. **Inject and use the resolver** in your component or service:

```csharp
@inject IJsModuleResolver JsModuleResolver

@code {
    private IJSObjectReference? _module;

    protected override async Task OnInitializedAsync()
    {
        _module = await JsModuleResolver.ResolveAsync("./js/myModule.js");
        await _module.InvokeVoidAsync("myJsFunction");
    }

    public async ValueTask DisposeAsync()
    {
        if (_module is not null)
            await _module.DisposeAsync();
    }
}
```

3. **Options**: You can configure options via `AddJsModuleResolver(options => { ... })`.

## API
- `IJsModuleResolver.ResolveAsync(string path)` – Loads a JS module and returns an `IJSObjectReference`.
- Extension methods for common scenarios (see source for details).

## License

MIT
