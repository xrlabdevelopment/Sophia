SET (PLATFORM_LIBRARY_NAME sophia_platform)

message(STATUS "Adding " ${PLATFORM_LIBRARY_NAME} " library")

# -------------------------------
# Unity
# -------------------------------
SET(UNITY_INSTALL_LOCATION "C:/Program Files/Unity/Hub/Editor")
SET(SOPHIA_UNITY_INSTALL_DIRECTORY ${UNITY_INSTALL_LOCATION} CACHE STRING "Setup Unity install directory")

SET(UNITY_VERSION "2019.3.0f1")
SET(SOPHIA_UNITY_VERSION ${UNITY_VERSION} CACHE STRING "Setup Unity version")

# When the install location is not found or when the unity version is not found,
#   we cannot continue generating the project.
if(NOT EXISTS "${SOPHIA_UNITY_INSTALL_DIRECTORY}")
    message(FATAL_ERROR "Could not find Unity install location")
endif()
if(NOT EXISTS "${SOPHIA_UNITY_INSTALL_DIRECTORY}/${SOPHIA_UNITY_VERSION}")
    message(FATAL_ERROR "Could not find Unity version")
endif()

# --------------------------
# Setup sources
# --------------------------
file(GLOB_RECURSE TARGET_SRC "src/*.cs")

GROUPSOURCES(src)

ADD_LIBRARY(${PLATFORM_LIBRARY_NAME} SHARED
            ${TARGET_SRC}) 
            
SETUP_LIBRARY(${PLATFORM_LIBRARY_NAME})  

# --------------------------
# Folder structure in the IDE
# --------------------------
set_target_properties(${PLATFORM_LIBRARY_NAME} PROPERTIES   FOLDER          "1. Libraries"
                                                        DEFINE_SYMBOL   "")

# --------------------------------
# Setup working directory
# --------------------------------
SETUP_WORKING_DIRECTORY(${PLATFORM_LIBRARY_NAME})

# --------------------------
# Target link libraries.
# --------------------------
set_property(TARGET ${PLATFORM_LIBRARY_NAME} PROPERTY VS_DOTNET_TARGET_FRAMEWORK_VERSION "v4.0")
set_property(TARGET ${PLATFORM_LIBRARY_NAME} PROPERTY VS_DOTNET_REFERENCES 
                    "System"
                    "${SOPHIA_UNITY_INSTALL_DIRECTORY}/${SOPHIA_UNITY_VERSION}/${UNITY_BINDIR}/UnityEngine.dll")

target_link_libraries(${PLATFORM_LIBRARY_NAME} PUBLIC sophia_core)

# --------------------------
# Install.
# --------------------------
SETUP_INSTALL_DIRECTORIES(${PLATFORM_LIBRARY_NAME} sophia_platform_targets)

INSTALL_DIRECTORY("${CURRENT_PLATFORM_PATH}/src/" ${SOPHIA_INC_INSTALL_DIR})