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
# Add subdirectories
# --------------------------------
add_subdirectory(json)