IF NOT EXIST C:\ProgramData\Miniconda3\ GOTO NOCONDA
call C:\ProgramData\Miniconda3\Scripts\activate
python generateProject.py "VC2019_x64"  "Visual Studio 16" -A x64
:NOCONDA
python "%~dp0generateProject.py" "VC2019_x64"  "Visual Studio 16" -A x64 "%*"

