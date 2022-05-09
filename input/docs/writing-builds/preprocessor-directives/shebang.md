Order: 50
Title: Shebang directive
Excerpt: Directive to define the interpreter
---

Under Unix-like operating systems, when a script with a shebang is run as a program, the program loader parses the rest of the script's initial line as an interpreter directive; the specified interpreter program is run instead, passing to it as an argument the path that was initially used when attempting to run the script.

These are only used for shells to identify how to run the script and are omitted when Cake compiles the script.

# Usage

```bash
#!path/to/launch/cake
```