$libraryFiles = @("Sophia.Core.dll", "Sophia.Platform.dll")

$editorFiles = Get-ChildItem -Path "./Sophia.Editor/*.cs" -Recurse -Exclude "*AssemblyInfo.cs"
$editorFiles = $editorFiles.FullName.Replace("\", "/")

$myPath = (Get-Item -Path ".\").FullName;
$myPath = $myPath.Replace("\", "/")

$libraryBasePath = -join ($myPath, "/lib/")
$editorBasePath = -join ($myPath, "/Sophia.Editor/")

$builderPath = -join ($myPath, "/Libraries/UnityPackager/UnityPackager.exe")
$packageOutPath = -join ($myPath, "/Sophia.unitypackage")

# Args for library generation
$libraryBuildArgs = -join ("null", " ", $packageOutPath, " ")

# Add library files to library package
For ($i=0; $i -lt $libraryFiles.Count; $i++)  
{
    $libraryBuildArgs += -join ($libraryBasePath, $libraryFiles.Get($i), " ", "Assets/Plugins/Sophia/", $libraryFiles.Get($i), " ")
}

# Add editor files to library package under a different path
For ($i=0; $i -lt $editorFiles.Count; $i++)  
{
    $libraryBuildArgs += -join ($editorFiles.Get($i), " ", "Assets/Editor/Sophia/", $editorFiles.Get($i).Replace($editorBasePath, ""), " ") 
}

Write-Host $builderPath
Write-Host $libraryBuildArgs

Start-Process -FilePath $builderPath -ArgumentList $libraryBuildArgs