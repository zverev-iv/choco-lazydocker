$ErrorActionPreference = 'Stop'; # stop on all errors
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"

$packageArgs = @{
  packageName   = $env:ChocolateyPackageName
  unzipLocation = $toolsDir
  url           = 'https://github.com/jesseduffield/lazydocker/releases/download/v0.8/lazydocker_0.8_Windows_x86.zip'
  url64bit      = 'https://github.com/jesseduffield/lazydocker/releases/download/v0.8/lazydocker_0.8_Windows_x86_64.zipы'
  softwareName  = 'lazydocker*' #part or all of the Display Name as you see it in Programs and Features. It should be enough to be unique
  checksum      = 'c01aa910f06321a3fe5e2d0145d56683337bf7c68db86488d071e595628d4c7b'
  checksumType  = 'sha256'
  checksum64    = 'adcb89d19831d43fcc2ac2fc12d19e4db54025ce797bcdc72174e6b431665bee'
  checksumType64= 'sha256'
}

Install-ChocolateyZipPackage @packageArgs