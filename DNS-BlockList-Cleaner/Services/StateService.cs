using System.Text.Json;
using DNS_BlockList_Cleaner.Models;

namespace DNS_BlockList_Cleaner.Services;

/// <summary>
/// Persists the <see cref="AppState"/> (memory of the previously processed
/// blocklist) as JSON so new versions can be diffed against it.
/// </summary>
public static class StateService
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true
    };

    /// <summary>
    /// Loads the state from <paramref name="path"/>, or returns an empty state
    /// when the file does not exist.
    /// </summary>
    public static async Task<AppState> LoadAsync(
        string path, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path is required.", nameof(path));

        if (!File.Exists(path))
            return new AppState();

        await using FileStream stream = File.OpenRead(path);
        AppState? state = await JsonSerializer
            .DeserializeAsync<AppState>(stream, Options, cancellationToken)
            .ConfigureAwait(false);

        return state ?? new AppState();
    }

    /// <summary>Saves <paramref name="state"/> to <paramref name="path"/> as JSON.</summary>
    public static async Task SaveAsync(
        string path, AppState state, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path is required.", nameof(path));
        ArgumentNullException.ThrowIfNull(state);

        await using FileStream stream = File.Create(path);
        await JsonSerializer
            .SerializeAsync(stream, state, Options, cancellationToken)
            .ConfigureAwait(false);
    }
}
