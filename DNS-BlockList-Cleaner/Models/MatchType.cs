namespace DNS_BlockList_Cleaner.Models;

/// <summary>
/// Determines how a removal rule's pattern is matched against domains.
/// </summary>
public enum MatchType
{
    /// <summary>Domain contains the pattern text anywhere.</summary>
    Contains = 0,

    /// <summary>Wildcard pattern, e.g. "*.google.com".</summary>
    Wildcard = 1,

    /// <summary>Exact domain match only.</summary>
    Exact = 2,

    /// <summary>Exact domain and any of its subdomains.</summary>
    ExactOrSubdomain = 3
}
