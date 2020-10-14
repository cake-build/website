Order: 30
Title: Visual Studio
Description: Extensions and supported features for Visual Studio
RedirectFrom: docs/editors/visualstudio
---

# Syntax Highlighting

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#tool1">.NET Core Tool</a></li>
    <li><a data-toggle="tab" href="#frosting1">Cake Frosting</a></li>
    <li><a data-toggle="tab" href="#netfx1">Cake runner for .NET Framework</a></li>
    <li><a data-toggle="tab" href="#core1">Cake runner for .NET Core</a></li>
</ul>

<div class="tab-content">
    <div id="tool1" class="tab-pane fade in active">
        <p>
            Syntax highlighting for <code>.cake</code> files is provided by the <a href="https://marketplace.visualstudio.com/items?itemName=vs-publisher-1392591.CakeforVisualStudio">Cake for Visual Studio extension</a>.
        </p>
    </div>
    <div id="frosting1" class="tab-pane fade">
        <p>
            <a href="/docs/running-builds/runners/cake-frosting">Cake Frosting</a> projects have all features of a .NET Console application, including syntax highlighting.
        </p>
    </div>
    <div id="netfx1" class="tab-pane fade">
        <p>
            Syntax highlighting for <code>.cake</code> files is provided by the <a href="https://marketplace.visualstudio.com/items?itemName=vs-publisher-1392591.CakeforVisualStudio">Cake for Visual Studio extension</a>.
        </p>
    </div>
    <div id="core1" class="tab-pane fade">
        <p>
            Syntax highlighting for <code>.cake</code> files is provided by the <a href="https://marketplace.visualstudio.com/items?itemName=vs-publisher-1392591.CakeforVisualStudio">Cake for Visual Studio extension</a>.
        </p>
    </div>
</div>

# IntelliSense

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#tool2">.NET Core Tool</a></li>
    <li><a data-toggle="tab" href="#frosting2">Cake Frosting</a></li>
    <li><a data-toggle="tab" href="#netfx2">Cake runner for .NET Framework</a></li>
    <li><a data-toggle="tab" href="#core2">Cake runner for .NET Core</a></li>
</ul>

<div class="tab-content">
    <div id="tool2" class="tab-pane fade in active">
        <p>
            There is currently no support for Intellisense in <code>.cake</code> script files within Visual Studio.
        </p>
    </div>
    <div id="frosting2" class="tab-pane fade">
        <p>
            <a href="/docs/running-builds/runners/cake-frosting">Cake Frosting</a> projects have all features of a .NET Console application, including IntelliSense.
        </p>
    </div>
    <div id="netfx2" class="tab-pane fade">
        <p>
            There is currently no support for Intellisense in <code>.cake</code> script files within Visual Studio.
        </p>
    </div>
    <div id="core2" class="tab-pane fade">
        <p>
            There is currently no support for Intellisense in <code>.cake</code> script files within Visual Studio.
        </p>
    </div>
</div>

# Templates

Choose the "Cake" category from the File > New > Project menu to quickly create a new Cake [addin](/docs/extending/addins/),
[module](/docs/extending/modules) or addin test project.

![Project templates](https://raw.githubusercontent.com/cake-build/cake-vs/develop/art/project-template.png)

You can also use the commands from the Build > Cake Build menu to install the default bootstrapper scripts for
[Cake runner for .NET Framework](/docs/running-builds/runners/cake-runner-for-dotnet-framework) or Cake configuration files into an open solution.

# Code Snippets

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#tool3">.NET Core Tool</a></li>
    <li><a data-toggle="tab" href="#frosting3">Cake Frosting</a></li>
    <li><a data-toggle="tab" href="#netfx3">Cake runner for .NET Framework</a></li>
    <li><a data-toggle="tab" href="#core3">Cake runner for .NET Core</a></li>
</ul>

<div class="tab-content">
    <div id="tool3" class="tab-pane fade in active">
        <p>
            Code snippets for <code>.cake</code> files are provided by the <a href="https://marketplace.visualstudio.com/items?itemName=vs-publisher-1392591.CakeforVisualStudio">Cake for Visual Studio extension</a>.
        </p>
    </div>
    <div id="frosting3" class="tab-pane fade">
        <p>
            Code snippets are currently not supported for <a href="/docs/running-builds/runners/cake-frosting">Cake Frosting</a> projects.
        </p>
    </div>
    <div id="netfx3" class="tab-pane fade">
        <p>
            Code snippets for <code>.cake</code> files are provided by the <a href="https://marketplace.visualstudio.com/items?itemName=vs-publisher-1392591.CakeforVisualStudio">Cake for Visual Studio extension</a>.
        </p>
    </div>
    <div id="core3" class="tab-pane fade">
        <p>
            Code snippets for <code>.cake</code> files are provided by the <a href="https://marketplace.visualstudio.com/items?itemName=vs-publisher-1392591.CakeforVisualStudio">Cake for Visual Studio extension</a>.
        </p>
    </div>
</div>

# Task Runner

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#tool4">.NET Core Tool</a></li>
    <li><a data-toggle="tab" href="#frosting4">Cake Frosting</a></li>
    <li><a data-toggle="tab" href="#netfx4">Cake runner for .NET Framework</a></li>
    <li><a data-toggle="tab" href="#core4">Cake runner for .NET Core</a></li>
</ul>

<div class="tab-content">
    <div id="tool4" class="tab-pane fade in active">
            Task runner is currently not supported for <a href="/docs/running-builds/runners/dotnet-core-tool">.NET Core Tool</a>.
    </div>
    <div id="frosting4" class="tab-pane fade">
        <p>
            Task runner is currently not supported for <a href="/docs/running-builds/runners/cake-frosting">Cake Frosting</a> projects.
        </p>
    </div>
    <div id="netfx4" class="tab-pane fade">
        <p>
            If your <code>build.cake</code> file is included in your solution, you can open the Task Runner Explorer window by right-clicking on the file from your
            Solution Explorer and choosing <em>Task Runner Explorer</em>.
        </p>
        <p>
            Individual tasks will be listed on the left, and each task can be executed by double-clicking the task.
        </p>
        <p>
            <img src="https://raw.githubusercontent.com/cake-build/cake-vs/develop/art/console.png" class="img-fluid" alt="Task Runner">
        </p>
        <p>
            Task bindings make it possible to associate individual tasks with Visual Studio events such as Project Open, Build or Clean.
            These bindings are saved in your <code>cake.config</code> file.
        </p>
    </div>
    <div id="core4" class="tab-pane fade">
            Task runner is currently not supported for <a href="/docs/running-builds/runners/cake-runner-for-dotnet-core">Cake runner for .NET Core</a>.
    </div>
</div>
