using DNS_BlockList_Cleaner.Models;

namespace DNS_BlockList_Cleaner.Services;

/// <summary>
/// Downloads the latest HaGeZi blocklist directly from the raw main branch URL
/// and parses its version metadata.
/// </summary>
public sealed class HaGeZiUpdateService : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly bool _ownsClient;

    /// <summary>Creates a service with its own <see cref="HttpClient"/>.</summary>
    public HaGeZiUpdateService()
        : this(new HttpClient(), ownsClient: true)
    {
    }

    /// <summary>Creates a service using the supplied <see cref="HttpClient"/>.</summary>
    public HaGeZiUpdateService(HttpClient httpClient, bool ownsClient = false)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        _httpClient = httpClient;
        _ownsClient = ownsClient;
    }

    /// <summary>
    /// Downloads and parses the blocklist at <paramref name="sourceUrl"/>.
    /// </summary>
    public async Task<BlocklistDocument> DownloadAsync(
        string sourceUrl, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(sourceUrl))
            throw new ArgumentException("Source URL is required.", nameof(sourceUrl));

        using HttpResponseMessage response = await _httpClient
            .GetAsync(sourceUrl, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        string content = await response.Content
            .ReadAsStringAsync(cancellationToken)
            .ConfigureAwait(false);

        return BlocklistService.Parse(content);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_ownsClient)
            _httpClient.Dispose();
    }
}
