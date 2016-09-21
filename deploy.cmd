@echo off
IF NOT EXIST "Tools" (md "Tools")
IF NOT EXIST "Tools\Addins" (md "Tools\Addins")
nuget install Cake -ExcludeVersion -OutputDirectory "Tools" -Source https://api.nuget.org/v3/index.json
Tools\Cake\Cake.exe -version
Tools\Cake\Cake.exe deploy.cake