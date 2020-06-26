SET (DEPRECATED_LIBRARY_NAME sophia_deprecated)

message(STATUS "Generating ${DEPRECATED_LIBRARY_NAME}")

# --------------------------
# Setup sources
# --------------------------
file(GLOB_RECURSE TARGET_SRC "src/*.cs")

GROUPSOURCES(src)

ADD_LIBRARY(${DEPRECATED_LIBRARY_NAME} SHARED
            ${TARGET_SRC}) 
            
SETUP_LIBRARY(${DEPRECATED_LIBRARY_NAME})            

# --------------------------
# Target properties
# --------------------------
set_target_properties( ${DEPRECATED_LIBRARY_NAME}   PROPERTIES    VS_DEBUGGER_WORKING_DIRECTORY   "${OUTPUT_BINDIR}"    # Working directory
                                                                  FOLDER                          "1. Libraries"        # Project filter location
                                                                  DEFINE_SYMBOL                   "" )                  # Default library symbol can be empty ( already set )
# --------------------------
# Target include directories.
# --------------------------
target_include_directories(${DEPRECATED_LIBRARY_NAME}     PUBLIC    $<BUILD_INTERFACE:${CURRENT_BACKUP_PATH}/src>
                                                                    $<INSTALL_INTERFACE:${CMAKE_INSTALL_INCLUDEDIR}>)

# --------------------------
# Target compile options.
# --------------------------
target_compile_options(${DEPRECATED_LIBRARY_NAME} PRIVATE "/langversion:6")
                                           
# --------------------------
# Target link libraries.
# --------------------------
set_property(TARGET ${DEPRECATED_LIBRARY_NAME} PROPERTY VS_DOTNET_TARGET_FRAMEWORK_VERSION "v4.5")
set_property(TARGET ${DEPRECATED_LIBRARY_NAME} PROPERTY VS_DOTNET_REFERENCES 
                    "System"
                    "System.Xml"
                    "${UNITY_ENGINE_DLL}"
                    "${UNITY_EDITOR_DLL}")

target_link_libraries(${DEPRECATED_LIBRARY_NAME} PUBLIC sophia_core)

# --------------------------
# Install.
# --------------------------
SETUP_INSTALL_DIRECTORIES(${DEPRECATED_LIBRARY_NAME} sophia_backup_targets ${SOPHIA_UNITY_VERSION})

INSTALL_DIRECTORY("${CURRENT_BACKUP_PATH}/src/" ${CMAKE_INSTALL_INCLUDEDIR})
INSTALL_PDB(${DEPRECATED_LIBRARY_NAME} ${SOPHIA_UNITY_VERSION})

message(STATUS "Generation ${DEPRECATED_LIBRARY_NAME} \t- done")
