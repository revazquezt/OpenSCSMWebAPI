

Import-module SMlets 
$smdefaultserver = $scsmEnviroment 
$IRid = $incidentID 

$IRclass=Get-SCSMclass -name System.Workitem.Incident$ 
$IRobject=Get-SCSMobject -class $IRclass -filter "ID -eq $IRid" 
$IRobject | Format-List | Out-Null


$Title = $IRobject.Title
$Description = $IRobject.Description
$Status = $IRobject.Status
$Impact = $IRobject.Impact.DisplayName
$Urgency = $IRobject.Urgency
$Id = $IRobject.Id
$CreatedDate = $IRobject.CreatedDate.ToString("yyyy-MM-ddTHH:mm:ss")
$LastModifiedDate = $IRobject.LastModified.ToString("yyyy-MM-ddTHH:mm:ss")
$Classification = $IRobject.Classification.DisplayName
$SupportGroup = $IRobject.TierQueue.DisplayName

$relAffectedUser = Get-SCSMRelationshipClass -Name System.WorkItemAffectedUser
$relAssignToUser = Get-SCSMRelationshipClass -Name System.WorkItemAssignedToUser
$relAffectedConfigItems = Get-SCSMRelationshipClass -Name System.WorkItemAboutConfigItem
$affectedUserDisplayName = (Get-SCSMRelatedobject -SMObject $IRobject -Relationship $relAffectedUser).DisplayName
$affectedUserUserName = (Get-SCSMRelatedobject -SMObject $IRobject -Relationship $relAffectedUser).UserName
$assignedToUserDisplayName = (Get-SCSMRelatedobject -SMObject $IRobject -Relationship $relAssignToUser).DisplayName
$assignedToUserUserName = (Get-SCSMRelatedobject -SMObject $IRobject -Relationship $relAssignToUser).UserName
$affectedConfigItemsDisplayName = (Get-SCSMRelatedobject -SMObject $IRobject -Relationship $relAffectedConfigItems).DisplayName


$data = @{
    Title  = $Title 
Description  = $Description 
Status  = $Status 
Impact  = $Impact 
Urgency  = $Urgency 
Id  = $Id 
CreatedDate  = $CreatedDate 
LastModifiedDate  = $LastModifiedDate 
Classification  = $Classification 
SupportGroup  = $SupportGroup 


affectedUserDisplayName  = $affectedUserDisplayName 
affectedUserUserName  = $affectedUserUserName 

assignedToUserDisplayName  = $assignedToUserDisplayName 
assignedToUserUserName  = $assignedToUserUserName 

affectedConfigItemsDisplayName  = $affectedConfigItemsDisplayName 
}

return ConvertTo-Json @($data)