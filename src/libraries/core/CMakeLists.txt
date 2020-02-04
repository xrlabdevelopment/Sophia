SET (CORE_LIBRARY_NAME sophia_core)

message(STATUS "Generating ${CORE_LIBRARY_NAME}")

# --------------------------
# Setup sources
# --------------------------
file(GLOB_RECURSE TARGET_SRC "src/*.cs")

GROUPSOURCES(src)

ADD_LIBRARY(${CORE_LIBRARY_NAME} SHARED
            ${TARGET_SRC}) 
            
SETUP_LIBRARY(${CORE_LIBRARY_NAME})            

# --------------------------
# Target properties
# --------------------------
set_target_properties( ${CORE_LIBRARY_NAME}         PROPERTIES      VS_DEBUGGER_WORKING_DIRECTORY   "${OUTPUT_BINDIR}"  # Working directory
                                                                    FOLDER                          "1. Libraries"      # Project filter location
                                                                    DEFINE_SYMBOL                   "" )                # Default library symbol can be empty ( already set )
# --------------------------
# Target include directories.
# --------------------------
target_include_directories(${CORE_LIBRARY_NAME}     PUBLIC          $<BUILD_INTERFACE:${CURRENT_CORE_PATH}/src>
                                                                    $<INSTALL_INTERFACE:${CMAKE_INSTALL_INCLUDEDIR}>)
                                           
# --------------------------
# Target link libraries.
# --------------------------
set_property(TARGET ${CORE_LIBRARY_NAME} PROPERTY VS_DOTNET_TARGET_FRAMEWORK_VERSION "v4.5")
set_property(TARGET ${CORE_LIBRARY_NAME} PROPERTY VS_DOTNET_REFERENCES 
                    "System")

# --------------------------
# Install.
# --------------------------
SETUP_INSTALL_DIRECTORIES(${CORE_LIBRARY_NAME} sophia_core_targets ${SOPHIA_UNITY_VERSION})

INSTALL_DIRECTORY("${CURRENT_CORE_PATH}/src/" ${CMAKE_INSTALL_INCLUDEDIR})
INSTALL_PDB(${CORE_LIBRARY_NAME} ${SOPHIA_UNITY_VERSION})

message(STATUS "Generation ${CORE_LIBRARY_NAME} \t- done")