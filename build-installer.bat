@echo off
REM DIMP EXE Installer Build Script
REM Requires Inno Setup to be installed
REM Download from: https://jrsoftware.org/isinfo.php

setlocal

REM Check for Inno Setup installation
set "ISCC="
if exist "%ProgramFiles(x86)%\Inno Setup 6\ISCC.exe" (
    set "ISCC=%ProgramFiles(x86)%\Inno Setup 6\ISCC.exe"
) else if exist "%ProgramFiles%\Inno Setup 6\ISCC.exe" (
    set "ISCC=%ProgramFiles%\Inno Setup 6\ISCC.exe"
) else if exist "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" (
    set "ISCC=C:\Program Files (x86)\Inno Setup 6\ISCC.exe"
)

if "%ISCC%"=="" (
    echo ERROR: Inno Setup 6 not found!
    echo Please install Inno Setup 6 from: https://jrsoftware.org/isinfo.php
    echo.
    echo After installation, run this script again.
    pause
    exit /b 1
)

echo Found Inno Setup at: %ISCC%

REM Get version from project file
for /f "tokens=2 delims=<>" %%a in ('findstr /i "Version" MissionPlanner.csproj ^| findstr /i "1.3"') do (
    set "VERSION=%%a"
)
echo Building DIMP v%VERSION% installer...

REM Create output directory
if not exist "bin\installer" mkdir "bin\installer"

REM Update version in ISS file
powershell -Command "(Get-Content MissionPlanner.iss) -replace '#define MyAppVersion \"[^\"]*\"', '#define MyAppVersion \"%VERSION%\"' | Set-Content MissionPlanner.iss"

REM Compile the installer
echo Compiling installer...
"%ISCC%" MissionPlanner.iss

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ================================================
    echo Build successful!
    echo Installer location: bin\installer\DIMP-%VERSION%-Setup.exe
    echo ================================================
) else (
    echo.
    echo Build failed with error code %ERRORLEVEL%
)

pause