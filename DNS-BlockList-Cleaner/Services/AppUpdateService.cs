using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;

namespace DNS_BlockList_Cleaner.Services;

/// <summary>
/// Information about a release available on GitHub.
/// </summary>
public sealed record AppRelease(Version Version, string TagName, string DownloadUrl, string AssetName);

/// <summary>
/// Checks GitHub for newer application releases and downloads the packaged executable.
/// </summary>
public sealed class AppUpdateService : IDisposable
{
    private const string LatestReleaseUrl =
        "https://api.github.com/repos/hvkooten/dns-blocklist-cleaner/releases/latest";

    private readonly HttpClient _httpClient;
    private readonly bool _ownsClient;

    /// <summary>Creates a service with its own <see cref="HttpClient"/>.</summary>
    public AppUpdateService()
        : this(new HttpClient(), ownsClient: true)
    {
    }

    /// <summary>Creates a service using the supplied <see cref="HttpClient"/>.</summary>
    public AppUpdateService(HttpClient httpClient, bool ownsClient = false)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        _httpClient = httpClient;
        _ownsClient = ownsClient;

        // GitHub rejects API requests that do not carry a User-Agent header.
        if (!_httpClient.DefaultRequestHeaders.UserAgent.Any())
            _httpClient.DefaultRequestHeaders.UserAgent.Add(
                new ProductInfoHeaderValue("DNS-BlockList-Cleaner", GetCurrentVersion().ToString()));
    }

    /// <summary>
    /// Returns the version of the currently running application.
    /// </summary>
    public static Version GetCurrentVersion()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        string? informational = assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion;

        if (!string.IsNullOrWhiteSpace(informational))
        {
            int plusIndex = informational.IndexOf('+');
            string trimmed = plusIndex >= 0 ? informational[..plusIndex] : informational;
            if (Version.TryParse(trimmed, out Version? parsed))
                return parsed;
        }

        return assembly.GetName().Version ?? new Version(1, 0, 0);
    }

    /// <summary>
    /// Queries GitHub for the latest published release and its Windows executable asset.
    /// Returns <see langword="null"/> when no suitable release or asset is found.
    /// </summary>
    public async Task<AppRelease?> GetLatestReleaseAsync(CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await _httpClient
            .GetAsync(LatestReleaseUrl, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        await using Stream stream = await response.Content
            .ReadAsStreamAsync(cancellationToken)
            .ConfigureAwait(false);

        using JsonDocument document = await JsonDocument
            .ParseAsync(stream, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        JsonElement root = document.RootElement;

        if (!root.TryGetProperty("tag_name", out JsonElement tagElement))
            return null;

        string? tagName = tagElement.GetString();
        if (string.IsNullOrWhiteSpace(tagName)
            || !Version.TryParse(tagName.TrimStart('v', 'V'), out Version? version))
            return null;

        if (!root.TryGetProperty("assets", out JsonElement assets)
            || assets.ValueKind != JsonValueKind.Array)
            return null;

        foreach (JsonElement asset in assets.EnumerateArray())
        {
            string? name = asset.TryGetProperty("name", out JsonElement nameElement)
                ? nameElement.GetString()
                : null;
            string? url = asset.TryGetProperty("browser_download_url", out JsonElement urlElement)
                ? urlElement.GetString()
                : null;

            if (!string.IsNullOrWhiteSpace(name)
                && !string.IsNullOrWhiteSpace(url)
                && name.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
            {
                return new AppRelease(version, tagName, url, name);
            }
        }

        return null;
    }

    /// <summary>
    /// Downloads the release asset to <paramref name="destinationPath"/>, optionally
    /// reporting progress as a value between 0 and 1 (or -1 when the size is unknown).
    /// </summary>
    public async Task DownloadAsync(
        AppRelease release,
        string destinationPath,
        IProgress<double>? progress = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(release);
        if (string.IsNullOrWhiteSpace(destinationPath))
            throw new ArgumentException("Destination path is required.", nameof(destinationPath));

        using HttpResponseMessage response = await _httpClient
            .GetAsync(release.DownloadUrl, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        long? totalBytes = response.Content.Headers.ContentLength;

        await using Stream source = await response.Content
            .ReadAsStreamAsync(cancellationToken)
            .ConfigureAwait(false);

        await using FileStream target = new(
            destinationPath, FileMode.Create, FileAccess.Write, FileShare.None);

        byte[] buffer = new byte[81920];
        long totalRead = 0;
        int read;

        while ((read = await source.ReadAsync(buffer, cancellationToken).ConfigureAwait(false)) > 0)
        {
            await target.WriteAsync(buffer.AsMemory(0, read), cancellationToken).ConfigureAwait(false);
            totalRead += read;

            if (progress is not null)
                progress.Report(totalBytes is > 0 ? (double)totalRead / totalBytes.Value : -1);
        }

        progress?.Report(1);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_ownsClient)
            _httpClient.Dispose();
    }
}
