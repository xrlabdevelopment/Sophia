@ECHO OFF
del /f /s /q ..\VC2019_x64\CMakeFiles
del /f /s /q ..\VC2019_x64\CMakeCache.txt
IF NOT EXIST C:\ProgramData\Miniconda3\ GOTO NOCONDA
call C:\ProgramData\Miniconda3\Scripts\activate
python generateProject.py "VC2019_x64"  "Visual Studio 16" -A x64 -DSOPHIA_FETCH_SUBMODULE=1
:NOCONDA
"%~dp0generateProject.py" "VC2019_x64"  "Visual Studio 16" -A x64 -DSOPHIA_FETCH_SUBMODULE=1 "%*"