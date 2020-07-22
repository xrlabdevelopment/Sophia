$vswhere = "${Env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"

$mspath = & $vswhere -latest -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe | select-object -first 1

& "dotnet" restore
if ($mspath) {
  & $mspath './Sophia.Editor/Sophia.Editor.csproj' '/p:Configuration=Release' '/p:Platform=x64'
} else {
  Write-Debug -join ("mspath not valid:", " ", $mspath)
}
Read-Host -Prompt "Press Enter to exit"