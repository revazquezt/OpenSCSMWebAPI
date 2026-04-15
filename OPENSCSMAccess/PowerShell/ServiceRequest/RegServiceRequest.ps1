


Import-Module SMLets ;


$srTitle = $title;
$srDescription = $description;
$srUrgency = $urgency;
$srPriority = $priority;
$srArea = $area;
$srSupportGroup = $supportGroup;
$srAffectedUser = $affectedUser;
$srStatus = $status;
$smdefaultserver = $scsmEnviroment 


$SRclass = Get-SCSMclass -name "System.WorkItem.ServiceRequest$" ; 
$UserClass = Get-SCSMClass -name System.Domain.User$ ; 
$relAffectedUser = Get-SCSMRelationshipClass -Name System.WorkItemAffectedUser ; 

$srAffectedUserObj = Get-SCSMObject -Class $UserClass -Filter "UserName -eq $srAffectedUser";

$properties = @{
    Id             = "SR{0}"
    Title          = $srTitle
    Description    = $srDescription
    Urgency        = $srUrgency
    Priority       = $srPriority
    Area           = $srArea
    SupportGroup   = $srSupportGroup
    Status         = $srStatus
};

$newServiceRequest = New-SCSMObject -Class $SRclass -PropertyHashtable $properties -PassThru;

if ($srAffectedUserObj -and $newServiceRequest) {
    New-SCSMRelationshipObject -RelationShip $relAffectedUser -Source $newServiceRequest -Target $srAffectedUserObj -Bulk;
}
 

$result = @{SRId = "$newServiceRequest"}
$result | ConvertTo-Json -Depth 5