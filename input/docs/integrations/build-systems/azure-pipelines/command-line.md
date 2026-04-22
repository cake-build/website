---
Order: 20
Title: GitHub Actions Command Line
Description: Running Cake in GitHub Actions using the run command.
---

# Run via Command Line

To run Cake in GitHub Actions using the standard command line, add a `run` step to your workflow.

```yaml
jobs:
  build:
    runs-on: ubuntu-latest
    steps:
    - name: Run Cake
      run: dotnet cake