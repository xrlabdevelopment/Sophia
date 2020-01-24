SET (CORE_LIBRARY_NAME sophia_core)

message(STATUS "Adding " ${CORE_LIBRARY_NAME} " library")

# --------------------------
# Setup sources
# --------------------------
file(GLOB_RECURSE TARGET_SRC "src/*.cs")

GROUPSOURCES(src)

ADD_LIBRARY(${CORE_LIBRARY_NAME} SHARED
            ${TARGET_SRC}) 
            
SETUP_LIBRARY(${CORE_LIBRARY_NAME})            

# --------------------------
# Folder structure in the IDE
# --------------------------
set_target_properties(${CORE_LIBRARY_NAME} PROPERTIES   FOLDER          "1. Libraries"
                                                        DEFINE_SYMBOL   "")

# --------------------------------
# Setup working directory
# --------------------------------
SETUP_WORKING_DIRECTORY(${CORE_LIBRARY_NAME})

# --------------------------
# Target include directories.
# --------------------------
target_include_directories(${CORE_LIBRARY_NAME} PUBLIC     
    $<BUILD_INTERFACE:${CURRENT_CORE_PATH}/src>
    $<INSTALL_INTERFACE:${CMAKE_INSTALL_INCLUDEDIR}>)
                                           
# --------------------------
# Target link libraries.
# --------------------------
set_property(TARGET ${CORE_LIBRARY_NAME} PROPERTY VS_DOTNET_TARGET_FRAMEWORK_VERSION "v4.5")
set_property(TARGET ${CORE_LIBRARY_NAME} PROPERTY VS_DOTNET_REFERENCES 
                    "System")

target_link_libraries(${CORE_LIBRARY_NAME} PUBLIC "Newtonsoft.Json")

# --------------------------
# Install.
# --------------------------
SETUP_INSTALL_DIRECTORIES(${CORE_LIBRARY_NAME} sophia_core_targets ${SOPHIA_UNITY_VERSION})

INSTALL_DIRECTORY("${CURRENT_CORE_PATH}/src/" ${SOPHIA_INC_INSTALL_DIR})
INSTALL_PDB(${CORE_LIBRARY_NAME} ${SOPHIA_UNITY_VERSION})

# --------------------------
# Tests.
# --------------------------
if(SOPHIA_ENABLE_TESTS)
    SET (TEST_CORE_LIBRARY_NAME "${CORE_LIBRARY_NAME}_test")

    file(GLOB TEST_TARGET_SRC ${CURRENT_CORE_PATH}/tests/*.cs)
    add_executable(${TEST_CORE_LIBRARY_NAME} ${TEST_TARGET_SRC})

    set_property(TARGET ${TEST_CORE_LIBRARY_NAME} PROPERTY VS_DOTNET_TARGET_FRAMEWORK_VERSION "v4.0")
    set_property(TARGET ${TEST_CORE_LIBRARY_NAME} PROPERTY VS_DOTNET_REFERENCES 
                        "System"
                        "Microsoft.VisualStudio.QualityTools.UnitTestFramework")

    target_link_libraries(${TEST_CORE_LIBRARY_NAME} PUBLIC ${CORE_LIBRARY_NAME})

    # --------------------------
    # Folder structure in the IDE
    # --------------------------
    set_target_properties(${TEST_CORE_LIBRARY_NAME}         PROPERTIES   FOLDER          "2. Tests"
                                                            DEFINE_SYMBOL   "")

    add_test(core_test ${TEST_CORE_LIBRARY_NAME})
endif()
