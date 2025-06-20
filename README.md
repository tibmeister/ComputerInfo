# ComputerInfo

A Windows Forms application for Windows 11 built with .NET 8.0, designed to display a comprehensive snapshot of the host system’s hardware, software, and network details.

## Features

- Displays critical system information:
  - Manufacturer & Model
  - System Name & Serial Number
  - Operating System & Version
  - Active Network Adapter & IP Address
  - Firewall Status
  - CPU & RAM Info
  - Disk Space (Total & Free)
  - Last Boot Time
  - Latest Windows Update
  - List of Installed Applications (Name, Version, Install Date)
- Dark/Light Mode support (auto-detects system setting)
- Export output to .csv or .txt
- Grouped changelogs with links to associated issues (#123)

## Architecture

- Built with .NET 8.0 for Windows
- Targets Windows Forms (net8.0-windows)
- Builds as a single-file, self-contained .exe — no external .NET installation required
- Services:
  - SystemInfoService.cs: Retrieves system hardware/software information.
  - NetworkInfoService.cs: Retrieves network adapter and IP details.
- Export Buttons:
  - Export output to .csv or .txt.
  - Export changelog as .pdf and .html.

## Versioning, Releases, and Changelogs

### Versioning

- Versioning follows SemVer conventions.
- The ComputerInfo.csproj auto-increments the build number on every build.

### Changelog Generation

- Changes are grouped by version tag (v1.0.15), making it easy for end-users to review.
- Each changelog entry includes:
  - Commit hash
  - Commit message
  - Date
  - Links to associated issues (e.g., #123)

### Releases

- A new tag (vX.Y.Z) triggers an automated GitHub Action:
  - Appends changelog entries grouped by version.
  - Pushes changes to the changelog branch.
  - Exported as PDF and HTML.
  - Attached to the tagged release.

## Build & Deployment

### Requirements

- .NET 8.0 SDK

### Build

```bash
dotnet build
```

### Publish as Single File

```bash
dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true
```

## Folder Structure

```
ComputerInfo/
├─ .github/
│  └─ workflows/
├─ src/
│  └─ ComputerInfo/
├─ CHANGELOG.md
├─ CHANGELOG.pdf
├─ CHANGELOG.html
├─ ComputerInfo.sln
├─ ComputerInfo.csproj
├─ MainForm.cs
├─ MainForm.Designer.cs
├─ SystemInfoService.cs
├─ NetworkInfoService.cs
├─ README.md
```

## Contributing

We welcome contributions! Please open an issue or PR for:

- Bugs
- New Features
- Documentation Improvements

## License

Released under the MIT License — See LICENSE for details.

## Acknowledgements

Built with .NET 8.0, Windows Forms, and GitHub Actions.

