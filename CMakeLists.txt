# -------------------------------
# Setup CMake minimum version
# -------------------------------
cmake_minimum_required(VERSION 3.10)

# -------------------------------
# Sophia Version
# -------------------------------
SET(VERSION_MAJOR 0)
SET(VERSION_MINOR 1)
SET(VERSION_PATCH 0)

# -------------------------------
# Configurations
# -------------------------------
SET(CONFIGURATION_DEBUG Debug)
SET(CONFIGURATION_RELWITHDEBINFO RelWithDebInfo)
SET(CONFIGURATION_MINSIZEREL Release)
SET(CONFIGURATION_RELEASE Release)

# -------------------------------
# Setup the "DAE" solution
# -------------------------------
project(DAE VERSION     "${VERSION_MAJOR}.${VERSION_MINOR}.${VERSION_PATCH}"
            LANGUAGES   CSharp)

message(STATUS "#--------------------- Starting project generation ---------------------")
message("CMake Version: \t\t${CMAKE_VERSION}")
message("Architecture: \t\t${CMAKE_GENERATOR_PLATFORM}")
message("Generating project: \t${PROJECT_NAME} v${VERSION_MAJOR}.${VERSION_MINOR}.${VERSION_PATCH}")

# -------------------------------
# CMake dependencies
# -------------------------------
include(cmake\\directories.txt)
include(cmake\\macros.txt)

include(GNUInstallDirs)

# --------------------------
# Setup global properties
# --------------------------
SET_PROPERTY(GLOBAL PROPERTY USE_FOLDERS ON)
SET(CMAKE_DEBUG_POSTFIX _d)

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

message("Output directory: \t\t${OUTPUT_BINDIR}")

add_subdirectory(${SRC_DIR})

# --------------------------------
# Install readme and license file.
# --------------------------------
# Resources
# 	- branding
install(DIRECTORY 	"${CMAKE_CURRENT_SOURCE_DIR}/resources"
    	DESTINATION "${CMAKE_INSTALL_DATADIR}/${PROJECT_NAME}"
    	FILE_PERMISSIONS OWNER_READ OWNER_WRITE OWNER_EXECUTE GROUP_READ WORLD_READ
    	DIRECTORY_PERMISSIONS OWNER_READ OWNER_WRITE OWNER_EXECUTE GROUP_READ GROUP_EXECUTE WORLD_READ WORLD_EXECUTE)

# Documentation
# 	- License
# 	- Readme
install(FILES 		"${CMAKE_CURRENT_SOURCE_DIR}/LICENSE" "${CMAKE_CURRENT_SOURCE_DIR}/README.md"
    	DESTINATION "${CMAKE_INSTALL_DATADIR}/${PROJECT_NAME}"
		PERMISSIONS OWNER_READ GROUP_READ WORLD_READ)

message(STATUS "#---------------------- Ending project generation ----------------------")	