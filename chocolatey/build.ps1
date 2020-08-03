
$version = $env:ShortVersion

write-host "packing $version"

$setupVersion = $version -replace '.', '_'
$url = "https://github.com/DaxStudio/DaxStudio/releases/download/$($version)/DaxStudio_$($setupVersion)_setup.exe"

$installFile = "$($env:APPVEYOR_BUILD_FOLDER)\chocolatey\tools\chocolateyInstall.ps1"

$checksum = Get-FileHash -Path "$($env:APPVEYOR_BUILD_FOLDER)\releases\DaxStudio_$($setupVersion)_setup.exe" -Algorithm SHA256

$script = Get-Content $installFile

write-host $script

$script = $script -replace  '(?<=url\s*=\s*'')([^'']*)(?='')', $url
$script = $script -replace  '(?<=checksum\s*=\s*'')([^'']*)(?='')', $checksum

$script | Set-Content -Path $installFile

& choco pack "$($env:APPVEYOR_BUILD_FOLDER)\chocolatey\daxstudio.nuspec" --version $env:ShortVersion --out "$($env:APPVEYOR_BUILD_FOLDER)\chocolatey"