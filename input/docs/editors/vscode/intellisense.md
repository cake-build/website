Order: 10
Title: IntelliSense in Visual Studio Code
---

# Enabling IntelliSense

To enable IntelliSense support in Visual Studio Code follow these steps:

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#tool">.NET Core Tool</a></li>
    <li><a data-toggle="tab" href="#frosting">Cake Frosting</a></li>
    <li><a data-toggle="tab" href="#netfx">Cake runner for .NET Framework</a></li>
    <li><a data-toggle="tab" href="#core">Cake runner for .NET Core</a></li>
</ul>

<div class="tab-content">
    <div id="tool" class="tab-pane fade in active">
        <p>
            IntelliSense support for <code>.cake</code> files is provided through the
            <a href="https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp">C# extension for Visual Studio Code</a>,
            along with <a href="https://github.com/cake-build/bakery">Cake.Bakery</a>.
        </p>
        <ol>
            <li>
                Open Visual Studio Code to the folder that you have a Cake file in.
            </li>
            <li>
                <p>
                    Install <a href="https://marketplace.visualstudio.com/items/cake-build.cake-vscode">Cake extension for Visual Studio Code</a>.
                    Make sure at least version 0.10.1 is installed.
                </p>
                <div class="alert alert-info">
                    <p>
                        For instructions how to install an extension in Visual Studio Code see
                        <a href="https://code.visualstudio.com/docs/editor/extension-gallery">Extension Marketplace documentation</a>.
                    </p>
                </div>
            </li>
            <li>
                <p>
                    Install <a href="https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp">C# extension for Visual Studio Code</a>.
                    Make sure at least version 1.13.0 is installed.
                </p>
                <div class="alert alert-info">
                    <p>
                        For instructions how to install an extension in Visual Studio Code see
                        <a href="https://code.visualstudio.com/docs/editor/extension-gallery">Extension Marketplace documentation</a>.
                    </p>
                </div>
            </li>
            <li>
                <p>Install <a href="https://github.com/cake-build/bakery">Cake.Bakery</a> using any of the following options:</p>
                <ul>
                    <li>
                        <p>
                            Using <a href="https://marketplace.visualstudio.com/items/cake-build.cake-vscode">Cake extension for Visual Studio Code</a>:
                        </p>
                        <ul>
                            <li>Open command palette (<code>Ctrl+Shift+P</code>)</li>
                            <li>Type <code>Cake</code></li>
                            <li>Select <code>Install intellisense support</code> command.</li>
                        </ul>
                    </li>
                    <li>
                        <p>
                            On Windows using <a href="https://chocolatey.org/">Chocolatey</a>:
                        </p>
                        <pre><code class="language-cmd">choco install cake-bakery.portable</code></pre>
                    </li>
                    <li>
                        <p>
                            Download <a href="https://github.com/cake-build/bakery/releases">Release ZIP</a>,
                            extract it and add it to <code>PATH</code> environment variable.
                        </p>
                        <div class="alert alert-info">
                            <p>
                                If you want to install Cake.Bakery outside of the <code>PATH</code> environment variable you can specify
                                the path to <code>Cake.Bakery.exe</code> using the
                                <a href="https://github.com/OmniSharp/omnisharp-roslyn/wiki/Configuration-Options" target="_blank">OmniSharp Configuration System</a>.
                            </p>
                            <p>
                                Create a file called <code>omnisharp.json</code> in <code>%USERPROFILE%/.omnisharp/</code>
                                (<code>~/.omnisharp/</code> if you are on Linux/macOS) with the following content
                                (replace <code>&lt;custom_path&gt;</code> with the path to Cake.Bakery.):
                            </p>
