@echo off

pushd .
setlocal

echo build_cache.json > .git/info/exclude

if not exist "c:\Python37" (
   echo "Downloading Python37..."
   powershell -command "[Net.ServicePointManager]::SecurityProtocol = [Net.ServicePointManager]::SecurityProtocol -bor [Net.SecurityProtocolType]::Tls12 ; wget https://www.python.org/ftp/python/3.7.1/python-3.7.1.exe -outfile python-3.7.1.exe"
   
   echo "Installing Python37..."
   start "Installing Python3.7.1 ..." /wait python-3.7.1.exe  /quiet InstallAllUsers=1 PrependPath=1 Include_test=0 TargetDir=c:\Python37

   powershell -command "rm python-3.7.1.exe"
)
set python3_dir=C:\Python37

cd build
"%python3_dir%\python.exe" buildProject.py %*
cd ..

popd