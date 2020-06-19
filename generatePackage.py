import os
import json
import glob
import shutil
import re
import argparse

cache_file = "build\\build_cache.json"

def getUnityVersions(cache_file):
	# load cache file or force update
	unity_versions = []
	if os.path.isfile(cache_file):
		with open(cache_file,) as f:
			cache_data = json.load(f)
		for unity_dir in cache_data["unity_dirs"]:
			unity_versions.extend(re.findall(r"\d{4}.*?(?=\\)", unity_dir))

	if not unity_versions:
		# call other script to generate this file
		print("No cached Unity version(s) found.")
		print("Enter '1' to generate and install the project from the master branch.")
		print("Enter '2' to generate and install the project.")
		print("Enter '3' to use 2019.2.10f1 as your minimum Unity version.")
		x = str(input("> ")).strip()
		if x == "1":
			os.system("build_project.bat -cmui")
			print("Cache has been generated. Please try again.")
			exit()
		elif x == "2":
			os.system("build_project.bat -ci")
			print("Cache has been generated. Please try again.")
			exit()
		elif x == "3":
			unity_versions = ["2019.2.10f1"]
		else:
			print("Invalid input. Ciao bye.")
			exit()

	# Sort Unity versions from oldest to most recent
	if len(unity_versions) > 1:	
		unity_dict = {}
		for v in unity_versions:
			m = re.match(r"(\d{4})\.(\d+)\.(\d+)(?=f)", v)
			unity_dict[m.groups()] = v
		unity_versions = list(dict(sorted(unity_dict.items(), reverse=args.latest)).values())		


	minVersion = unity_versions[0].split(".") 						# split in three parts
	versionMajor = ".".join(minVersion[0:-1])
	versionMinor = minVersion[-1].strip()
	return versionMajor, versionMinor
	

def createManifest(args):
	packageData = {}

	packageData["name"] 		= args.name
	packageData["version"] 		= args.version
	packageData["displayName"] 	= args.display_name
	packageData["description"] 	= args.description
	packageData["unity"] 		= args.unity 						# min Unity version
	if args.unity_release:
		packageData["unityRelease"] = args.unity_release 						# optional
	packageData["keywords"] 	= args.keywords
	packageData["author"] 		= { 	
									"name": args.author_name,
									"email": args.author_email,
									"url": args.author_url
								  }
						
	if not os.path.isdir(args.out_dir):
		os.mkdir(args.out_dir)

	with open(args.out_dir + "package.json", "w+", encoding="utf-16") as file:
			json.dump(packageData, file, indent=4, ensure_ascii=False)

if __name__ == '__main__':
	parser = argparse.ArgumentParser()
	parser.add_argument("-o", "--out_dir", help="Directory to publish the package in", default="C:\\DAE\\sophia_package\\")
	parser.add_argument("-l", "--latest", help="Use latest Unity version as minimum version", action="store_true")

	parser.add_argument("-n", "--name", help="Full name of the package", default="com.xrlab.sophia")
	parser.add_argument("-v", "--version", help="Version of the package", default="2.0.0")
	parser.add_argument("-d", "--display_name", help="Name of the package", default="Sophia")
	parser.add_argument("-e", "--description", help="Description of the package", default="Sophia is an early-stage library used by the research team of Digital Arts and Entertainment.")
	parser.add_argument("-u", "--unity", help="Major version of Unity, e.g. 2019.2")
	parser.add_argument("-r", "--unity_release", help="Minor version of Unity, e.g. 10f1")
	parser.add_argument("-k", "--keywords", help="Keywords", default=["sophia", "xrlab", "dae"])
	parser.add_argument("--author_name", help="Author name", default="XR LAB")
	parser.add_argument("--author_email", help="Author email", default="xrlab@howest.be")
	parser.add_argument("--author_url", help="Author url", default="http://www.digitalartsandentertainment.be/page/133/Research")
	args = parser.parse_args()

	## Get unity versions
	if not args.unity:
		args.unity, args.unity_release = getUnityVersions(cache_file)

	## Generate package manifest
	createManifest(args)
	
	## Find files
	dir_path = os.path.dirname(os.path.realpath(__file__))
	editor_path = os.path.normpath(dir_path + "/src/libraries/editor/src/")
	runtime_path = os.path.normpath(dir_path + "/src/libraries/core/src/")

	## Cleanup and copy
	dst_editor = args.out_dir + "Editor\\"
	dst_runtime = args.out_dir + "Runtime\\"

	if os.path.exists(dst_editor):
		shutil.rmtree(dst_editor)
	if os.path.exists(dst_runtime):
		shutil.rmtree(dst_runtime)
	
	shutil.copytree(editor_path, dst_editor)
	shutil.copytree(runtime_path, dst_runtime)

	print("Succesfully generated package at " + args.out_dir)
	os.startfile(args.out_dir)


