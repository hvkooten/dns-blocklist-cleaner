namespace DNS_BlockList_Cleaner.Models;

/// <summary>
/// A single, remembered rule describing which domains should be removed
/// from a blocklist. Rules are persisted so they can be re-applied to new
/// versions of the blocklist automatically.
/// </summary>
public sealed record RemovalRule
{
    /// <summary>The search text or pattern entered by the user.</summary>
    public required string Pattern { get; init; }

    /// <summary>How <see cref="Pattern"/> is matched against a domain.</summary>
    public MatchType MatchType { get; init; } = MatchType.Contains;

    /// <summary>
    /// When <see langword="false"/>, the rule is kept but not applied when
    /// finding matches or generating a cleaned blocklist.
    /// </summary>
    public bool Enabled { get; init; } = true;

    /// <summary>When the rule was created or last confirmed.</summary>
    public DateTimeOffset CreatedUtc { get; init; } = DateTimeOffset.UtcNow;
}
