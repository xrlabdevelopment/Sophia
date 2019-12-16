@ECHO off

REM --------------------------------------------
REM Clear the screen before we start
cls

REM --------------------------------------------
REM Declare common directories
REM Source and Build directory of Sophia library
SET DAE_SOURCE=%~dp0
SET DAE_BUILD=%~dp0\..\VC2019_x64\

REM Relative path to the sophia projects
SET SOPHIA_LIB=%DAE_BUILD%src\libraries\sophia

REM --------------------------------------------
REM Find directory of MSBuild
REM Log some information about what we are doing
ECHO Looking for MSBuild.exe.
ECHO Please wait ...

REM Run search script
"%DAE_SOURCE%\cmake\build\findVisualStudioVersion.py" > output

ECHO Done!

REM Save output file content inside MSBUILD parameter
REM Delete output file afterwards
SET /p MSBUILD=<output											
DEL output

REM If MSBuild was not found we exit the install script
IF "%MSBUILD%" == "" GOTO NOT_FOUND														

REM --------------------------------------------
REM Print out found directories
REM Print out the source and build directory
ECHO.
ECHO source directory: %DAE_SOURCE%
ECHO build directory:  %DAE_BUILD%

REM Print out the location of MSBUILD
ECHO msbuild directory: "%MSBUILD%"
ECHO.

REM --------------------------------------------
REM Generate the project
del /f /s /q %DAE_BUILD%CMakeFiles
del /f /s /q %DAE_BUILD%CMakeCache.txt
"%DAE_SOURCE%\cmake\build\generateProject.py" "VC2019_x64"  "Visual Studio 16" -A x64 "%*"

REM --------------------------------------------
REM Build sophia projects
REM Build the Sophia library in Debug and Release
"%MSBUILD%\MSBuild.exe" %SOPHIA_LIB%\core\sophia_core.csproj /p:Configuration=Debug /p:Platform="x64" 
"%MSBUILD%\MSBuild.exe" %SOPHIA_LIB%\core\sophia_core.csproj /p:Configuration=Release /p:Platform="x64" 
"%MSBUILD%\MSBuild.exe" %SOPHIA_LIB%\editor\sophia_editor.csproj /p:Configuration=Debug /p:Platform="x64" 
"%MSBUILD%\MSBuild.exe" %SOPHIA_LIB%\editor\sophia_editor.csproj /p:Configuration=Release /p:Platform="x64" 
"%MSBUILD%\MSBuild.exe" %SOPHIA_LIB%\platform\sophia_platform.csproj /p:Configuration=Debug /p:Platform="x64" 
"%MSBUILD%\MSBuild.exe" %SOPHIA_LIB%\platform\sophia_platform.csproj /p:Configuration=Release /p:Platform="x64"

REM --------------------------------------------
REM Install the Sophia library
REM Install Debug and Release versions
"%MSBUILD%\MSBuild.exe" %SOPHIA_LIB%\INSTALL.vcxproj /p:Configuration=Debug /p:Platform="x64" /t:Clean,Build
"%MSBUILD%\MSBuild.exe" %SOPHIA_LIB%\INSTALL.vcxproj /p:Configuration=Release /p:Platform="x64" /t:Clean,Build

:NOT_FOUND
ECHO Location of MSBUILD was not found.
:END
