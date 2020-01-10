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

# --------------------------------
# Add subdirectories
# --------------------------------
add_subdirectory(core)
add_subdirectory(platform)
add_subdirectory(editor)