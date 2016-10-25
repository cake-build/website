@ECHO OFF
REM SET Cake version
SET CAKE_VERSION=0.16.2
SET CAKE_FOLDER=Cake.%CAKE_VERSION%

REM Cleanup any old Cake versions
FOR /f "delims=" %%c IN ('dir /AD /B "Tools\Cake*"') DO (
        IF NOT "%%c" == "%CAKE_FOLDER%" (RD /S /Q "Tools\%%c")
)

REM Validate nuget version
IF EXIST "Tools\nuget.exe" (
    Tools\nuget.exe|findstr /n "3.4.4.13" > nul
    IF NOT %ERRORLEVEL% == 0 (
        ECHO Wrong nuget version, deleting...
        del "Tools\nuget.exe"
    )
)

IF NOT EXIST "Tools" (md "Tools")
IF NOT EXIST "Tools\Addins" (md "Tools\Addins")
IF NOT EXIST "Tools\nuget.exe" ( powershell -NoProfile -ExecutionPolicy Bypass -Command "(New-Object System.Net.WebClient).DownloadFile('https://dist.nuget.org/win-x86-commandline/v3.4.4/NuGet.exe','%~dp0\Tools\nuget.exe')" )
SET PATH=%~dp0\Tools;%PATH%
SET NUGET_EXE=%~dp0\Tools\nuget.exe
Tools\nuget.exe install Cake -Version %CAKE_VERSION% -OutputDirectory "Tools" -Source https://api.nuget.org/v3/index.json
Tools\%CAKE_FOLDER%\Cake.exe -version
Tools\%CAKE_FOLDER%\Cake.exe deploy.cake