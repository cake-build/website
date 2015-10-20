---
content-type: markdown
---

### Syntax Highlighting

To enable syntax highlighting for .cake files in Visual Studio Code follow these steps:

#### Install Yo Code

**NOTE:** This assumes that you already have the Node Package Manager installed and configured on your path.  If this is not the case, follow the instructions [here](https://docs.npmjs.com/getting-started/installing-node)

1. `npm install -g yo`
2. `npm install -g generator-code`

#### Run the Visual Studio Code generator

1. `yo code`
2. Select `New Language Support` and press enter
3. When prompted for a URL for file, enter `https://raw.githubusercontent.com/wintermi/csharp-tmbundle/master/Syntaxes/C%23.tmLanguage` and press enter
4. When prompted for the name of your extension enter `cake` and press enter
5. When prompted for the detected languageId leave the default and simply press enter
6. When prompted for the detected name, enter `C# Cake File` and press enter
7. When prompted for the detected file extension, enter `.cake` and press enter
8. This will then generate two files `package.json` and `syntaxes\cs.tmLanguage` contained within a folder called `cake` in the folder that you executed the command on
9. Copy this `cake` folder into your customizations folder (which depends on your environment)
  * **Windows** `%USERPROFILE%\.vscode\extensions`
  * **Mac** `$HOME/.vscode/extensions`
  * **Linux** `$HOME/.vscode/extensions`
10. Open Visual Studio Code and open a .cake file, and you should now have both syntax highlighting and brace matching



### Intellisense

There is currently no support for Intellisense in .cake files within Visual Studio Code.