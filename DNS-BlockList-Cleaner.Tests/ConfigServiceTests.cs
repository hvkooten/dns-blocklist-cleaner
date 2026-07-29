namespace DNS_BlockList_Cleaner.Tests;

public class ConfigServiceTests
{
    [Fact]
    public async Task LoadAsync_ReturnsDefault_WhenFileMissing()
    {
        string path = Path.Combine(Path.GetTempPath(), $"cfg-{Guid.NewGuid():N}.json");

        AppConfig config = await ConfigService.LoadAsync(path);

        config.Should().BeEquivalentTo(new AppConfig());
    }

    [Fact]
    public async Task SaveThenLoad_RoundTripsConfig()
    {
        string path = Path.Combine(Path.GetTempPath(), $"cfg-{Guid.NewGuid():N}.json");
        var original = new AppConfig
        {
            SourceUrl = "https://example.com/list.txt",
            OutputFileName = "clean.txt",
            RemovalRules =
            [
                new RemovalRule { Pattern = "ads", MatchType = MatchType.Contains },
                new RemovalRule { Pattern = "*.tracker.com", MatchType = MatchType.Wildcard, Enabled = false }
            ]
        };

        try
        {
            await ConfigService.SaveAsync(path, original);
            AppConfig loaded = await ConfigService.LoadAsync(path);

            loaded.SourceUrl.Should().Be(original.SourceUrl);
            loaded.OutputFileName.Should().Be(original.OutputFileName);
            loaded.RemovalRules.Should().HaveCount(2);
            loaded.RemovalRules[1].Enabled.Should().BeFalse();
            loaded.RemovalRules[1].MatchType.Should().Be(MatchType.Wildcard);
        }
        finally
        {
            File.Delete(path);
        }
    }
}
