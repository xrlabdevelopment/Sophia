@ECHO OFF
ECHO Press 1) Generate VS2019 Win64 with cmake cache cleared
ECHO Press 2) Generate VS2019 Win64 using cmake cache

SET /P input=Please enter a value to generate your project: 

IF "%input%"=="1" GOTO VS2019_CLEARCACHE
IF "%input%"=="2" GOTO VS2019_USECACHE

ELSE GOTO VS2019_CLEARCACHE_FETCH

:VS2019_CLEARCACHE
cmake/build/_generateVC2019_x64.bat %*
ECHO Project generation complete
GOTO End

:VS2019_USECACHE
cmake/build/_cached_generateVC2019_x64.bat %*
ECHO Project generation complete
GOTO End

:End