# -------------------------------
# Unity
# -------------------------------
SET(UNITY_INSTALL_LOCATION "C:/Program Files/Unity/Hub/Editor")
SET(SOPHIA_UNITY_INSTALL_DIRECTORY ${UNITY_INSTALL_LOCATION} CACHE PATH "Setup Unity install directory")

SET(UNITY_VERSION "2019.3.0f3")
SET(SOPHIA_UNITY_VERSION ${UNITY_VERSION} CACHE STRING "Setup Unity version")

SET(UNITY_ENGINE_DLL "${SOPHIA_UNITY_INSTALL_DIRECTORY}/${SOPHIA_UNITY_VERSION}/${UNITY_BINDIR}/UnityEngine.dll")
SET(UNITY_EDITOR_DLL "${SOPHIA_UNITY_INSTALL_DIRECTORY}/${SOPHIA_UNITY_VERSION}/${UNITY_BINDIR}/UnityEditor.dll")

# When the install location is not found or when the unity version is not found,
#   we cannot continue generating the project.
if(NOT EXISTS "${SOPHIA_UNITY_INSTALL_DIRECTORY}")
    message(FATAL_ERROR "Could not find Unity install location")
endif()
if(NOT EXISTS "${SOPHIA_UNITY_INSTALL_DIRECTORY}/${SOPHIA_UNITY_VERSION}")
    message(FATAL_ERROR "Could not find Unity version")
endif()

message(STATUS "Generation started with Unity version: ${SOPHIA_UNITY_VERSION}")

# --------------------------------
# Add subdirectories
# --------------------------------
message(STATUS "#--------- Adding libraries ---------")
add_subdirectory(libraries)