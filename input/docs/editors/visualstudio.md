Order: 30
Title: Visual Studio
---

# Syntax Highlighting

Syntax highlighting and Code snippets for .cake files are provided by the **Cake for Visual Studio** extension available from the [Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=vs-publisher-1392591.CakeforVisualStudio).

# Intellisense

There is currently no support for Intellisense in .cake script files within Visual Studio.

# Templates

Choose the "Cake" category from the File > New > Project menu to quickly create a new Cake addin, module or addin test project.

![Project templates](https://raw.githubusercontent.com/cake-build/cake-vs/develop/art/project-template.png)

You can also use the commands from the Build > Cake Build menu to install the default bootstrapper scripts or Cake configuration files into an open solution.

# Using the Task Runner

If your `build.cake` file is included in your solution, you can open the Task Runner Explorer window by right-clicking on the file from your Solution Explorer and choosing *Task Runner Explorer*.

Individual tasks will be listed on the left, and each task can be executed by double-clicking the task.

![Task Runner](https://raw.githubusercontent.com/cake-build/cake-vs/develop/art/console.png)

Task bindings make it possible to associate individual tasks with Visual Studio events such as Project Open, Build or Clean. These bindings are saved in your `cake.config` file.