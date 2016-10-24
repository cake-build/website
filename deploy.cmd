@echo off
IF EXIST "Tools\nuget.exe" (del "Tools\nuget.exe")
IF NOT EXIST "Tools" (md "Tools")
IF NOT EXIST "Tools\Addins" (md "Tools\Addins")
IF NOT EXIST "Tools\nuget.exe" ( powershell -NoProfile -ExecutionPolicy Bypass -Command "(New-Object System.Net.WebClient).DownloadFile('https://dist.nuget.org/win-x86-commandline/v3.2.0/nuget.exe','%~dp0\Tools\nuget.exe')" )
SET PATH=%~dp0\Tools;%PATH%
Tools\nuget install Cake -ExcludeVersion -OutputDirectory "Tools" -Source https://api.nuget.org/v3/index.json
Tools\Cake\Cake.exe -version
Tools\Cake\Cake.exe deploy.cake