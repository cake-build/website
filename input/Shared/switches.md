## Switches

| Switch | Description |
|--------|-------------|
| --target=&lt;target&gt; | Target task to invoke. This is a [custom switch](#custom-switches) which script must support explicitly. |
| --verbosity=&lt;value&gt; | Specifies the amount of information to be displayed (quiet, minimal, normal, verbose, diagnostic). |
| --debug | Launches script in debug mode. |
| --showdescription | Shows description about tasks. |
| --showtree, --tree | Shows the task dependency tree. |
| --dryrun | Performs a dry run. |
| --exclusive | Execute a single task without any dependencies. |
| --version | Displays version information. |
| --info | Displays additional information about Cake execution. |
| --help | Prints help information. |

## Custom switches

All switches not recognized by Cake will be added to an argument list that is passed to the build script.
See [Arguments And Environment Variables](/writing-builds/args-and-environment-vars#arguments) how to read arguments in your script.
