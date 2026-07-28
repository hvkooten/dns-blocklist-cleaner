namespace DNS_BlockList_Cleaner.Models;

/// <summary>
/// Persisted memory between sessions. Holds a snapshot of the domain set of the
/// previously processed blocklist so new versions can be diffed against it.
/// </summary>
public sealed record AppState
{
    /// <summary>Version string of the last processed blocklist (from its header).</summary>
    public string? LastProcessedVersion { get; init; }

    /// <summary>"Last modified" value of the last processed blocklist (from its header).</summary>
    public string? LastProcessedModified { get; init; }

    /// <summary>All domains present in the previously processed source blocklist.</summary>
    public List<string> PreviousDomains { get; init; } = [];
}
