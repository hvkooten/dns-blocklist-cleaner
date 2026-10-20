using System.Net;

namespace DNS_BlockList_Cleaner.Tests;

public class AppUpdateServiceTests
{
    [Fact]
    public void GetCurrentVersion_ReturnsParsableVersion()
    {
        Version version = AppUpdateService.GetCurrentVersion();

        version.Should().NotBeNull();
        version.Major.Should().BeGreaterThanOrEqualTo(0);
    }

    [Fact]
    public async Task GetLatestReleaseAsync_ParsesTagAndExecutableAsset()
    {
        const string json =
            """
            {
              "tag_name": "v2.5.0",
              "assets": [
                { "name": "notes.txt", "browser_download_url": "https://example.com/notes.txt" },
                { "name": "DNS-BlockList-Cleaner.exe", "browser_download_url": "https://example.com/app.exe" }
              ]
            }
            """;

        using var handler = new StubHttpMessageHandler(StubHttpMessageHandler.Json(json));
        using var httpClient = new HttpClient(handler);
        using var service = new AppUpdateService(httpClient);

        AppRelease? release = await service.GetLatestReleaseAsync();

        release.Should().NotBeNull();
        release!.Version.Should().Be(new Version(2, 5, 0));
        release.TagName.Should().Be("v2.5.0");
        release.AssetName.Should().Be("DNS-BlockList-Cleaner.exe");
        release.DownloadUrl.Should().Be("https://example.com/app.exe");
    }

    [Fact]
    public async Task GetLatestReleaseAsync_ReturnsNull_WhenNoExecutableAsset()
    {
        const string json =
            """
            {
              "tag_name": "v2.5.0",
              "assets": [
                { "name": "notes.txt", "browser_download_url": "https://example.com/notes.txt" }
              ]
            }
            """;

        using var handler = new StubHttpMessageHandler(StubHttpMessageHandler.Json(json));
        using var httpClient = new HttpClient(handler);
        using var service = new AppUpdateService(httpClient);

        (await service.GetLatestReleaseAsync()).Should().BeNull();
    }

    [Fact]
    public async Task GetLatestReleaseAsync_ReturnsNull_WhenTagIsNotAVersion()
    {
        const string json =
            """
            { "tag_name": "nightly", "assets": [] }
            """;

        using var handler = new StubHttpMessageHandler(StubHttpMessageHandler.Json(json));
        using var httpClient = new HttpClient(handler);
        using var service = new AppUpdateService(httpClient);

        (await service.GetLatestReleaseAsync()).Should().BeNull();
    }

    [Fact]
    public void Constructor_AddsUserAgentHeader_WhenMissing()
    {
        using var httpClient = new HttpClient(new StubHttpMessageHandler(new HttpResponseMessage()));

        using var service = new AppUpdateService(httpClient);

        httpClient.DefaultRequestHeaders.UserAgent.Should().NotBeEmpty();
    }

    [Fact]
    public async Task DownloadAsync_WritesContentAndReportsCompletion()
    {
        byte[] payload = [1, 2, 3, 4, 5];
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new ByteArrayContent(payload)
        };

        using var handler = new StubHttpMessageHandler(response);
        using var httpClient = new HttpClient(handler);
        using var service = new AppUpdateService(httpClient);

        var release = new AppRelease(new Version(2, 5, 0), "v2.5.0",
            "https://example.com/app.exe", "app.exe");
        string path = Path.Combine(Path.GetTempPath(), $"upd-{Guid.NewGuid():N}.exe");
        var progressValues = new List<double>();
        var progress = new Progress<double>(progressValues.Add);

        await service.DownloadAsync(release, path, progress);

        (await File.ReadAllBytesAsync(path)).Should().Equal(payload);
    }

    [Fact]
    public async Task DownloadAsync_ThrowsForEmptyDestination()
    {
        using var httpClient = new HttpClient(new StubHttpMessageHandler(new HttpResponseMessage()));
        using var service = new AppUpdateService(httpClient);
        var release = new AppRelease(new Version(1, 0), "v1.0", "https://example.com/app.exe", "app.exe");

        await FluentActions
            .Awaiting(() => service.DownloadAsync(release, "   "))
            .Should().ThrowAsync<ArgumentException>();
    }
}
