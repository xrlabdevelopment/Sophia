# ---------------------------------------------
# Imports
# ---------------------------------------------
import string
import os

from ctypes import windll
from datetime import date

# retrieve all "drives" on this machine
# eg.   C:\
#       D:\
def get_drives():
    drives = []
    bitmask = windll.kernel32.GetLogicalDrives()
    for letter in string.uppercase:
        if bitmask & 1:
            drives.append(letter)
        bitmask >>= 1

    return drives
    
# retrieve all supported unity versions
# this is done by using the year a unity version was released
# eg.   2018 => all unity versions from 2018 are supported
#       2019 => all unity versions from 2019 are supported        
def get_supported_unity_version_years(supported_years):
    current_year = date.today().year
    
    supported_versions = []
    for i in range(supported_years):
        supported_versions.append(current_year - i)
        
    return supported_versions

# retrieve all installed unity versions on disk
# this is done by traversing over all directories on this machine
# when an executable named "Unity.exe" is found the directory is checked if it contains one of the supported versions + " Editor " 
# eg.   2018.../Editor
#       2019.../Editor        
def get_installed_unity_version_directories(supported_versions):
    installed_unity_versions = []

    drives = get_drives()
    for drive in drives:
        start = drive + ":\\"
        print "Looking for Unity.exe in: " + start
                       
        # look for the unity editor executable
        for root, dirnames, filenames in os.walk(start):           
            # parse filenames in directory
            for filename in filenames:
                if filename == "Unity.exe":                
                    # look for a supported unity version
                    for supported_version in supported_versions:
                        if str(supported_version) in root and "Editor" in root:
                            installed_unity_versions.append(root)
                
    return installed_unity_versions

# retrieve all installed unity versions
# eg.   2019.2.15f1
#       2019.3.0f1
def get_installed_unity_versions(installed_unity_version_directories):
    installed_unity_versions = []
    for unity_directory in installed_unity_version_directories:
        if(unity_directory == ""): 
            continue
            
        dirs = unity_directory.split('\\')
        for supported_version in supported_versions:
            for dir in dirs:
                if str(supported_version) in dir:
                    installed_unity_versions.append(dir)
                    
    return installed_unity_versions

# write all found directories to disk so we do not have to look them up again
def write_to_disk(data, filename):
    with open(filename, 'w') as file:
        for d in data:
            file.write(d + "\n")

# main function - entry point of the python script
if __name__ == '__main__':
    supported_versions = get_supported_unity_version_years(3)

    installed_unity_version_directories = []
    installed_unity_versions = []
    
    if os.path.isfile('installed_unity_version_directories.txt') and os.path.isfile('installed_unity_versions.txt'):
        dirty = 0
        
        directories_file = open("installed_unity_version_directories.txt", "r")
        directories_file_content = directories_file.read()
        versions_file = open("installed_unity_versions.txt", "r")
        versions_content = versions_file.read()
    
        installed_unity_version_directories = directories_file_content.split("\n")
        for dir in installed_unity_version_directories:
            if dir == "":   
                continue    #empty paths should not be processed
                
            if not os.path.isdir(dir):
                print("Path was not found: " + dir)
                dirty += 1  # our installed unity version directory file is out of date
        
        # when our installed unity version directory file is out of date
        # we write all versions found versions back to disk
        if dirty > 0:
            installed_unity_version_directories = get_installed_unity_version_directories(supported_versions)
            installed_unity_versions = get_installed_unity_versions(installed_unity_version_directories)
            
            write_to_disk(installed_unity_version_directories, "installed_unity_version_directories.txt")
            write_to_disk(installed_unity_versions, "installed_unity_versions.txt")
        else:
            installed_unity_versions = versions_content.split("\n")
        
    else:
        installed_unity_version_directories = get_installed_unity_version_directories(supported_versions)
        installed_unity_versions = get_installed_unity_versions(installed_unity_version_directories)
        
        write_to_disk(installed_unity_version_directories, "installed_unity_version_directories.txt")
        write_to_disk(installed_unity_versions, "installed_unity_versions.txt")
    
    installed_unity_version_directories.pop()   # trailing "\n" gives an empty element at the end
    installed_unity_versions.pop()              # trailing "\n" gives an empty element at the end
    
    print(installed_unity_version_directories)
    print(installed_unity_versions)