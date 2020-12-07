Order: 60
Title: Upgrade instructions
Description: Upgrade instructions between different versions of Cake
---

# Cake 0.38.x to Cake 1.0

Cake 1.0 is a major version containing breaking changes.

## Replace obsolete members

Members marked as obsolete in previous versions have been removed in Cake 1.0.
Update to the member suggested in the obsolete message.

## Cake CLI updates

As part of the rewrite of the  CLI of Cake for Cake 1.0 parsing of switches is now stricter.

### Argument syntax

With Cake 1.0 arguments should always be called with multi-dash syntax (e.g. `--target=Foo`).
When using single-dash syntax (e.g. `-target=Foo`) an error message similar to the following will be shown:

```
Error: Unknown command 'Foo'.
       build.cake -target=Foo
                          ^^^^^^ No such command
```

### Passing empty arguments

With previous versions of Cake it was possible to define an empty argument (e.g. `--foo=`).

With Cake 1.0 an error message similar to the following will be shown:

```
Error: Expected an option value.
```

This syntax is no longer supported with Cake 1.0.
