---
title: Dependabot for Cake - Preview
category: News
author: nils-a
---

While [Dependabot][] currently has [no support for Cake][issue-733], work towards that [has already been started][pr-1848].

Using a GitHub Action it is now possible to utilize that work-in-progress to check Cake scripts for outdated references.

<!--excerpt-->

By adding a simple workflow like the following to your `.github/workflows` folder

```yml
name: check and update cake references
on:
  schedule:
    # run everyday at 06:00
    - cron:  '0 6 * * *'

jobs:
  dependabot-cake:
    runs-on: ubuntu-latest # linux, because this is a docker-action
    steps:
      - name: check/update cake dependencies
        uses: nils-org/dependabot-cake-action@v1
```

the Cake scripts (all files named `*.cake`) of the project will be checked daily for outdated references (Meaning `#tool`, `#addin` and `#module` references - but only if they are referencing nuget packages).

The action (or rather the Dependabot code running inside the action) will create pull requests for all outdated references. 

![A created PR for cake.recipe](/assets/img/dependabot-cake-action/pr.png){.img-responsive}

While the created PRs will look somewhat like "real" Dependabot pull requests, they will have some differences:

* They will not be from the Dependabot user, but rather from the workflow running the action.
* They will not have the Dependabot interaction capabilities (e.g. `@dependabot rebase`).
* They will not auto-rebase on pushes to the destination of the pull request.

There are some configuration options: Check the [`README`][readme] for details.

**Technical Background**

* The workflow, as shown above uses [nils-org/dependabot-cake-action][dependabot-cake-action] - a GitHub Action.
* The GitHub Action uses a specially prepared [Docker image][docker-image] to create a container and wraps the configuration options of that container.
* The Docker container contains (among others) the code from [pharos/dependabot-core][pharos-repo] which is the basis for the pull request to enable Cake scripts in Dependabot. 

  Using this code, the container will invoke a simple script that mimics all steps Dependabot normally invokes but hard-coded for Cake scripts.

[Dependabot]: https://docs.github.com/en/free-pro-team@latest/github/administering-a-repository/keeping-your-dependencies-updated-automatically
[issue-733]: https://github.com/dependabot/dependabot-core/issues/733
[pr-1848]: https://github.com/dependabot/dependabot-core/pull/1848
[pharos-repo]: https://github.com/pharos/dependabot-core/tree/add-support-for-cake
[dependabot-cake-action]: https://github.com/nils-org/dependabot-cake-action
[docker-image]: https://hub.docker.com/r/nilsa/dependabot-cake
[readme]: https://github.com/nils-org/dependabot-cake-action/blob/develop/README.md