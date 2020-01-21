@ECHO OFF
ECHO Press 1) Generate VS2019 Win64 with cmake cache cleared ( fetch of submodules )
ECHO Press 2) Generate VS2019 Win64 with cmake cache cleared ( no fetch of submodules )
ECHO Press 3) Generate VS2019 Win64 using cmake cache

SET /P input=Please enter a value to generate your project: 

IF "%input%"=="1" GOTO VS2019_CLEARCACHE_FETCH
IF "%input%"=="2" GOTO VS2019_CLEARCACHE_NOFETCH
IF "%input%"=="3" GOTO VS2019_USECACHE

ELSE GOTO VS2019_CLEARCACHE_FETCH

:VS2019_CLEARCACHE_FETCH
cmake/build/_generateVC2019_x64_fetch_conda.bat %*
ECHO Project generation complete
GOTO End
:VS2019_CLEARCACHE_NOFETCH
cmake/build/_generateVC2019_x64_no_fetch_conda.bat %*
ECHO Project generation complete
GOTO End

:VS2019_USECACHE
cmake/build/_cached_generateVC2019_x64_conda.bat %*
ECHO Project generation complete
GOTO End

:End