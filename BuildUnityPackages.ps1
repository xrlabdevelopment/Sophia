$libraryFiles = @("Sophia.Core.dll", "Sophia.Platform.dll")
$installerFiles = @("SophiaEditor.cs")

$editorFiles = Get-ChildItem -Path "./Sophia.Editor/*.cs" -Recurse -Exclude ("*AssemblyInfo.cs", "SophiaEditor.cs")
$editorFiles = $editorFiles.FullName.Replace("\", "/")

$myPath = (Get-Item -Path ".\").FullName;
$myPath = $myPath.Replace("\", "/")

$libraryBasePath = -join ($myPath, "/lib/")
$editorBasePath = -join ($myPath, "/Sophia.Editor/")

$builderPath = -join ($myPath, "/Libraries/UnityPackager/UnityPackager.exe")
$packageOutPath = -join ($myPath, "/Sophia.unitypackage")
$installerOutPath = -join ($myPath, "/Sophia-Installer.unitypackage")

# Args for library and installer generation
$libraryBuildArgs = -join ("null", " ", $packageOutPath, " ")
$installerBuildArgs = -join ("null", " ", $installerOutPath, " ")

# Add library files to library package
For ($i=0; $i -lt $libraryFiles.Count; $i++)  
{
    $libraryBuildArgs += -join ($libraryBasePath, $libraryFiles.Get($i), " ", "Assets/Sophia/Lib/", $libraryFiles.Get($i), " ")
}

# Add editor files to library package under a different path
For ($i=0; $i -lt $editorFiles.Count; $i++)  
{
    $libraryBuildArgs += -join ($editorFiles.Get($i), " ", "Assets/Editor/Sophia/", $editorFiles.Get($i).Replace($editorBasePath, ""), " ") 
}

# Add installer file(s) to the installer package
For ($i=0; $i -lt $installerFiles.Count; $i++)  
{
    $installerBuildArgs += -join ($editorBasePath, $installerFiles.Get($i), " ", "Assets/Editor/Sophia/", $installerFiles.Get($i).Replace($editorBasePath, ""), " ") 
}

# Write-Host $builderPath
# Write-Host $libraryBuildArgs

Start-Process -FilePath $builderPath -ArgumentList $libraryBuildArgs
Start-Process -FilePath $builderPath -ArgumentList $installerBuildArgs