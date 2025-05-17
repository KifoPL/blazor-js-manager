namespace BlazorJsManager;

/// <summary>
/// Represents options for configuring the JavaScript module resolver.
/// </summary>
public class JsModuleResolverOptions
{
    public const string DefaultOptionsPath = "JsModuleResolver";

    /// <summary>
    /// Gets or sets the base path to the folder containing JavaScript module files.
    /// </summary>
    /// <remarks>
    /// Default is "/assets/js/".
    /// </remarks>
    public string ModulePath { get; set; } = "/assets/js/";

    /// <summary>
    /// Gets or sets the file extension used for JavaScript modules.
    /// </summary>
    /// <remarks>
    /// Default is ".min.js".
    /// </remarks>
    public string FileType { get; set; } = ".min.js";
}