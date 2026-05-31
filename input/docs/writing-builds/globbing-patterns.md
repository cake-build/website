---
Order: 50
Title: Globbing Patterns
Description: Documentation for supported globbing syntax in Cake.
---

# Globbing Patterns

Cake supports globbing patterns for matching files and directories.

## Wildcards

### *

Matches zero or more characters.

```csharp
GetFiles("./src/*.cs");
```

### **

Matches directories recursively.

```csharp
GetFiles("./src/**/*.cs");
```

### ?

Matches exactly one character.

```csharp
GetFiles("./Test?.cs");
```

## Character Matching

```csharp
GetFiles("./folder/fooba[rz].txt");
```

Matches:

- foobar.txt
- foobaz.txt

Directories:

```csharp
GetDirectories("./folder/fooba[rz]");
```

## Brace Expansion

Files:

```csharp
GetFiles("./**/*.{cs,json,txt}");
```

Directories:

```csharp
GetDirectories("./**/^{obj,bin,lib}");
```

Matches:

- obj
- bin
- lib

## Examples

```csharp
var files = GetFiles("./**/*.cs");
```

```csharp
var directories = GetDirectories("./src/**/bin");
```