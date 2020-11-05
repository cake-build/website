---
title: Extending the bootstrapper to parse cake.config
category: How To's
author: flcdrg
---

Cake makes a number of assumptions about files and directories in your project. If you're adding Cake to a brand new project then that isn't a problem, but what happens if you're adding Cake to a 'legacy' project that is already using some of these locations?

This was a problem I faced with a project that was already using the `tools` directory, and at first glance I thought that [cake.config](https://cakebuild.net/docs/running-builds/configuration/set-configuration-values#configuration-file) would solve this, but it turns out this file is not referenced by the bootstrapper scripts.

I created an extended bootstrapper that adds parsing of the cake.config file and allowed me to tell Cake to use a different directory, so that everyone could play nicely together!

eg. Using a Cake.config file like this to avoid clashing with an existing `tools` directory.

```ini
[Paths]
Tools=./caketools
Addins=./caketools/Addins
Modules=./caketools/Modules
```

The changes cover both PowerShell and Bash bootstrap scripts.

### PowerShell

Cake.config uses the ".ini" format, so I make use of the [Get-IniContent function](https://github.com/lipkau/PsIni/blob/master/PSIni/Functions/Get-IniContent.ps1) from Oliver Lipkau's [PsIni Module](https://github.com/lipkau/PsIni).

With that available, we can then use it to set the `$TOOLS_DIR` variable like so:

```powershell
$CAKE_CONFIG = Join-Path $PSScriptRoot "cake.config"

$TOOLS_DIR = Join-Path $PSScriptRoot "tools"

if (Test-Path $CAKE_CONFIG) {
    $ini = Get-IniContent $CAKE_CONFIG

    if ($ini["Paths"] -and $ini["Paths"]["Tools"]) {
        $TOOLS_DIR = $ini["Paths"]["Tools"]

        # Ensure absolute path
        if (-not [IO.Path]::IsPathRooted($TOOLS_DIR)) {
            $TOOLS_DIR = Join-Path $PSScriptRoot $TOOLS_DIR
        }
    }
}
```

### Bash

For Bash, we leverage Alberto Fanjul's [bash-ini-parser](https://github.com/albfan/bash-ini-parser/blob/master/bash-ini-parser). Again with that in place we can now use it to set `TOOLS_DIR`

```bash
if [ -f "cake.config" ]; then

  cfg_parser cake.config
  cfg_section_Paths

  if [ ! -z ${Tools+x} ]; then

    # Make absolute and normalise
    TOOLS_DIR=$( python -c "import os,sys; print os.path.realpath(sys.argv[1])" $Tools)

    echo "Parsed cake.config. TOOLS_DIR updated to $TOOLS_DIR"
  fi
fi
```

### Use it

These changes haven't been merged to the main bootstrapper scripts as the intention is to keep those as simple as possible for the majority of users, but they're available on the branch linked to [this pull request](https://github.com/cake-build/resources/pull/17) if you'd like to make use of them. Use [this link to compare](https://github.com/cake-build/resources/compare/master...flcdrg:UseConfig-15) to the latest original bootstrapper code.
