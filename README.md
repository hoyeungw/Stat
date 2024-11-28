![Banner](https://raw.githubusercontent.com/sharpyr/Stat/refs/heads/master/media/stat-banner.svg)

Cross-table analytics

[![Version](https://img.shields.io/nuget/vpre/Stat.svg)](https://www.nuget.org/packages/Stat)
[![Downloads](https://img.shields.io/nuget/dt/Stat.svg)](https://www.nuget.org/packages/Stat)
[![Dependent Libraries](https://img.shields.io/librariesio/dependents/nuget/Stat.svg?label=dependent%20libraries)](https://libraries.io/nuget/Stat)
[![Language](https://img.shields.io/badge/language-C%23-blueviolet.svg)](https://dotnet.microsoft.com/learn/csharp)
[![Compatibility](https://img.shields.io/badge/compatibility-.NET%20Standard%202.0-blue.svg)]()
[![License](https://img.shields.io/github/license/sharpyr/Stat.svg)](https://github.com/sharpyr/Stat/LICENSE)

## Install

Stat targets .NET Standard 2.0, fits both .NET and .NET Framework.

Install [Stat package](https://www.nuget.org/packages/Stat) and sub packages.

NuGet Package Manager:

```powershell
Install-Package Stat
```

.NET CLI:

```shell
dotnet add package Stat
```

All versions can be found [on nuget](https://www.nuget.org/packages/Stat#versions-body-tab).

## Usage

### Create a Crostab

```csharp
using Stat.Percentile
using Spare
using static Palett.Presets;

var table = Crostab<int>.Build(
    new[] {"high", "mid", "low"}, // side
    new[] {"tier 1", "tier 2", "tier 3"}, // head
    new [,] {
        { 960, 660, 240 },
        { 840, 570, 180 },
        { 720, 480, 120 },
    });
table.Deco().Logger();
```

>
# Examples
---------------------
Stat has a test suite in the [test project](https://github.com/sharpyr/Stat/tree/master/Stat.Test/Src).

## Feedback

Stat is licensed under the [MIT](https://github.com/sharpyr/Stat/LICENSE) license.

Bug report and contribution are welcome at [the GitHub repository](https://github.com/sharpyr/Stat).


