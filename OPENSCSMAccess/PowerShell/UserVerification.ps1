

Import-Module SMLets ;

$smPrincipalUserName = $PrincipalUserName
$smDomainUserName = $DomainUserName
$smdefaultserver = $scsmEnviroment 


$output = Get-SCSMUser -Filter "UserName -eq '$smDomainUserName.$smPrincipalUserName'" | Select FirstName, LastName, UserName, ObjectStatus


ConvertTo-Json @($output | Select-Object @{Name = 'UserName'; Expression = { $_.UserName }},
                                         @{Name = 'FirstName'; Expression = { $_.FirstName }},
                                         @{Name = 'LastName'; Expression = { $_.LastName }},
                                         @{Name = 'Status'; Expression = { $_.ObjectStatus.DisplayName  }}
) 

