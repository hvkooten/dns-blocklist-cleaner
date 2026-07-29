namespace DNS_BlockList_Cleaner.Tests;

public class DomainMatcherTests
{
    [Theory]
    [InlineData("ads.google.com", "google", true)]
    [InlineData("ads.google.com", "GOOGLE", true)]
    [InlineData("ads.example.com", "google", false)]
    public void Matches_Contains(string domain, string pattern, bool expected)
    {
        DomainMatcher.Matches(domain, pattern, MatchType.Contains)
            .Should().Be(expected);
    }

    [Theory]
    [InlineData("google.com", "google.com", true)]
    [InlineData("ads.google.com", "google.com", false)]
    [InlineData("GOOGLE.com", "google.com", true)]
    public void Matches_Exact(string domain, string pattern, bool expected)
    {
        DomainMatcher.Matches(domain, pattern, MatchType.Exact)
            .Should().Be(expected);
    }

    [Theory]
    [InlineData("google.com", "google.com", true)]
    [InlineData("ads.google.com", "google.com", true)]
    [InlineData("notgoogle.com", "google.com", false)]
    [InlineData("google.com.evil.com", "google.com", false)]
    public void Matches_ExactOrSubdomain(string domain, string pattern, bool expected)
    {
        DomainMatcher.Matches(domain, pattern, MatchType.ExactOrSubdomain)
            .Should().Be(expected);
    }

    [Theory]
    [InlineData("ads.google.com", "*.google.com", true)]
    [InlineData("google.com", "*.google.com", false)]
    [InlineData("track3r.com", "track?r.com", true)]
    [InlineData("tracker.com", "track?r.com", true)]
    [InlineData("trackerr.com", "track?r.com", false)]
    public void Matches_Wildcard(string domain, string pattern, bool expected)
    {
        DomainMatcher.Matches(domain, pattern, MatchType.Wildcard)
            .Should().Be(expected);
    }

    [Theory]
    [InlineData("", "google")]
    [InlineData("google.com", "")]
    [InlineData("google.com", "   ")]
    public void Matches_ReturnsFalse_ForEmptyInput(string domain, string pattern)
    {
        DomainMatcher.Matches(domain, pattern, MatchType.Contains)
            .Should().BeFalse();
    }

    [Fact]
    public void Matches_UsesRulePatternAndType()
    {
        var rule = new RemovalRule
        {
            Pattern = "*.ads.com",
            MatchType = MatchType.Wildcard
        };

        DomainMatcher.Matches("banner.ads.com", rule).Should().BeTrue();
        DomainMatcher.Matches("banner.example.com", rule).Should().BeFalse();
    }
}
