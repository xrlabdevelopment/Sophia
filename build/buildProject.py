# Windows only script
import sys
import os

import argparse
import glob
import json

def get_msbuild_dir(args):
	# At the moment there is no need to support different build environments so we can ignore the AMD one
	# Default behaviour: first one found is selected
	msbuild_dir = glob.glob(args.vs_dir + "**\\MSBuild.exe")
	if not msbuild_dir:
		drives = [chr(x) + ":" for x in range(65,90) if os.path.exists(chr(x) + ":")]
		ms_dirs = []
		for drive in drives:
			ms_dirs.extend(glob.glob(drive + "\\**\\Microsoft Visual Studio"))
		
		print(str(len(ms_dirs)) + " msbuild installations found")
		if len(ms_dirs) == 0:
			sys.exit("Did you install Microsoft Visual Studio? If so, run the script again with --vs_dir to your MSBuild directory")

		for ms_dir in ms_dirs:
			for root, dirs, files in os.walk(ms_dir + "\\"):
				if "MSBuild.exe" in files and "amd64" not in root:
					msbuild_dir.append(os.path.join(root, "MSBuild.exe"))
					print(root)
					if not args.latest:
						dirs[:] = [] 												# as soon as we found a file we will exit the loop
						ms_dirs[:] = [] 											# additionally other .exe files in deeper subfolders (amd64) won't be found
	
	if args.latest and len(msbuild_dir) > 1:
		print("Multiple MSBuild dirs found. Using latest.")
		return msbuild_dir[-1]
	else:
		return msbuild_dir[0]

def get_unity_dirs(args):
	unity_dirs = glob.glob(args.unity_dir + "\\**\\Unity.exe", recursive=True)
	if not unity_dirs:
		drives = [chr(x) + ":" for x in range(65,90) if os.path.exists(chr(x) + ":")]
		unity_candidates = []
		for drive in drives:
			unity_candidates.extend(glob.glob(drive + "\\**\\Unity"))
		if not unity_candidates:
			print("You did something weird with your Unity installation and now you shall suffer.")
			unity_dirs.extend(glob.glob(drive + "\\**\\Editor\\Unity.exe", recursive=True))
		else:
			for unity_dir in unity_candidates:
				unity_dirs.extend(glob.glob(unity_dir + "\\**\\Editor\\Unity.exe", recursive=True))
	if len(unity_dirs) == 0:
		sys.exit("Did you install Unity? If so, run the script again with --unity_dir to your Editor directory")
	return unity_dirs

if __name__ == '__main__':
	parser = argparse.ArgumentParser()
	parser.add_argument("-m", "--master", help="Checkout master repository, will trigger update", action="store_true")
	parser.add_argument("-r", "--reload", help="Purge cache and locate VS and Unity installations ", action="store_true")
	parser.add_argument("-l", "--latest", help="Install only for the latest version of Unity and Visual Studio", action="store_true")
	parser.add_argument("-c", "--clear", help="Clear CMake cache from the shadow build", action="store_true")
	parser.add_argument("-u", "--update", help="Pull from repository before building", action="store_true")
	parser.add_argument("--vs_dir", help="Root directory to search for Visual Studio installation", default="C:\\*\\Microsoft Visual Studio\\*\\*\\MSBuild\\Current\\Bin")
	parser.add_argument("--unity_dir", help="Root directory to search for Unity installation", default="C:\\Program Files\\Unity\\Hub\\Editor")

	# Find root dir and shadow build of git repo
	root_dir = os.path.dirname(os.path.abspath(__file__))
	while not os.path.isdir(os.path.join(root_dir, ".git")):
		root_dir = os.path.dirname(root_dir)
		if not root_dir:
			sys.exit("Please place this script in the sophia git repo.")
	print("ROOT: " + root_dir)
	
	cache_file = "build_cache.json"
	project_name = "VC2019_x64"
	shadow_build = os.path.normpath(os.path.join(root_dir, f"..\\{project_name}\\"))
	print("shadow_build: " + shadow_build)
			
	# Check parse args 
	args = parser.parse_args()
	if args.master: 
		print("Checking out master, connecting to repository...")
		os.system("git checkout master")
		args.update = True
	if args.reload: 
		print("Reloading all directories")
		os.system(f"del {cache_file}")
	if args.latest:
		print("Searching for latest versions of Unity and VS")
	if args.update:
		os.system("git pull")
	if args.clear:
		os.system(f"rd /S /Q {shadow_build}")

	# Find MSBuild.exe and Unity version(s) on cache or on disk
	cache_data = None
	if os.path.isfile(cache_file) and os.stat(cache_file).st_size > 0:
		print("Using information from cache. Use --reload to purge cache.")
		with open(cache_file,) as f:
			cache_data = json.load(f)	

	msbuild_dir, unity_dirs = [], []
	if cache_data:
		msbuild_dir = cache_data["msbuild_dir"]
		unity_dirs = cache_data["unity_dirs"]
	if not msbuild_dir or not unity_dirs:	
		msbuild_dir = get_msbuild_dir(args) 	
		unity_dirs = get_unity_dirs(args) 
		with open(cache_file, "w+") as f:
			json.dump({"msbuild_dir": msbuild_dir, "unity_dirs": unity_dirs}, f, indent=4)
			print(f"Saved data to {cache_file}.")
	if args.latest:
		unity_dirs = [unity_dirs[-1]]

	print("MSBUILD_DIR: " + msbuild_dir)
	print("UNITY_DIR(S): " + ", ".join(unity_dirs))

	# Generate project
	cmake_shadow_build = project_name
	cmake_IDE = "Visual Studio 16"
	cmake_make_arguments = "-A"
	cmake_build_target = "x64"

	if not os.path.exists(shadow_build):
		os.mkdir(shadow_build)
	os.chdir(shadow_build)

	for unity_dir in unity_dirs:
		if "Unity.exe" in unity_dir:
			unity_dir = unity_dir[:-len("Unity.exe")]
		cmake_unity_dir = "-DSOPHIA_UNITY_INSTALL_DIRECTORY:STRING=" + os.path.abspath(os.path.join(unity_dir, "../.."))
		cmake_unity_ver = "-DSOPHIA_UNITY_VERSION:STRING=" + os.path.basename(os.path.dirname(unity_dir[:-1]))
		cmd = f"cmake {root_dir} -G \"{cmake_IDE}\" \"{cmake_make_arguments}\" \"{cmake_build_target}\" \"{cmake_unity_dir}\" \"{cmake_unity_ver}\""
		os.system(cmd)

	# Build the project
	# check if \\sophia\\ subdir is required
	# optionally filter on all keywords such as 'backup' or 'deprecated'
	csprojects = glob.glob(shadow_build + "\\src\\libraries\\sophia\\**\\*.csproj", recursive=True)
	install = glob.glob(shadow_build + "\\src\\libraries\\INSTALL.vcxproj")

	for project in list(csprojects + install):
		os.system(f"\"\"{msbuild_dir}\" \"{project}\" /p:Configuration=Debug /p:Platform=\"x64\"\"")
		os.system(f"\"\"{msbuild_dir}\" \"{project}\" /p:Configuration=Release /p:Platform=\"x64\"\"")
