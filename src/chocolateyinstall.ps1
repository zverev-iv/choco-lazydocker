$ErrorActionPreference = 'Stop';

$packageArgs = @{
	softwareName   = 'lazydocker'
	packageName    = $env:ChocolateyPackageName
	unzipLocation  = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
	url            = 'https://github.com/jesseduffield/lazydocker/releases/download/v0.12/lazydocker_0.12_Windows_x86.zip'
	checksum       = '747762627AF23B7C535CDFD353073244B977788CFDB22F83F50840E356F08673'
	checksumType   = 'sha256'
	url64bit       = 'https://github.com/jesseduffield/lazydocker/releases/download/v0.12/lazydocker_0.12_Windows_x86_64.zip'
	checksum64     = '4C09FDB74E96B2DFEBAB8775D0AE436B2713AD9F2CBF6C901B63AD4C1FFEF83B'
	checksumType64 = 'sha256'
}

Install-ChocolateyZipPackage @packageArgs
