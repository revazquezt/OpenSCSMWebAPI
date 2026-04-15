

function Get-UserAsignedValue {
    

    param (
        [Parameter(Mandatory)]
        $Incidentp,

        [Parameter(Mandatory)]
        [string]$irScsmEnviromentp
        
    )

    $Incidentf  = $Incidentp;
    $irScsmEnviromentf = $irScsmEnviromentp;

    $assignedUserRelClassf = Get-SCSMRelationshipClass -Name "System.WorkItemAssignedToUser$" -computername $irScsmEnviromentf
    

    return Get-SCSMObject -id (Get-SCSMRelationshipObject -BySource $incidentf | Where-Object {$_.RelationshipId -eq $assignedUserRelClassf.Id}).TargetObject.Id | ForEach-Object DisplayName
    
}

function Get-UserAffectedValue {
    

    param (
        [Parameter(Mandatory)]
        $Incidentp,

        [Parameter(Mandatory)]
        [string]$irScsmEnviromentp
        
    )

    $Incidentf  = $Incidentp
    $irScsmEnviromentf = $irScsmEnviromentp

    $affectedUserRelClassf = Get-SCSMRelationshipClass -Name "System.WorkItemAffectedUser$" -computername $irScsmEnviromentf
    

    return Get-SCSMObject -id (Get-SCSMRelationshipObject -BySource $incidentf | Where-Object {$_.RelationshipId -eq $affectedUserRelClassf.Id}).TargetObject.Id | ForEach-Object DisplayName
    
}