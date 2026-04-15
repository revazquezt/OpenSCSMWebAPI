param (
    [string]$catalogName,
    [string]$scsmEnviroment
)

Import-Module SMLets ;

$smcatalogName = $catalogName
$smdefaultserver = $scsmEnviroment 


$RowsCatalog = Get-SCSMEnumeration -name $smcatalogName | Get-SCSMChildEnumeration | select-object displayname 

ConvertTo-Json @($RowsCatalog) 