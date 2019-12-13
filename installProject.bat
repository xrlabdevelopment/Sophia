@ECHO off

REM --------------------------------------------
REM Declare common directories

REM Source and Build directory of Sophia library
SET DAE_SOURCE			=%~dp0
SET DAE_BUILD			=%~dp0/../VC2019_x64/

REM Relative path to the sophia projects
SET SOPHIA_LIB			=%DAE_BUILD%src/libraries/sophia

REM Program Files and Program Files (x86) directory
SET PROGRAM_FILES		=%ProgramFiles%
SET PROGRAM_FILES_X86	=%ProgramFiles% (x86)

REM Location of MSBUILD
SET MSBUILD				=%PROGRAM_FILES_X86%\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\MSBuild.exe

REM --------------------------------------------
REM Print out the source and build directory
ECHO.
ECHO source directory: %DAE_SOURCE%
ECHO build directory:  %DAE_BUILD%

REM Print out the location of MSBUILD
ECHO "%MSBUILD%"

REM --------------------------------------------
REM Build the Sophia library in Debug and Release
"%MSBUILD%" %SOPHIA_LIB%/core/sophia_core.csproj /p:Configuration=Debug /p:Platform="x64" 
"%MSBUILD%" %SOPHIA_LIB%/core/sophia_core.csproj /p:Configuration=Release /p:Platform="x64" 
"%MSBUILD%" %SOPHIA_LIB%/core/sophia_editor.csproj /p:Configuration=Debug /p:Platform="x64" 
"%MSBUILD%" %SOPHIA_LIB%/core/sophia_editor.csproj /p:Configuration=Release /p:Platform="x64" 
"%MSBUILD%" %SOPHIA_LIB%/platform/sophia_platform.csproj /p:Configuration=Debug /p:Platform="x64" 
"%MSBUILD%" %SOPHIA_LIB%/platform/sophia_platform.csproj /p:Configuration=Release /p:Platform="x64"

REM --------------------------------------------
REM Install the Sophia library
"%MSBUILD%" %SOPHIA_LIB%/INSTALL.vcxproj /p:Configuration=Debug /p:Platform="x64" /t:Clean,Build
"%MSBUILD%" %SOPHIA_LIB%/INSTALL.vcxproj /p:Configuration=Release /p:Platform="x64" /t:Clean,Build
