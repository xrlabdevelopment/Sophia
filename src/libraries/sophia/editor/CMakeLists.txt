SET (EDITOR_LIBRARY_NAME sophia_editor)

message(STATUS "Adding " ${EDITOR_LIBRARY_NAME} " library")

# --------------------------
# Setup sources
# --------------------------
file(GLOB_RECURSE TARGET_SRC "src/*.cs")

GROUPSOURCES(src)

ADD_LIBRARY(${EDITOR_LIBRARY_NAME} SHARED
            ${TARGET_SRC}) 
            
SETUP_LIBRARY(${EDITOR_LIBRARY_NAME})  

# --------------------------
# Folder structure in the IDE
# --------------------------
set_target_properties(${EDITOR_LIBRARY_NAME} PROPERTIES     FOLDER          "1. Libraries"
                                                            DEFINE_SYMBOL   "")

# --------------------------------
# Setup working directory
# --------------------------------
SETUP_WORKING_DIRECTORY(${EDITOR_LIBRARY_NAME})

# --------------------------
# Target link libraries.
# --------------------------
set_property(TARGET ${EDITOR_LIBRARY_NAME} PROPERTY VS_DOTNET_TARGET_FRAMEWORK_VERSION "v4.0")
set_property(TARGET ${EDITOR_LIBRARY_NAME} PROPERTY VS_DOTNET_REFERENCES 
                    "System"
                    "${UNITY_ENGINE_DLL}"
                    "${UNITY_EDITOR_DLL}")

target_link_libraries(${EDITOR_LIBRARY_NAME} PUBLIC sophia_core)

# --------------------------
# Install.
# --------------------------
SETUP_INSTALL_DIRECTORIES(${EDITOR_LIBRARY_NAME} sophia_editor_targets)

INSTALL_DIRECTORY("${CURRENT_EDITOR_PATH}/src/" ${SOPHIA_INC_INSTALL_DIR})