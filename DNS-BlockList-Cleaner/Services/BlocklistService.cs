using DNS_BlockList_Cleaner.Models;

namespace DNS_BlockList_Cleaner.Services;

/// <summary>
/// Parses HaGeZi "onlydomains" blocklists and generates cleaned output.
/// The header (comments starting with '#') is used only to extract metadata;
/// generated files never contain comments.
/// </summary>
public static class BlocklistService
{
    private const string VersionPrefix = "# Version:";
    private const string ModifiedPrefix = "# Last modified:";

    /// <summary>Parses raw blocklist text into a <see cref="BlocklistDocument"/>.</summary>
    public static BlocklistDocument Parse(string content)
    {
        ArgumentNullException.ThrowIfNull(content);

        string[] lines = content.Split('\n');
        return ParseLines(lines);
    }

    /// <summary>Reads and parses a blocklist file from disk.</summary>
    public static async Task<BlocklistDocument> LoadAsync(
        string path, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path is required.", nameof(path));

        string[] lines = await File.ReadAllLinesAsync(path, cancellationToken)
            .ConfigureAwait(false);
        return ParseLines(lines);
    }

    private static BlocklistDocument ParseLines(IEnumerable<string> lines)
    {
        string? version = null;
        string? modified = null;
        List<string> domains = [];
        HashSet<string> set = new(StringComparer.OrdinalIgnoreCase);

        foreach (string raw in lines)
        {
            string line = raw.Trim();
            if (line.Length == 0)
                continue;

            if (line.StartsWith('#'))
            {
                if (version is null
                    && line.StartsWith(VersionPrefix, StringComparison.OrdinalIgnoreCase))
                {
                    version = line[VersionPrefix.Length..].Trim();
                }
                else if (modified is null
                    && line.StartsWith(ModifiedPrefix, StringComparison.OrdinalIgnoreCase))
                {
                    modified = line[ModifiedPrefix.Length..].Trim();
                }

                continue;
            }

            if (set.Add(line))
                domains.Add(line);
        }

        return new BlocklistDocument
        {
            Version = version,
            LastModified = modified,
            Domains = domains,
            DomainSet = set
        };
    }

    /// <summary>
    /// Returns the domains that match at least one of the given removal rules.
    /// </summary>
    public static List<string> FindMatches(
        IEnumerable<string> domains, IReadOnlyCollection<RemovalRule> rules)
    {
        ArgumentNullException.ThrowIfNull(domains);
        ArgumentNullException.ThrowIfNull(rules);

        List<string> result = [];
        foreach (string domain in domains)
        {
            foreach (RemovalRule rule in rules)
            {
                if (rule.Enabled && DomainMatcher.Matches(domain, rule))
                {
                    result.Add(domain);
                    break;
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Produces the cleaned list of domains with all removal rules applied.
    /// </summary>
    public static List<string> ApplyRemovals(
        BlocklistDocument document, IReadOnlyCollection<RemovalRule> rules)
    {
        ArgumentNullException.ThrowIfNull(document);
        ArgumentNullException.ThrowIfNull(rules);

        if (rules.Count == 0)
            return [.. document.Domains];

        List<string> kept = [];
        foreach (string domain in document.Domains)
        {
            bool remove = false;
            foreach (RemovalRule rule in rules)
            {
                if (rule.Enabled && DomainMatcher.Matches(domain, rule))
                {
                    remove = true;
                    break;
                }
            }

            if (!remove)
                kept.Add(domain);
        }

        return kept;
    }

    /// <summary>
    /// Writes domains to <paramref name="path"/> without any comments,
    /// one domain per line.
    /// </summary>
    public static async Task WriteAsync(
        string path, IEnumerable<string> domains,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path is required.", nameof(path));
        ArgumentNullException.ThrowIfNull(domains);

        await File.WriteAllLinesAsync(path, domains, cancellationToken)
            .ConfigureAwait(false);
    }
}
