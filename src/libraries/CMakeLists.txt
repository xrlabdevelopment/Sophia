# --------------------------------
# Validation macro
# --------------------------------
macro(VALIDATE_DIRECTORY DIRECTORY)
IF(NOT EXISTS ${DIRECTORY} OR NOT IS_DIRECTORY "${DIRECTORY}")
    file(MAKE_DIRECTORY ${DIRECTORY})
    message(FATAL_ERROR "Directory, ${DIRECTORY} does not exist, directory was generated but configuration will be stopped.")
ENDIF()
endmacro(VALIDATE_DIRECTORY)

# --------------------------------
# Setup backup path
# --------------------------------
SET(BACKUP_FOLDER "backup")
SET(CURRENT_BACKUP_PATH ${CMAKE_CURRENT_LIST_DIR}/${BACKUP_FOLDER})

VALIDATE_DIRECTORY(${CURRENT_BACKUP_PATH})

# --------------------------------
# Setup core path
# --------------------------------
SET(CORE_FOLDER "core")
SET(CURRENT_CORE_PATH ${CMAKE_CURRENT_LIST_DIR}/${CORE_FOLDER})

VALIDATE_DIRECTORY(${CURRENT_CORE_PATH})
# --------------------------------
# Setup editor path
# --------------------------------
SET(EDITOR_FOLDER "editor")
SET(CURRENT_EDITOR_PATH ${CMAKE_CURRENT_LIST_DIR}/${EDITOR_FOLDER})

VALIDATE_DIRECTORY(${CURRENT_EDITOR_PATH})

# --------------------------------
# Add subdirectories
# --------------------------------
add_subdirectory(backup)
add_subdirectory(core)
add_subdirectory(editor)