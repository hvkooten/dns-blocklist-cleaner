using System.Text.Json;
using DNS_BlockList_Cleaner.Models;

namespace DNS_BlockList_Cleaner.Services;

/// <summary>
/// Loads and saves the <see cref="AppConfig"/> as JSON.
/// </summary>
public static class ConfigService
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true
    };

    /// <summary>
    /// Loads the config from <paramref name="path"/>, or returns a default
    /// config when the file does not exist.
    /// </summary>
    public static async Task<AppConfig> LoadAsync(
        string path, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path is required.", nameof(path));

        if (!File.Exists(path))
            return new AppConfig();

        await using FileStream stream = File.OpenRead(path);
        AppConfig? config = await JsonSerializer
            .DeserializeAsync<AppConfig>(stream, Options, cancellationToken)
            .ConfigureAwait(false);

        return config ?? new AppConfig();
    }

    /// <summary>Saves <paramref name="config"/> to <paramref name="path"/> as JSON.</summary>
    public static async Task SaveAsync(
        string path, AppConfig config, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path is required.", nameof(path));
        ArgumentNullException.ThrowIfNull(config);

        await using FileStream stream = File.Create(path);
        await JsonSerializer
            .SerializeAsync(stream, config, Options, cancellationToken)
            .ConfigureAwait(false);
    }
}
