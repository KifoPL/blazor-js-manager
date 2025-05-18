# blazor-js-manager

Latest version:

[![NuGet Version](https://img.shields.io/nuget/v/BlazorJsManager.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/BlazorJsManager)
![GitHub commits since latest release](https://img.shields.io/github/commits-since/KifoPL/blazor-js-manager/latest)

Status:

[![CodeQL](https://github.com/KifoPL/blazor-js-manager/actions/workflows/github-code-scanning/codeql/badge.svg)](https://github.com/KifoPL/blazor-js-manager/actions/workflows/github-code-scanning/codeql)
[![Dependabot Updates](https://github.com/KifoPL/blazor-js-manager/actions/workflows/dependabot/dependabot-updates/badge.svg)](https://github.com/KifoPL/blazor-js-manager/actions/workflows/dependabot/dependabot-updates)


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
        _module = await JsModuleResolver.ResolveAsync("myModule");
        await _module.InvokeVoidAsync("myJsFunction");
    }
}
```

3. **Options**: You can configure options via `AddJsModuleResolver(configure: options => { ... })`.

## API
- `IJsModuleResolver.ResolveAsync(string path)` – Loads a JS module and returns an `IJSObjectReference`.
- Extension methods for common scenarios (see source for details).

## License ![GitHub License](https://img.shields.io/github/license/KifoPL/blazor-js-manager)

See [License](LICENSE) for details.
