import os
import sys
import string
import shutil
import logging
import glob

from ctypes import windll
from datetime import date

# write msbuild directory to disk
def write_msbuild_dir(filename, directory):
	with open(filename, 'w') as file:
			file.write(directory)
	print("MSBuild directory was saved to disk.")

# write unity data to disk
def write_to_disk(data, filename):
	with open(filename, 'w') as file:
		for d in data:
			file.write(d + "\n")
	print("Unity data was saved to disk.")

# retrieve all "drives" on this machine
# eg.   C:\
#       D:\
def get_drives():
	drives = []
	bitmask = windll.kernel32.GetLogicalDrives()
	for letter in string.ascii_uppercase:
		if bitmask & 1:
			drives.append(letter)
		bitmask >>= 1

	return drives

# look for msbuild installed on this machine and return the location
# retrieve the latest version
def get_msbuild_dir(vs_version_years, vs_version_types):
	msbuild_dirs = []
	
	drives = get_drives()
	for drive in drives:
		# when one install location is found on a specific drive we have enough
		if len(msbuild_dirs) > 0 and not parse_all:
			break
		
		start = drive + ":\\"
		
		print("Looking for MSBuild.exe in: " + start)
		for root, dirnames, filenames in os.walk(start):
			if "MSBuild.exe" not in filenames:
				continue;
			for vs_version_type in vs_version_types:
				for vs_version_year in vs_version_years:
					if vs_version_type in root and str(vs_version_year) in root and "amd" not in root:
						msbuild_dirs.append(root)
								
	print("Found versions of MSBuild.exe: ")
	for msbuild_dir in msbuild_dirs:
		print("\t" + msbuild_dir)
	
	# sort on years, retrieve latest version
	# 2019, 2017, 2015, ...
	vs_version_years.sort(reverse = True)
	for vs_version_year in vs_version_years:
		for msbuild_dir in msbuild_dirs :
			if str(vs_version_year) in msbuild_dir:
				return msbuild_dir
				
	return ""

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
def get_installed_unity_version_directories(supported_versions, parse_all):
	installed_unity_versions = []

	drives = get_drives()
	for drive in drives:

		# when one install location is found on a specific drive we have enough
		if len(installed_unity_versions) > 0 and not parse_all:
			return installed_unity_versions


		start = drive + ":\\"
		print ("Looking for Unity.exe in: " + start)

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

