@ECHO OFF
REM SET Cake & NuGet version
SET CAKE_VERSION=0.17.0
SET CAKE_FOLDER=Cake.%CAKE_VERSION%
SET NUGET_VERSION=3.5.0.1938
SET NUGET_EXE=%~dp0\Tools\nuget.exe
SET PATH=%~dp0\Tools;%PATH%

REM Cleanup any old Cake versions
FOR /f "delims=" %%c IN ('dir /AD /B "Tools\Cake*"') DO (
        IF NOT "%%c" == "%CAKE_FOLDER%" (RD /S /Q "Tools\%%c")
)

REM Validate nuget version
IF EXIST ".\Tools\nuget.exe" (
    .\Tools\nuget.exe|findstr /n "%NUGET_VERSION%" > nul || echo Wrong nuget.exe version, deleting... && del /F "Tools\nuget.exe"
)

REM Install Dependencies
IF NOT EXIST "Tools" (md "Tools")
IF NOT EXIST "Tools\Addins" (md "Tools\Addins")
IF NOT EXIST "Tools\nuget.exe" ( echo Downloading nuget.exe && powershell -NoProfile -ExecutionPolicy Bypass -Command "(New-Object System.Net.WebClient).DownloadFile('https://dist.nuget.org/win-x86-commandline/v3.5.0/NuGet.exe','%NUGET_EXE%')" )
IF NOT EXIST "Tools\%CAKE_FOLDER%\Cake.exe" (echo Dwonloading Cake %CAKE_VERSION% && %NUGET_EXE% install Cake -Version %CAKE_VERSION% -OutputDirectory "Tools" -Source https://api.nuget.org/v3/index.json)

REM Execute deploy
Tools\%CAKE_FOLDER%\Cake.exe -version
Tools\%CAKE_FOLDER%\Cake.exe deploy.cake