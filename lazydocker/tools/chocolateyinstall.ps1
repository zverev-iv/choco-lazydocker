$ErrorActionPreference = 'Stop'; # stop on all errors
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"

$packageArgs = @{
  packageName   = $env:ChocolateyPackageName
  unzipLocation = $toolsDir
  url           = 'https://github.com/jesseduffield/lazydocker/releases/download/v0.9/lazydocker_0.9_Windows_x86.zip'
  url64bit      = 'https://github.com/jesseduffield/lazydocker/releases/download/v0.9/lazydocker_0.9_Windows_x86_64.zip'
  softwareName  = 'lazydocker*'
  checksum      = '7a1071b8e90234401bc39b75cb1a869664d65cd0b25f3a4b1e62266a712cbf4d'
  checksumType  = 'sha256'
  checksum64    = 'b4a4b69d4b1d4e8edd7ab2e995f9a574951c82606670a68c7044e8cb3244e603'
  checksumType64= 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
