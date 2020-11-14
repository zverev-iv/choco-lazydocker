$ErrorActionPreference = 'Stop'; # stop on all errors
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"

$packageArgs = @{
  packageName   = $env:ChocolateyPackageName
  unzipLocation = $toolsDir
  url           = 'https://github.com/jesseduffield/lazydocker/releases/download/v0.10/lazydocker_0.10_Windows_x86.zip'
  url64bit      = 'https://github.com/jesseduffield/lazydocker/releases/download/v0.10/lazydocker_0.10_Windows_x86_64.zip'
  softwareName  = 'lazydocker*'
  checksum      = '89af08532d8a486ff36ba712ab1464b06454cf67052ae5ff9c6cc066dd382fd2'
  checksumType  = 'sha256'
  checksum64    = '743172a04ab046c6a772e6c5d309da1cc4a89926c0846a71bdd10d05dbe5c79c'
  checksumType64= 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
