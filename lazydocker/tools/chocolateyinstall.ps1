$ErrorActionPreference = 'Stop'; # stop on all errors
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"

$packageArgs = @{
  packageName   = $env:ChocolateyPackageName
  unzipLocation = $toolsDir
  url           = 'https://github.com/jesseduffield/lazydocker/releases/download/v0.7.6/lazydocker_0.7.6_Windows_x86.zip'
  url64bit      = 'https://github.com/jesseduffield/lazydocker/releases/download/v0.7.6/lazydocker_0.7.6_Windows_x86_64.zip'
  softwareName  = 'lazydocker*' #part or all of the Display Name as you see it in Programs and Features. It should be enough to be unique
  checksum      = '45B475D4B1C9EF7673022283AE0853ACAA1EF48EA44AFDA68656A5F83D9B2F7A'
  checksumType  = 'sha256'
  checksum64    = '9C59C501D307D9BD46CD5C03D6278A79873F08591768ABC0C16478C828878C54'
  checksumType64= 'sha256'
}

Install-ChocolateyZipPackage @packageArgs