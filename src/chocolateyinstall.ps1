$ErrorActionPreference = 'Stop';

$packageArgs = @{
	softwareName   = 'lazydocker'
	packageName    = $env:ChocolateyPackageName
	unzipLocation  = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
	url            = 'https://github.com/jesseduffield/lazydocker/releases/download/v0.18.1/lazydocker_0.18.1_Windows_x86.zip'
	checksum       = 'c2646a6c1835443441014b1a846f75391c449079644fcc23d0c09b3c3549a427'
	checksumType   = 'sha256'
	url64bit       = 'https://github.com/jesseduffield/lazydocker/releases/download/v0.18.1/lazydocker_0.18.1_Windows_x86_64.zip'
	checksum64     = '6f99fe8e85f410ce119e6294603d8bec2dae72ec309d9f1fb55f8a54f3088bbc'
	checksumType64 = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
