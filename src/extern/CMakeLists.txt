# -------------------------------
# Setup GIT
# Setup Externals
# 
# When git IS NOT installed no fetching will be done.
# 	In this case the externals need to be copied manually.
# When git IS installed CMake will expect that the the working directory is a git repository.
# -------------------------------
find_package(Git QUIET)

if(GIT_FOUND)
	if(EXISTS "${PROJECT_SOURCE_DIR}/.git")
		option(SOPHIA_FETCH_SUBMODULE "Fetch Submodules" OFF)
	
		if(SOPHIA_FETCH_SUBMODULE)

			set(SUBMODULE_STATUS)

			# This will update all submodules
			message(STATUS "Fetching submodules ...")
			execute_process(COMMAND ${GIT_EXECUTABLE} submodule update --init --remote
							WORKING_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}
							RESULT_VARIABLE GIT_SUBMOD_RESULT)

			execute_process(COMMAND ${GIT_EXECUTABLE} submodule status
							WORKING_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}
							OUTPUT_VARIABLE SUBMODULE_STATUS)
				
			# Log a message with the status of the included submodules
			message(STATUS ${SUBMODULE_STATUS})

			if(NOT GIT_SUBMOD_RESULT EQUAL "0")
				message(FATAL_ERROR "git submodule update --init failed with ${GIT_SUBMOD_RESULT}, please checkout submodules")
			endif()
			
		endif()
	else()
	# Log a message when we are not a git repository
	message(FATAL_ERROR "${PROJECT_NAME} is not a git repository")
	endif()
else()
# Log a message when the git package was not found
message(FATAL_ERROR "Git package was not found, did you install GIT?")
endif()

# --------------------------------
# Newtonsoft.Json
# Apperantly Newtonsoft.Json can come preinstalled at certain locations.
#
# If it does, VS automatically includes Newtonsoft.Json as a reference when required.
# If that is the case we do not need to add our version of Newtonsoft.Json.
# If that is not the case we process the CMakeList of our own version of Newtonsoft.Json and add it to Sophia.
# --------------------------------
SET (IIS_PATH "C:/Program Files/IIS")
SET (IIS_EXPRESS_PATH "C:/Program Files/IIS Express")

SET (SOPHIA_FOUND_NEWTONSOFT)
foreach(install_path ${IIS_PATH} ${IIS_EXPRESS_PATH})
    find_file(  
                SOPHIA_FOUND_NEWTONSOFT
                NAMES Newtonsoft.Json.dll
                PATHS ${install_path}
                PATH_SUFFIXES "Microsoft Web Deploy V3"
             )
endforeach(install_path)

# --------------------------------
# Add subdirectories
# --------------------------------
if(${SOPHIA_FOUND_NEWTONSOFT} STREQUAL "SOPHIA_FOUND_NEWTONSOFT-NOTFOUND")
    MESSAGE(STATUS "Newtonsoft.Json not found, adding reference ...")
    add_subdirectory(json)
else()
    MESSAGE(STATUS "Newtonsoft.Json found at: ${SOPHIA_FOUND_NEWTONSOFT}")
endif()