# main function - entry point of the python script
if __name__ == '__main__':
	# Warning: this only works when the script is called from the sophia root dir; e.g. through a .bat file
	dae_source = os.getcwd() + "\\"
	dae_build = dae_source + "..\\VC2019_x64\\"
	
	# If CMakeFiles are always present alongside csproj files then this block can eliminated
	root = dae_build + "src\\libraries\\"
	dirs = []
	for x, y, z in os.walk(root):
		if "CMakeFiles" in y:
			dirs.append(os.path.normpath(x))

	parent_folders = set([os.path.normpath(os.path.dirname(d)) for d in dirs])
	projects = set(dirs) - parent_folders
	
		   
	os.system("cls")

	print(f"Argument List: \t{str(sys.argv)}\n")

	# Parse arguments
	parse_all = False
	latest = False
	if "-parse_all" in sys.argv:
		parse_all = True
		print("\tParsing all drives")
	if "-master" in sys.argv:
		print("\tChecking out master, connecting to repository ...")
		os.system("git checkout master")
	if "-reload" in sys.argv:
		print("\tReloading all directories")
		os.system("del installed_unity_version_directories.txt")
		os.system("del installed_unity_versions.txt")
		os.system("del msbuild_dir.txt")
	if "-latest" in sys.argv:
		print("Searching for latest versions of Unity and VS")
		latest = True
		parse_all = True # If we don't parse all versions we won't know whether the last one has been selected 


	# this if check is only present to layout the terminal a bit more.
	if len(sys.argv) > 1:
		print("")
		
	print("Pulling changes from repository ...")
	os.system("git pull")

	print("")
	print("Source directory: \t" + dae_source)
	print("Build directory: \t" + dae_build)
	
	# ---------------------------------------------------
	# Look for MSBuild
	print("--------------------------------------------")
	print("Starting MSBuild search ...")
	print("")
	
	# Supported VS versions
	vs_version_years = [2019, 2017]
	vs_version_types = ["Enterprise","Professional","Community"]

	if latest:
		vs_version_years = vs_version_years[0]

	print("Supported VS version years: " + str(vs_version_years))
	print("Supported VS version types: " + str(vs_version_types))
	
	# Execute search
	msbuild_directory = ""
	msbuild_path = ""

	# Load the directory from disk if present
	# Save the directory to disk otherwise
	if os.path.isfile('msbuild_dir.txt'):
		file = open("msbuild_dir.txt", "r")
		msbuild_directory = file.read()

		if not os.path.isdir(msbuild_directory) or msbuild_directory == "":    # if the directory is not valid anymore, reload it
			msbuild_directory = get_msbuild_dir(vs_version_years, vs_version_types)
			write_msbuild_dir("msbuild_dir.txt", msbuild_dir)
	else:
		msbuild_directory = get_msbuild_dir(vs_version_years, vs_version_types)
		write_msbuild_dir("msbuild_dir.txt", msbuild_directory)

	msbuild_path = msbuild_directory + "\\MSBuild.exe"
	
	if msbuild_directory == "":
		print("")
		print("MSBuild.exe was not found, exiting ...")
		os.system("exit")
	else:
		print("")
		print("We picked MSBuild.exe in: " + msbuild_directory)
	print("--------------------------------------------") 
	# ---------------------------------------------------
	
	# ---------------------------------------------------\
	# Look for Unity versions
	print("--------------------------------------------")
	print("Starting Unity search ...")
	print("")
	supported_versions = get_supported_unity_version_years(3)

	print("Supported Unity years: " + str(supported_versions))
	
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
			installed_unity_version_directories = get_installed_unity_version_directories(supported_versions, parse_all)
			installed_unity_versions = get_installed_unity_versions(installed_unity_version_directories)
			
			write_to_disk(installed_unity_version_directories, "installed_unity_version_directories.txt")
			write_to_disk(installed_unity_versions, "installed_unity_versions.txt")
		else:
			installed_unity_versions = versions_content.split("\n")
	else:
		installed_unity_version_directories = get_installed_unity_version_directories(supported_versions, parse_all)
		installed_unity_versions = get_installed_unity_versions(installed_unity_version_directories)

		write_to_disk(installed_unity_version_directories, "installed_unity_version_directories.txt")
		write_to_disk(installed_unity_versions, "installed_unity_versions.txt")

	if installed_unity_version_directories[-1] == "":
		installed_unity_version_directories.pop()
	if installed_unity_versions[-1] == "":
		installed_unity_versions.pop()

	if latest:
		print("Latest Unity version selected - ignoring other versions")
		installed_unity_versions = [installed_unity_versions[-1]]
		installed_unity_version_directories = [installed_unity_version_directories[-1]]

	print("Unity version directories:")
	for installed_unity_version_directory in installed_unity_version_directories:
		print("\t" + installed_unity_version_directory)
	print("Unity versions:")
	for installed_unity_version in installed_unity_versions:
		print("\t" + installed_unity_version)
	
	if len(installed_unity_version_directories) != len(installed_unity_versions):
		logging.error("Size of unity directories and unity versions is not the same this cannot happen!")
		os.system("exit")
	
	print ("")
	print ("Found Unity versions")
	print("--------------------------------------------")

	# ---------------------------------------------------
	# run CMAKE
	print("--------------------------------------------")
	print("Build CMAKE command")
	print("")
	cmake_shadow_build = "VC2019_x64"
	cmake_IDE = "Visual Studio 16"
	cmake_make_arguments = "-A"
	cmake_build_target = "x64"

	for i in range(len(installed_unity_version_directories)):
		
		print("Processing Unity versions")
		print("directory: \t" + installed_unity_version_directories[i] + "\\..\\..")
		print("version: \t" + installed_unity_versions[i])
		print("")

		cmake_unity_dir = "-DSOPHIA_UNITY_INSTALL_DIRECTORY:STRING=" + installed_unity_version_directories[i] + "\\..\\.."
		cmake_unity_ver = "-DSOPHIA_UNITY_VERSION:STRING=" + installed_unity_versions[i]

		all_arguments = [cmake_shadow_build, 
						 cmake_IDE,
						 cmake_make_arguments,
						 cmake_build_target,
						 cmake_unity_dir,
						 cmake_unity_ver]

		project_generation_script = "\"\"" + dae_source + "\\cmake\\build\\generateProject.py\" "
		print("Project generation script: \t" + project_generation_script)

		argument_string = ""
		for arg in all_arguments:
			argument_string += "\"" + arg + "\" "
			print("argument: \t" + "\"" + arg + "\" ")
		
		print("")
		
		# remove cmake cache if present
		if os.path.isdir(dae_build + "CMakeFiles"):
			shutil.rmtree(dae_build + "CMakeFiles")
		if os.path.isfile(dae_build + "CMakeCache.txt"):
			os.remove(dae_build + "CMakeCache.txt")

		os.system("python " + project_generation_script + argument_string)

		print("")
		print("CMAKE project generation complete")
		print("--------------------------------------------")

		# ---------------------------------------------------
		# Build the project
		print("--------------------------------------------")
		print("Start build process of sophia ...")


		# Make a list of all the csprojects and the installer in their parent directory
		csprojects = []
		libroot = []
		for proj in projects:
			csprojects += glob.glob(os.path.join(proj, "*.csproj"))
			libroot += glob.glob(os.path.join(os.path.dirname(proj), "INSTALL.vcxproj"))
		for project in list(csprojects + libroot):
			os.system(f"\"\"{msbuild_path}\" \"{project}\" /p:Configuration=Debug /p:Platform=\"x64\"\"")
			os.system(f"\"\"{msbuild_path}\" \"{project}\" /p:Configuration=Release /p:Platform=\"x64\"\"")

		print("Build complete")
		print("--------------------------------------------")

		print("Project successfully installed!")