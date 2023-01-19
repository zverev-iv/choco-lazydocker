$ErrorActionPreference = 'Stop';

$packageArgs = @{
	packageName    = $env:ChocolateyPackageName
	softwareName  = "${softwareName}"
	unzipLocation  = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
	url           = "${url}"
	url64bit      = "${url64bit}"
	checksum      = "${checksum}"
	checksumType  = "${checksumType}"
	checksum64    = "${checksum64}"
	checksumType64= "${checksumType64}"
}

Install-ChocolateyZipPackage @packageArgs
