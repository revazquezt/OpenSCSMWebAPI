

Import-Module SMLets

$irId=$incidentID 
$resolvedByUserName = $ResolvedUserPrincipalName 

$resolutionDescription = $IncidentResolutionDescription 
$resolutionCategory = $IncidentresolutionCategory 

$irClass = Get-SCSMClass -Name System.workitem.Incident$
$userClass = Get-SCSMclass -Name System.Domain.User$

$resolvedByUserRel = Get-SCSMRelationshipClass -name System.WorkItem.TroubleTicketResolvedByUser$

$irObj = Get-SCSMObject -Class $irClass -Filter "ID -eq $irId"

$resolvedByUserObj = Get-SCSMObject -Class $userClass -Filter "UserName -eq $resolvedByUserName"

$resolveDetails=@{
"Status" = "Resolved";
"ResolutionDescription" = "$resolutionDescription";
"ResolutionCategory" = "$resolutionCategory";
"ResolvedDate" = (Get-Date).ToUniversalTime();
"TargetResolutionTime" = (Get-Date).ToUniversalTime();
}

Set-SCSMObject -SMObject $irObj -PropertyHashtable $resolveDetails


New-SCSMRelationshipObject -Relationship $resolvedByUserRel -Source $irObj -Target $resolvedByUserObj -Bulk
$result = @{result = "OK"}
$result | ConvertTo-Json -Depth 5