<pre><code class="language-json">
{
  "cake": {
    "bakeryPath": "&lt;custom_path&gt;/Cake.Bakery/tools/Cake.Bakery.exe"
  }
}
</code></pre>
                        </div>
                    </li>
                </ul>
            </li>
        </ol>
    </div>
    <div id="frosting" class="tab-pane fade">
        <p>
            IntelliSense support for <a href="../getting-started/running-cake-scripts#cake-frosting">Cake Frosting</a> projects is provided through the
            <a href="https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp">C# extension for Visual Studio Code</a>.
        </p>
        <ol>
            <li>
                <p>
                    Install <a href="https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp">C# extension for Visual Studio Code</a>.
                </p>
                <div class="alert alert-info">
                    <p>
                        For instructions how to install an extension in Visual Studio Code see
                        <a href="https://code.visualstudio.com/docs/editor/extension-gallery">Extension Marketplace documentation</a>.
                    </p>
                </div>
            </li>
        </ol>
    </div>
    <div id="netfx" class="tab-pane fade">
        <p>
            IntelliSense support for <code>.cake</code> files is provided through the
            <a href="https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp">C# extension for Visual Studio Code</a>,
            along with <a href="https://github.com/cake-build/bakery">Cake.Bakery</a>.
        </p>
        <ol>
            <li>
                Open Visual Studio Code to the folder that you have a Cake file in.
            </li>
            <li>
                <p>
                    Install <a href="https://marketplace.visualstudio.com/items/cake-build.cake-vscode">Cake extension for Visual Studio Code</a>.
                    Make sure at least version 0.10.1 is installed.
                </p>
                <div class="alert alert-info">
                    <p>
                        For instructions how to install an extension in Visual Studio Code see
                        <a href="https://code.visualstudio.com/docs/editor/extension-gallery">Extension Marketplace documentation</a>.
                    </p>
                </div>
            </li>
            <li>
                <p>
                    Install <a href="https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp">C# extension for Visual Studio Code</a>.
                    Make sure at least version 1.13.0 is installed.
                </p>
                <div class="alert alert-info">
                    <p>
                        For instructions how to install an extension in Visual Studio Code see
                        <a href="https://code.visualstudio.com/docs/editor/extension-gallery">Extension Marketplace documentation</a>.
                    </p>
                </div>
            </li>
            <li>
                <p>
                    Make sure that <a href="https://www.nuget.org/packages/Cake/" target="_blank">Cake</a> is installed in your <code>tools</code> folder.
                    We recommend v0.22.0 or later, otherwise your addins will be installed twice.
                    The easiest way to do this is to run your bootstrapping file (e.g. <code>.\build.ps1</code>).
                </p>
                <div class="alert alert-warning">
                    <p>
                        For Cake.Bakery <code>Cake.Core.dll</code> and <code>Cake.exe</code> need to be available in the same folder.
                        Installation of Cake through <a href="https://chocolatey.org/packages/cake.portable" target="_blank">Cake.Portable Chocolatey pacakge</a>
                        therefore won't support IntelliSense.
                    </p>
                </div>
            </li>
            <li>
                <p>Install <a href="https://github.com/cake-build/bakery">Cake.Bakery</a> using any of the following options:</p>
                <ul>
                    <li>
                        <p>
                            Using <a href="https://marketplace.visualstudio.com/items/cake-build.cake-vscode">Cake extension for Visual Studio Code</a>:
                        </p>
                        <ul>
                            <li>Open command palette (<code>Ctrl+Shift+P</code>)</li>
                            <li>Type <code>Cake</code></li>
                            <li>Select <code>Install intellisense support</code> command.</li>
                        </ul>
                    </li>
                    <li>
                        <p>
                            On Windows using <a href="https://chocolatey.org/">Chocolatey</a>:
                        </p>
                        <pre><code class="language-cmd">choco install cake-bakery.portable</code></pre>
                    </li>
                    <li>
                        <p>
                            Download <a href="https://github.com/cake-build/bakery/releases">Release ZIP</a>,
                            extract it and add it to <code>PATH</code> environment variable.
                        </p>
                        <div class="alert alert-info">
                            <p>
                                If you want to install Cake.Bakery outside of the <code>PATH</code> environment variable you can specify
                                the path to <code>Cake.Bakery.exe</code> using the
                                <a href="https://github.com/OmniSharp/omnisharp-roslyn/wiki/Configuration-Options" target="_blank">OmniSharp Configuration System</a>.
                            </p>
                            <p>
                                Create a file called <code>omnisharp.json</code> in <code>%USERPROFILE%/.omnisharp/</code>
                                (<code>~/.omnisharp/</code> if you are on Linux/macOS) with the following content
                                (replace <code>&lt;custom_path&gt;</code> with the path to Cake.Bakery.):
                            </p>
<pre><code class="language-json">
{
  "cake": {
    "bakeryPath": "&lt;custom_path&gt;/Cake.Bakery/tools/Cake.Bakery.exe"
  }
}
</code></pre>
                        </div>
                    </li>
                </ul>
            </li>
            <li>
                Restart Visual Studio Code.
            </li>
        </ol>
    </div>
    <div id="core" class="tab-pane fade">
        <p>
            IntelliSense in Visual Studio Code is not supported for the Cake runner for .NET Core.
        </p>
    </div>
</div>

[Cake extension for Visual Studio Code]: https://marketplace.visualstudio.com/items/cake-build.cake-vscode
[C# extension for Visual Studio Code]: https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp
[Extension Marketplace documentation]: https://code.visualstudio.com/docs/editor/extension-gallery
[Cake.Bakery]: https://github.com/cake-build/bakery
[Chocolatey]: https://chocolatey.org/
