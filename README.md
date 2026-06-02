# jaywapp

A C#/.NET solution that bundles the **Jaywapp.Infrastructure** and **Jaywapp.Wpf** reusable libraries, with automated NuGet publishing via GitHub Actions.

## Projects

| Project | Type | Description |
|---------|------|-------------|
| `Jaywapp.Infrastructure` | Class Library | Core infrastructure helpers (submodule: `jaywapp/Jaywapp.Infrastructure`) |
| `Jaywapp.Infrastructure.Tests` | Unit Tests | Tests for the Infrastructure library |
| `Jaywapp.Wpf` | Class Library | WPF UI utilities and helpers (submodule: `jaywapp/Jaywapp.Wpf`) |
| `Jaywapp.Wpf.Tests` | Unit Tests | Tests for the WPF library |

Both libraries are maintained as independent Git submodules and published to [NuGet.org](https://www.nuget.org/).

## Tech Stack

- **Language**: C# (.NET 8)
- **UI Framework**: WPF (Windows Presentation Foundation)
- **Build**: MSBuild / `dotnet` CLI
- **Package management**: NuGet
- **CI/CD**: GitHub Actions (auto-publish on `v*` tags)

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Windows (required for WPF projects)
- Git with submodule support

## Setup

```bash
# Clone with submodules
git clone --recurse-submodules git@github.com:jaywapp/jaywapp.git
cd jaywapp

# If already cloned without submodules
git submodule update --init --recursive
```

## Build

```bash
# Restore NuGet packages
dotnet restore jaywapp.sln

# Build (Debug)
dotnet build jaywapp.sln -c Debug

# Build (Release)
dotnet build jaywapp.sln -c Release
```

## Test

```bash
dotnet test jaywapp.sln
```

## Release & NuGet Publish

Pushing a tag that matches `v*` triggers the `publish` GitHub Actions workflow, which:

1. Restores and packs `Jaywapp.Infrastructure` with the tag version.
2. Pushes the resulting `.nupkg` to NuGet.org.

```bash
git tag v1.0.0
git push origin v1.0.0
```

The `NUGET_API_KEY` secret must be configured in the repository settings.

## Scripts

| Script | Description |
|--------|-------------|
| `scripts/add_csharp_xml_docs.py` | Automatically inserts XML documentation stubs into public C# types, methods, and properties |

## License

MIT — see [LICENSE](LICENSE).
