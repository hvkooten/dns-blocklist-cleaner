using System.Net;

namespace DNS_BlockList_Cleaner.Tests;

public class HaGeZiUpdateServiceTests
{
    private const string Sample =
        "# Title: HaGeZi's Pro DNS Blocklist\n" +
        "# Version: 2026.0727.0750.43\n" +
        "\n" +
        "ads.example.com\n" +
        "tracker.example.net\n";

    [Fact]
    public async Task DownloadAsync_ParsesDownloadedBlocklist()
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(Sample)
        };

        using var handler = new StubHttpMessageHandler(response);
        using var httpClient = new HttpClient(handler);
        using var service = new HaGeZiUpdateService(httpClient);

        BlocklistDocument doc = await service.DownloadAsync("https://example.com/list.txt");

        doc.Version.Should().Be("2026.0727.0750.43");
        doc.Domains.Should().Equal("ads.example.com", "tracker.example.net");
        handler.Requests.Should().ContainSingle()
            .Which.Should().Be(new Uri("https://example.com/list.txt"));
    }

    [Fact]
    public async Task DownloadAsync_ThrowsForEmptySourceUrl()
    {
        using var httpClient = new HttpClient(new StubHttpMessageHandler(new HttpResponseMessage()));
        using var service = new HaGeZiUpdateService(httpClient);

        await FluentActions
            .Awaiting(() => service.DownloadAsync("   "))
            .Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task DownloadAsync_ThrowsForUnsuccessfulStatus()
    {
        var response = new HttpResponseMessage(HttpStatusCode.NotFound);

        using var handler = new StubHttpMessageHandler(response);
        using var httpClient = new HttpClient(handler);
        using var service = new HaGeZiUpdateService(httpClient);

        await FluentActions
            .Awaiting(() => service.DownloadAsync("https://example.com/missing.txt"))
            .Should().ThrowAsync<HttpRequestException>();
    }

    [Fact]
    public void Constructor_ThrowsForNullHttpClient()
    {
        FluentActions.Invoking(() => new HaGeZiUpdateService(null!))
            .Should().Throw<ArgumentNullException>();
    }
}
