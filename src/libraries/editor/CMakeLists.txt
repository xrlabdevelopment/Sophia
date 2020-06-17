SET (EDITOR_LIBRARY_NAME sophia_editor)

message(STATUS "Generating ${EDITOR_LIBRARY_NAME}")

# --------------------------
# Setup sources
# --------------------------
file(GLOB_RECURSE TARGET_SRC "src/*.cs")

GROUPSOURCES(src)

ADD_LIBRARY(${EDITOR_LIBRARY_NAME} SHARED
            ${TARGET_SRC}) 
            
SETUP_LIBRARY(${EDITOR_LIBRARY_NAME})  

# --------------------------
# Target properties
# --------------------------
set_target_properties( ${EDITOR_LIBRARY_NAME}       PROPERTIES      VS_DEBUGGER_WORKING_DIRECTORY   "${OUTPUT_BINDIR}"  # Working directory
                                                                    FOLDER                          "1. Libraries"      # Project filter location
                                                                    DEFINE_SYMBOL                   "" )                # Default library symbol can be empty ( already set )                                   
# --------------------------
# Target include directories.
# --------------------------
target_include_directories(${EDITOR_LIBRARY_NAME}   PUBLIC          $<BUILD_INTERFACE:${CURRENT_EDITOR_PATH}/src>
                                                                    $<INSTALL_INTERFACE:${CMAKE_INSTALL_INCLUDEDIR}>)

# --------------------------
# Target compile options.
# --------------------------
target_compile_options(${EDITOR_LIBRARY_NAME} PRIVATE "/langversion:6")

# --------------------------
# Target link libraries.
# --------------------------
set_property(TARGET ${EDITOR_LIBRARY_NAME} PROPERTY VS_DOTNET_TARGET_FRAMEWORK_VERSION "v4.5")
set_property(TARGET ${EDITOR_LIBRARY_NAME} PROPERTY VS_DOTNET_REFERENCES 
                    "System"
                    "${UNITY_ENGINE_DLL}"
                    "${UNITY_EDITOR_DLL}")

target_link_libraries(${EDITOR_LIBRARY_NAME} PUBLIC sophia_core)
target_link_libraries(${EDITOR_LIBRARY_NAME} PUBLIC sophia_platform)

# --------------------------
# Install.
# --------------------------
SETUP_INSTALL_DIRECTORIES(${EDITOR_LIBRARY_NAME} sophia_editor_targets ${SOPHIA_UNITY_VERSION})

INSTALL_DIRECTORY("${CURRENT_EDITOR_PATH}/src/" ${CMAKE_INSTALL_INCLUDEDIR})
INSTALL_PDB(${EDITOR_LIBRARY_NAME} ${SOPHIA_UNITY_VERSION})

message(STATUS "Generation ${EDITOR_LIBRARY_NAME} \t- done")
