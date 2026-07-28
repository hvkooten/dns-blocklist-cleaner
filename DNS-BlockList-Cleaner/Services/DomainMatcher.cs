using System.Text.RegularExpressions;
using DNS_BlockList_Cleaner.Models;

namespace DNS_BlockList_Cleaner.Services;

/// <summary>
/// Evaluates whether a domain matches a removal rule according to its
/// <see cref="MatchType"/>. Matching is case-insensitive.
/// </summary>
public static class DomainMatcher
{
    /// <summary>
    /// Returns <see langword="true"/> when <paramref name="domain"/> matches
    /// the given <paramref name="rule"/>.
    /// </summary>
    public static bool Matches(string domain, RemovalRule rule)
    {
        ArgumentNullException.ThrowIfNull(rule);

        return Matches(domain, rule.Pattern, rule.MatchType);
    }

    /// <summary>
    /// Returns <see langword="true"/> when <paramref name="domain"/> matches
    /// <paramref name="pattern"/> using the given <paramref name="matchType"/>.
    /// </summary>
    public static bool Matches(string domain, string pattern, Models.MatchType matchType)
    {
        if (string.IsNullOrWhiteSpace(domain) || string.IsNullOrWhiteSpace(pattern))
            return false;

        string d = domain.Trim();
        string p = pattern.Trim();

        return matchType switch
        {
            Models.MatchType.Contains =>
                d.Contains(p, StringComparison.OrdinalIgnoreCase),

            Models.MatchType.Exact =>
                string.Equals(d, p, StringComparison.OrdinalIgnoreCase),

            Models.MatchType.ExactOrSubdomain =>
                string.Equals(d, p, StringComparison.OrdinalIgnoreCase)
                || d.EndsWith("." + p, StringComparison.OrdinalIgnoreCase),

            Models.MatchType.Wildcard =>
                WildcardMatches(d, p),

            _ => false
        };
    }

    private static bool WildcardMatches(string domain, string pattern)
    {
        string regex = "^" + Regex.Escape(pattern)
            .Replace("\\*", ".*", StringComparison.Ordinal)
            .Replace("\\?", ".", StringComparison.Ordinal) + "$";

        return Regex.IsMatch(domain, regex,
            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
    }
}
