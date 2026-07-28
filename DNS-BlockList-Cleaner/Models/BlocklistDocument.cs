namespace DNS_BlockList_Cleaner.Models;

/// <summary>
/// A parsed HaGeZi "onlydomains" blocklist: header metadata (version, last
/// modified) plus the ordered set of domain entries. Comments are not retained
/// for output because generated files must be comment-free.
/// </summary>
public sealed class BlocklistDocument
{
    /// <summary>Version string parsed from the header ("# Version: ...").</summary>
    public string? Version { get; init; }

    /// <summary>"Last modified" string parsed from the header.</summary>
    public string? LastModified { get; init; }

    /// <summary>All domain entries in original order.</summary>
    public IReadOnlyList<string> Domains { get; init; } = [];

    /// <summary>Fast lookup set of the domains.</summary>
    public IReadOnlySet<string> DomainSet { get; init; } =
        new HashSet<string>(StringComparer.OrdinalIgnoreCase);
}
