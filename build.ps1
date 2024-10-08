Set-Location -Path "./SourceCode"
dotnet build -c Release

$modName = "CameraZoom"
$dllName = $modName + ".dll"
$pdbName = $modName + ".pdb"

$sourcePath      = Get-ChildItem -Path "bin/Release" -Recurse -Filter $dllName | Select-Object -ExpandProperty FullName
$destinationPath = Join-Path -Path ("../" + $modName + "/") -ChildPath $dllName
Copy-Item -Path $sourcePath -Destination $destinationPath -Force

$sourcePath      = Get-ChildItem -Path "bin/Release" -Recurse -Filter $pdbName | Select-Object -ExpandProperty FullName
$destinationPath = Join-Path -Path ("../" + $modName + "/") -ChildPath $pdbName
Copy-Item -Path $sourcePath -Destination $destinationPath -Force

Set-Location -Path ".."
