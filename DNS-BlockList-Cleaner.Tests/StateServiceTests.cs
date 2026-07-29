namespace DNS_BlockList_Cleaner.Tests;

public class StateServiceTests
{
    [Fact]
    public async Task LoadAsync_ReturnsEmpty_WhenFileMissing()
    {
        string path = Path.Combine(Path.GetTempPath(), $"state-{Guid.NewGuid():N}.json");

        AppState state = await StateService.LoadAsync(path);

        state.Should().BeEquivalentTo(new AppState());
    }

    [Fact]
    public async Task SaveThenLoad_RoundTripsState()
    {
        string path = Path.Combine(Path.GetTempPath(), $"state-{Guid.NewGuid():N}.json");
        var original = new AppState
        {
            LastProcessedVersion = "2026.0727.0750.43",
            LastProcessedModified = "27 Jul 2026 07:50 UTC",
            PreviousDomains = ["a.com", "b.com"]
        };

        try
        {
            await StateService.SaveAsync(path, original);
            AppState loaded = await StateService.LoadAsync(path);

            loaded.LastProcessedVersion.Should().Be(original.LastProcessedVersion);
            loaded.LastProcessedModified.Should().Be(original.LastProcessedModified);
            loaded.PreviousDomains.Should().Equal(original.PreviousDomains);
        }
        finally
        {
            File.Delete(path);
        }
    }
}
