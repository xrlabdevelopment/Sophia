SET (PLATFORM_LIBRARY_NAME sophia_platform)

message(STATUS "Generating ${PLATFORM_LIBRARY_NAME}")

# --------------------------
# Setup sources
# --------------------------
file(GLOB_RECURSE TARGET_SRC "src/*.cs")

GROUPSOURCES(src)

ADD_LIBRARY(${PLATFORM_LIBRARY_NAME} SHARED
            ${TARGET_SRC}) 
            
SETUP_LIBRARY(${PLATFORM_LIBRARY_NAME})  

# --------------------------
# Target properties
# --------------------------
set_target_properties( ${PLATFORM_LIBRARY_NAME}       PROPERTIES    VS_DEBUGGER_WORKING_DIRECTORY   "${OUTPUT_BINDIR}"  # Working directory
                                                                    FOLDER                          "1. Libraries"      # Project filter location
                                                                    DEFINE_SYMBOL                   "" )                # Default library symbol can be empty ( already set )                                   
# --------------------------
# Target include directories.
# --------------------------
target_include_directories(${PLATFORM_LIBRARY_NAME}   PUBLIC        $<BUILD_INTERFACE:${CURRENT_PLATFORM_PATH}/src>
                                                                    $<INSTALL_INTERFACE:${CMAKE_INSTALL_INCLUDEDIR}>)

# --------------------------
# Target compile options.
# --------------------------
target_compile_options(${PLATFORM_LIBRARY_NAME} PRIVATE "/langversion:6")

# --------------------------
# Target link libraries.
# --------------------------
set_property(TARGET ${PLATFORM_LIBRARY_NAME} PROPERTY VS_DOTNET_TARGET_FRAMEWORK_VERSION "v4.5")
set_property(TARGET ${PLATFORM_LIBRARY_NAME} PROPERTY VS_DOTNET_REFERENCES 
                    "System"
                    "${UNITY_ENGINE_DLL}")

target_link_libraries(${PLATFORM_LIBRARY_NAME} PUBLIC sophia_core)

# --------------------------
# Install.
# --------------------------
SETUP_INSTALL_DIRECTORIES(${PLATFORM_LIBRARY_NAME} sophia_platform_targets ${SOPHIA_UNITY_VERSION})

INSTALL_DIRECTORY("${CURRENT_PLATFORM_PATH}/src/" ${CMAKE_INSTALL_INCLUDEDIR})
INSTALL_PDB(${PLATFORM_LIBRARY_NAME} ${SOPHIA_UNITY_VERSION})

message(STATUS "Generation ${PLATFORM_LIBRARY_NAME} \t- done")
