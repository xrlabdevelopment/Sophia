# --------------------------------
# Setup core path
# --------------------------------
set(CORE_FOLDER "core")

set(CURRENT_CORE_PATH ${CMAKE_CURRENT_LIST_DIR}/${CORE_FOLDER})

IF(NOT EXISTS ${CURRENT_CORE_PATH} OR NOT IS_DIRECTORY "${CURRENT_CORE_PATH}")
    file(MAKE_DIRECTORY ${CURRENT_CORE_PATH})
    message(FATAL_ERROR "Directory, ${CURRENT_CORE_PATH} does not exist, directory was generated but configuration will be stopped.")
ENDIF()

# --------------------------------
# Setup platform path
# --------------------------------
set(PLATFORM_FOLDER "platform")

set(CURRENT_PLATFORM_PATH ${CMAKE_CURRENT_LIST_DIR}/${PLATFORM_FOLDER})

IF(NOT EXISTS ${CURRENT_PLATFORM_PATH} OR NOT IS_DIRECTORY "${CURRENT_PLATFORM_PATH}")
    file(MAKE_DIRECTORY ${CURRENT_PLATFORM_PATH})
    message(FATAL_ERROR "Directory, ${CURRENT_PLATFORM_PATH} does not exist, directory was generated but configuration will be stopped.")
ENDIF()

# --------------------------------
# Setup editor path
# --------------------------------
set(EDITOR_FOLDER "editor")

set(CURRENT_EDITOR_PATH ${CMAKE_CURRENT_LIST_DIR}/${EDITOR_FOLDER})

IF(NOT EXISTS ${CURRENT_EDITOR_PATH} OR NOT IS_DIRECTORY "${CURRENT_EDITOR_PATH}")
    file(MAKE_DIRECTORY ${CURRENT_EDITOR_PATH})
    message(FATAL_ERROR "Directory, ${CURRENT_EDITOR_PATH} does not exist, directory was generated but configuration will be stopped.")
ENDIF()

# -------------------------------
# Unity
# -------------------------------
SET(UNITY_INSTALL_LOCATION "C:/Program Files/Unity/Hub/Editor")
SET(SOPHIA_UNITY_INSTALL_DIRECTORY ${UNITY_INSTALL_LOCATION} CACHE PATH "Setup Unity install directory")

SET(UNITY_VERSION "2019.3.0f1")
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

# --------------------------------
# Add subdirectories
# --------------------------------
add_subdirectory(core)
add_subdirectory(platform)
add_subdirectory(editor)