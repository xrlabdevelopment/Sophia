import os
import json
import glob
import shutil

out_dir = "C:\\DAE\\sophia_package\\"
cache_file = "installed_unity_versions.txt"


def getUnityVersions(cache_file):
	# load cache file or force update
	if os.path.isfile(cache_file):
		with open(cache_file, "r") as file:
			versions = file.readlines()
	else:
		# call other script to generate this file
		print("No cached Unity version found.")
		print("Enter '1' to generate and install the project to update the cache. (This might take some time)")
		print("Enter '2' to use 2019.2.10f1 as your minimum Unity version.")
		x = str(input("> ")).strip()
		if x == "1":
			os.system("./cmake/build/_cached_generateVC2019_x64.bat")
			os.system("installProject.bat")
			os.system("python generatePackage.py")
			exit()
		elif x == "2":
			versions = ["2019.2.10f1"]
		else:
			print("Invalid input. Ciao bye.")
			exit()

	minVersion = versions[0].split(".") 							# split in three parts
	versionMajor = ".".join(minVersion[0:-1])
	versionMinor = minVersion[-1].strip()

	return versionMajor, versionMinor


def createManifest(outdir, versionMajor, versionMinor):
	packageData = {}

	packageData["name"] 		= "com.xrlab.sophia"
	packageData["version"] 		= "2.0.0"
	packageData["displayName"] 	= "Sophia"
	packageData["description"] 	= "Ἁγία Σοφία"
	packageData["unity"] 		= versionMajor 						# min Unity version
	packageData["unityRelease"] = versionMinor 						# optional
	packageData["keywords"] 	= [ "sophia", "xrlab", "dae"]
	packageData["author"] 		= { 	
									"name": "XR LAB",
									"email": "dendave@xrlab.be",
									"url": "https://www.digitalartsandentertainment.be"
								  }
						
	if not os.path.isdir(outdir):
		os.mkdir(outdir)

	with open(outdir + "package.json", "w+", encoding="utf-16") as file:
			json.dump(packageData, file, indent=4, ensure_ascii=False)

if __name__ == '__main__':

	## Get unity versions
	versionMajor, versionMinor = getUnityVersions(cache_file)

	## Generate package manifest
	createManifest(out_dir, versionMajor, versionMinor)
	
	## Find files
	dir_path = os.path.dirname(os.path.realpath(__file__))
	editor_path = os.path.normpath(dir_path + "/src/libraries/editor/src/")
	runtime_path = os.path.normpath(dir_path + "/src/libraries/core/src/")

	## Cleanup and copy
	dst_editor = out_dir + "Editor\\"
	dst_runtime = out_dir + "Runtime\\"

	if os.path.exists(dst_editor):
		shutil.rmtree(dst_editor)
	if os.path.exists(dst_runtime):
		shutil.rmtree(dst_runtime)
	
	shutil.copytree(editor_path, dst_editor)
	shutil.copytree(runtime_path, dst_runtime)

	print("Succesfully generated package at " + out_dir)
	os.startfile(out_dir)


