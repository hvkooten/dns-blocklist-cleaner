namespace DNS_BlockList_Cleaner.Tests;

public class BlocklistServiceTests
{
    private const string Sample =
        "# Title: HaGeZi's Pro DNS Blocklist\n" +
        "# Last modified: 27 Jul 2026 07:50 UTC\n" +
        "# Version: 2026.0727.0750.43\n" +
        "\n" +
        "ads.example.com\n" +
        "tracker.example.net\n" +
        "ads.example.com\n" +   // duplicate, should be ignored
        "  spaced.example.org  \n";

    [Fact]
    public void Parse_ExtractsHeaderMetadata()
    {
        BlocklistDocument doc = BlocklistService.Parse(Sample);

        doc.Version.Should().Be("2026.0727.0750.43");
        doc.LastModified.Should().Be("27 Jul 2026 07:50 UTC");
    }

    [Fact]
    public void Parse_DeduplicatesAndTrimsDomains()
    {
        BlocklistDocument doc = BlocklistService.Parse(Sample);

        doc.Domains.Should().Equal(
            "ads.example.com", "tracker.example.net", "spaced.example.org");
        doc.DomainSet.Should().Contain("ADS.EXAMPLE.COM");
    }

    [Fact]
    public void Parse_SkipsCommentsAndBlankLines()
    {
        BlocklistDocument doc = BlocklistService.Parse(Sample);

        doc.Domains.Should().NotContain(d => d.StartsWith('#'));
        doc.Domains.Should().NotContain(string.Empty);
    }

    [Fact]
    public void FindMatches_ReturnsOnlyRuleMatches()
    {
        BlocklistDocument doc = BlocklistService.Parse(Sample);
        var rules = new[]
        {
            new RemovalRule { Pattern = "ads", MatchType = MatchType.Contains }
        };

        List<string> matches = BlocklistService.FindMatches(doc.Domains, rules);

        matches.Should().ContainSingle().Which.Should().Be("ads.example.com");
    }

    [Fact]
    public void FindMatches_IgnoresDisabledRules()
    {
        BlocklistDocument doc = BlocklistService.Parse(Sample);
        var rules = new[]
        {
            new RemovalRule
            {
                Pattern = "ads",
                MatchType = MatchType.Contains,
                Enabled = false
            }
        };

        BlocklistService.FindMatches(doc.Domains, rules).Should().BeEmpty();
    }

    [Fact]
    public void ApplyRemovals_WithoutRules_ReturnsAllDomains()
    {
        BlocklistDocument doc = BlocklistService.Parse(Sample);

        BlocklistService.ApplyRemovals(doc, [])
            .Should().Equal(doc.Domains);
    }

    [Fact]
    public void ApplyRemovals_RemovesMatchingDomains()
    {
        BlocklistDocument doc = BlocklistService.Parse(Sample);
        var rules = new[]
        {
            new RemovalRule { Pattern = "example.com", MatchType = MatchType.ExactOrSubdomain }
        };

        BlocklistService.ApplyRemovals(doc, rules)
            .Should().Equal("tracker.example.net", "spaced.example.org");
    }

    [Fact]
    public void ApplyRemovals_IgnoresDisabledRules()
    {
        BlocklistDocument doc = BlocklistService.Parse(Sample);
        var rules = new[]
        {
            new RemovalRule
            {
                Pattern = "example.com",
                MatchType = MatchType.ExactOrSubdomain,
                Enabled = false
            }
        };

        BlocklistService.ApplyRemovals(doc, rules)
            .Should().Equal(doc.Domains);
    }

    [Fact]
    public async Task WriteAsync_WritesOneDomainPerLine_WithoutComments()
    {
        string path = Path.GetTempFileName();
        try
        {
            string[] domains = ["a.com", "b.com", "c.com"];
            await BlocklistService.WriteAsync(path, domains);

            string[] written = await File.ReadAllLinesAsync(path);
            written.Should().Equal(domains);
            written.Should().NotContain(line => line.StartsWith('#'));
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Fact]
    public async Task LoadAsync_ReadsAndParsesFile()
    {
        string path = Path.GetTempFileName();
        try
        {
            await File.WriteAllTextAsync(path, Sample);

            BlocklistDocument doc = await BlocklistService.LoadAsync(path);

            doc.Version.Should().Be("2026.0727.0750.43");
            doc.Domains.Should().HaveCount(3);
        }
        finally
        {
            File.Delete(path);
        }
    }
}
