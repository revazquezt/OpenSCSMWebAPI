

Import-Module SMLets ;
Import-Module $modulePath

$irUser = $User;
$irScsmEnviroment = $scsmEnviroment;




$userClass = Get-SCSMClass -Name "System.Domain.User$" -computername $irScsmEnviroment;
$affectedUserRelClass = Get-SCSMRelationshipClass -Name "System.WorkItemAffectedUser$" -computername $irScsmEnviroment;
$userscsm = Get-SCSMObject -class $userClass -filter "Username -eq $irUser" -computername $irScsmEnviroment;
$result =  (Get-SCSMRelationshipObject -ByTarget $userscsm -computername $irScsmEnviroment | Where-Object {$_.RelationshipId -eq $affectedUserRelClass.Id}).SourceObject;


ConvertTo-Json @($result | Select-Object @{Name = 'Id'; Expression = { $_.Values | Where-Object {$_.Type -in @('Id') } | Select-Object $_.Parent | Where-Object {$_.Type -in @('Id')} |  ForEach-Object Value}}, 
                        @{Name = 'Title'; Expression = { $_.Values | Where-Object {$_.Type -in @('Title') } | Select-Object $_.Parent | Where-Object {$_.Type -in @('Title')} |  ForEach-Object Value}}, 
                        @{Name = 'Status'; Expression = { $_.Values | Where-Object {$_.Type -in @('Status') } | Select-Object $_.Parent | Where-Object {$_.Type -in @('Status')}  | ForEach-Object Value |  ForEach-Object DisplayName  }},
                        @{Name = 'AffectedUser'; Expression = { Get-UserAffectedValue -Incidentp $_ -irScsmEnviromentp $irScsmEnviroment }},
                        @{Name = 'AssignedUser'; Expression = { Get-UserAsignedValue -Incidentp $_ -irScsmEnviromentp $irScsmEnviroment }},
                        @{Name = 'LastDateTimeModified'; Expression = { $_.LastModified.DateTime }}) #| ConvertTo-Json #| Where-Object { $_.Type -in @("Id") | Select-Object Type }}} | ConvertTo-Json