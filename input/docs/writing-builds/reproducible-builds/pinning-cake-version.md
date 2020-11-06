Order: 10
Description: How to make Cake builds reproducible
---

To have deterministic builds it is important that on every build the same version of Cake is used.

<ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#tool1">.NET Tool</a></li>
    <li><a data-toggle="tab" href="#frosting1">Cake Frosting</a></li>
    <li><a data-toggle="tab" href="#netfx1">Cake runner for .NET Framework</a></li>
    <li><a data-toggle="tab" href="#core1">Cake runner for .NET Core</a></li>
</ul>

<div class="tab-content">
    <div id="tool1" class="tab-pane fade in active">
        <p>
          When <a href="/running-builds/runners/dotnet-core-tool#bootstrapping-for.net-core-tool">installed as a local tool</a>, Cake will always be restored in the version mentioned in the manifest file.
        </p>
    </div>
    <div id="frosting1" class="tab-pane fade">
        <p>
            Make sure to use a fixed version number for <code>Cake.Frosting</code> in the <code>*.csproj</code> file:<br/>
<pre><code class="language-xml hljs">&lt;PackageReference Include="Cake.Frosting" Version="0.38.4" /&gt;</code></pre>
        </p>
        <p>
          To update the version of Cake you are using after you have pinned it, all you need to do is update the <code>Cake.Frosting</code> NuGet package to the newer version you would like to use.
        </p>
    </div>
    <div id="netfx1" class="tab-pane fade">
        <ol>
            <li>
                <p>
                    Pin version of Cake in the <code>tools/packages.config</code> file:<br/>
<pre><code class="language-xml hljs">&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;packages&gt;
    &lt;package id="Cake" version="0.38.5" /&gt;
&lt;/packages&gt;</code></pre>
                </p>
            </li>
            <li>
                <p>
                    Commit <code>tools/packages.config</code> file to source control repository.
                </p>
                <p>
                    This will require some tweaks to your <code>.gitignore</code> file.
                    Below are the tweaks that will be required.
                    The first line says to ignore all files underneath the <code>tool</code> directory.
                    The second says to not ignore the <code>packages.config</code> file.<br/>
<pre><code class="hljs">tools/*
!tools/packages.config</code></pre>
                </p>
            </li>
        </ol>
        <p>
          To update the version of Cake you are using after you have pinned it, all you need to do is update the version in the <code>packages.config</code> file to the newer version you would like to use.
        </p>
    </div>
    <div id="core1" class="tab-pane fade">
        <ol>
            <li>
                <p>
                    Pin version of Cake in the <code>tools/packages.config</code> file:<br/>
<pre><code class="language-xml hljs">&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;packages&gt;
    &lt;package id="Cake.CoreCLR" version="0.38.5" /&gt;
&lt;/packages&gt;</code></pre>
                </p>
            </li>
            <li>
                <p>
                    Commit <code>tools/packages.config</code> file to source control repository.
                </p>
                <p>
                    This will require some tweaks to your <code>.gitignore</code> file.
                    Below are the tweaks that will be required.
                    The first line says to ignore all files underneath the <code>tool</code> directory.
                    The second says to not ignore the <code>packages.config</code> file.<br/>
<pre><code class="hljs">tools/*
!tools/packages.config</code></pre>
                </p>
            </li>
        </ol>
        <p>
          To update the version of Cake you are using after you have pinned it, all you need to do is update the version in the <code>packages.config</code> file to the newer version you would like to use.
        </p>
    </div>
</div>
