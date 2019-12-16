# ---------------------------------------------
# Imports
# ---------------------------------------------
import string
import os

from ctypes import windll

def get_drives():
    drives = []
    bitmask = windll.kernel32.GetLogicalDrives()
    for letter in string.uppercase:
        if bitmask & 1:
            drives.append(letter)
        bitmask >>= 1

    return drives
    
def get_msbuild_dir():
    drives = get_drives()
    for drive in drives:
        start = drive + ":\\"
        #print "Looking for MSBuild.exe in: " + start
        for dirpath, dirnames, filenames in os.walk(start):
            for filename in filenames:
                if filename == "MSBuild.exe":
                    if "Community" in dirpath or "Professional" in dirpath or "Enterprise" in dirpath:
                        if "2017" in dirpath or "2019" in dirpath:
                            return dirpath

if __name__ == '__main__':
    if os.path.isfile('msbuild_dir.txt'):
        file = open("msbuild_dir.txt", "r")
        msbuild_dir = file.read()
        
        print(msbuild_dir)
    else:
        msbuild_dir = get_msbuild_dir()
        
        print(msbuild_dir)
        with open('msbuild_dir.txt', 'w') as file:
            file.write(msbuild_dir)