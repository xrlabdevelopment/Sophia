# -------------------------------
# Setup CMake minimum version
# -------------------------------
cmake_minimum_required(VERSION 3.10)

message(STATUS "Architecture: ${CMAKE_GENERATOR_PLATFORM}")

# -------------------------------
# Setup the "DAE" solution
# -------------------------------
include(cmake\\version.txt)

project(DAE VERSION     "${VERSION_MAJOR}.${VERSION_MINOR}.${VERSION_PATCH}"
            LANGUAGES   CSharp)

message(STATUS "Generating project: ${PROJECT_NAME} v${VERSION_MAJOR}.${VERSION_MINOR}.${VERSION_PATCH}")

# -------------------------------
# CMake dependencies
# -------------------------------
include(GNUInstallDirs)

include(cmake\\directories.txt)
include(cmake\\macros.txt)

# --------------------------
# Setup global properties
# --------------------------
SET_PROPERTY(GLOBAL PROPERTY USE_FOLDERS ON)
SET(CMAKE_DEBUG_POSTFIX _d)

OPTION(SOPHIA_ENABLE_TESTS "Add tests to the solution" OFF)

# ------------------------
# Setup CMAKE output directories
# ------------------------
SET (CMAKE_RUNTIME_OUTPUT_DIRECTORY_DEBUG           "${OUTPUT_BINDIR}/${CONFIGURATION_DEBUG}")
SET (CMAKE_RUNTIME_OUTPUT_DIRECTORY_MINSIZEREL      "${OUTPUT_BINDIR}/${CONFIGURATION_MINSIZEREL}")
SET (CMAKE_RUNTIME_OUTPUT_DIRECTORY_RELWITHDEBINFO  "${OUTPUT_BINDIR}/${CONFIGURATION_RELWITHDEBINFO}")
SET (CMAKE_RUNTIME_OUTPUT_DIRECTORY_RELEASE         "${OUTPUT_BINDIR}/${CONFIGURATION_RELEASE}")
SET (CMAKE_LIBRARY_OUTPUT_DIRECTORY_DEBUG           "${OUTPUT_BINDIR}/${CONFIGURATION_DEBUG}")
SET (CMAKE_LIBRARY_OUTPUT_DIRECTORY_MINSIZEREL      "${OUTPUT_BINDIR}/${CONFIGURATION_MINSIZEREL}")
SET (CMAKE_LIBRARY_OUTPUT_DIRECTORY_RELWITHDEBINFO  "${OUTPUT_BINDIR}/${CONFIGURATION_RELWITHDEBINFO}")
SET (CMAKE_LIBRARY_OUTPUT_DIRECTORY_RELEASE         "${OUTPUT_BINDIR}/${CONFIGURATION_RELEASE}")
SET (CMAKE_ARCHIVE_OUTPUT_DIRECTORY_DEBUG           "${OUTPUT_BINDIR}/${CONFIGURATION_DEBUG}")
SET (CMAKE_ARCHIVE_OUTPUT_DIRECTORY_MINSIZEREL      "${OUTPUT_BINDIR}/${CONFIGURATION_MINSIZEREL}")
SET (CMAKE_ARCHIVE_OUTPUT_DIRECTORY_RELWITHDEBINFO  "${OUTPUT_BINDIR}/${CONFIGURATION_RELWITHDEBINFO}")
SET (CMAKE_ARCHIVE_OUTPUT_DIRECTORY_RELEASE         "${OUTPUT_BINDIR}/${CONFIGURATION_RELEASE}")

message(STATUS "Output directory: ${OUTPUT_BINDIR}")

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
		option(SOPHIA_FETCH_SUBMODULE "Fetch Submodules" ON)
	
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

add_subdirectory(src)

# --------------------------------
# Install readme and license file.
# --------------------------------
INSTALL_DIRECTORY("${CMAKE_CURRENT_SOURCE_DIR}/resources" ${SOPHIA_ADDITIONAL_FILES_INSTALL_DIR})

install(FILES "${CMAKE_CURRENT_SOURCE_DIR}/LICENSE" "${CMAKE_CURRENT_SOURCE_DIR}/README.md"
    	DESTINATION ${SOPHIA_ADDITIONAL_FILES_INSTALL_DIR}
		PERMISSIONS OWNER_READ GROUP_READ WORLD_READ)