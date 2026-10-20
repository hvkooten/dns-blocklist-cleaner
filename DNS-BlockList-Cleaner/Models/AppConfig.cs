namespace DNS_BlockList_Cleaner.Models;

/// <summary>
/// User configuration, persisted as JSON. Defines where the source blocklist
/// comes from and the fixed name the generated (cleaned) file must always get.
/// </summary>
public sealed record AppConfig
{
    /// <summary>
    /// Raw URL of the HaGeZi blocklist on the main branch, e.g.
    /// https://raw.githubusercontent.com/hagezi/dns-blocklists/main/wildcard/pro-onlydomains.txt
    /// </summary>
    public string SourceUrl { get; init; } =
        "https://raw.githubusercontent.com/hagezi/dns-blocklists/main/wildcard/pro-onlydomains.txt";

    /// <summary>Fixed file name for the generated cleaned blocklist (no comments).</summary>
    public string OutputFileName { get; init; } = "cleaned-blocklist.txt";

    /// <summary>Remembered removal rules that are re-applied to every new version.</summary>
    public List<RemovalRule> RemovalRules { get; init; } = [];

    /// <summary>
    /// Domains that must never be removed, even when a rule matches them.
    /// Populated from the preview to fine-tune individual rule results.
    /// </summary>
    public List<string> ExcludedDomains { get; init; } = [];

    /// <summary>Whether to automatically check for application updates on startup.</summary>
    public bool CheckForUpdatesOnStartup { get; init; } = true;
}
