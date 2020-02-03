IF NOT EXIST C:\ProgramData\Miniconda3\ GOTO NOCONDA
call C:\ProgramData\Miniconda3\Scripts\activate
python cmake\build\installProject.py
:NOCONDA
"%~dp0cmake\build\installProject.py" "%*"