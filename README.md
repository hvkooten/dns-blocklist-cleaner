# DNS-BlockList-Cleaner

[![Build status](https://github.com/hvkooten/dns-blocklist-cleaner/actions/workflows/dotnet-desktop.yml/badge.svg?branch=main)](https://github.com/hvkooten/dns-blocklist-cleaner/actions/workflows/dotnet-desktop.yml)
[![Latest release](https://img.shields.io/github/v/release/hvkooten/dns-blocklist-cleaner?label=stable&sort=semver)](https://github.com/hvkooten/dns-blocklist-cleaner/releases/latest)
[![Latest pre-release](https://img.shields.io/github/v/release/hvkooten/dns-blocklist-cleaner?include_prereleases&label=beta&sort=semver)](https://github.com/hvkooten/dns-blocklist-cleaner/releases)
[![License](https://img.shields.io/github/license/hvkooten/dns-blocklist-cleaner)](LICENSE)

A small Windows desktop tool (WinForms, .NET 10) that downloads the
[HaGeZi DNS blocklist](https://github.com/hagezi/dns-blocklists), lets you review,
clean up and edit the rules, and keeps track of which version you last processed.

> The badges above update automatically: the build badge reflects the latest CI run on
> `main`, and the version badges always show the newest published release and pre-release.

## Features

- Download the latest HaGeZi blocklist directly from its raw source URL.
- Parse and display the blocklist version metadata.
- Review and edit rules with configurable match types.
- Remember the last processed version and compare against previous domains.
- Save your configuration and state locally between sessions.

## Download

| Channel | Description | Link |
|---------|-------------|------|
| **Stable** | Latest tested release (recommended) | [Download latest release](https://github.com/hvkooten/dns-blocklist-cleaner/releases/latest) |
| **Beta** | Latest pre-release / preview build | [Download latest beta](https://github.com/hvkooten/dns-blocklist-cleaner/releases) |

The application is published as a **self-contained, single-file** executable for
`win-x64`, so no separate .NET runtime installation is required.

### Versions

- **Latest stable version:** see the `stable` badge at the top of this page.
- **Latest beta version:** see the `beta` badge at the top of this page.

Both badges link straight to the corresponding downloads and are refreshed
automatically whenever a new release or pre-release is published on GitHub.

## Requirements

- Windows 10 version 10.0.17763.0 or later (x64)
- No .NET runtime needed (self-contained build)

For building from source:

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

## Build from source

```powershell
git clone https://github.com/hvkooten/dns-blocklist-cleaner.git
cd dns-blocklist-cleaner
dotnet build DNS-BlockList-Cleaner.slnx --configuration Release
```

Run the tests:

```powershell
dotnet test DNS-BlockList-Cleaner.slnx
```

Produce a self-contained single-file build locally:

```powershell
dotnet publish DNS-BlockList-Cleaner\DNS-BlockList-Cleaner.csproj `
  --configuration Release --runtime win-x64 --self-contained true `
  -p:PublishSingleFile=true --output publish
```

## Continuous integration

Every push and pull request to `main` triggers the
[.NET Core Desktop workflow](.github/workflows/dotnet-desktop.yml), which restores
dependencies, runs the unit tests and publishes the application. The current status is
shown by the build badge at the top of this README.

## Releasing a new version

Version numbers follow the `MAJOR.MINOR.PATCH` (`vX.Y.Z`) tag format, with the
following channel convention:

| Tag example | Meaning | Channel |
|-------------|---------|---------|
| `v2.0.0` | Major release (`X.0.0`) | **Stable** |
| `v1.1.0` | Minor release (`X.Y.0`) | **Stable** |
| `v1.0.1` | Patch release (anything not ending in `.0`) | **Beta** (pre-release) |

To publish an update, create and push a Git tag:

```powershell
git tag v1.1.0        # stable minor/major release (tag ends in .0)
# or
git tag v1.0.1        # beta / pre-release (patch tag)
git push origin --tags
```

The CI workflow then automatically:

1. Builds and tests the solution.
2. Publishes a self-contained single-file executable.
3. Creates a GitHub Release with that executable attached.
   - Tags ending in `.0` become **stable** releases.
   - All other tags become **beta** (pre-release) builds.

The version badges and download links in this README point at the GitHub releases
API, so they update automatically once the release is published — no manual edits
to this file are required.

## License

See [LICENSE](LICENSE).
