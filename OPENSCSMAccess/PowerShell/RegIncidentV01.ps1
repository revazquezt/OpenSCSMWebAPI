


Import-Module SMLets ;

$irTitle = $title;
$irDescription = $description;
$irUrgency = $urgency;
$irImpact = $impact;
$irSource = $source;
$irClassification = $classification;
$irStatus = $status;
$irTierQueue = $tierQueue;
$irAffectedUser = $affectedUser;
$smdefaultserver = $scsmEnviroment 

# --------------------
$IRclass = Get-SCSMclass -name System.Workitem.Incident$ ; 
$UserClass = Get-SCSMClass -name System.Domain.User$ ; 
$relAffectedUser = Get-SCSMRelationshipClass -Name System.WorkItemAffectedUser ; 

$irAffectedUserObj = Get-SCSMObject -Class $UserClass -Filter "UserName -eq $irAffectedUser";

$properties = @{
    Id             = "IR{0}"
    Title          = $irTitle
    Description    = $irDescription
    Urgency        = $irUrgency
    Impact         = $irImpact
    Source         = $irSource
    Status         = $irStatus
    Classification = $irClassification
    TierQueue      = $irTierQueue
};

$newIR = New-SCSMObject -Class $IRclass -PropertyHashtable $properties -PassThru;

if ($irAffectedUserObj -and $newIR) {
    New-SCSMRelationshipObject -RelationShip $relAffectedUser -Source $newIR -Target $irAffectedUserObj -Bulk;
}
 
$result = @{IRId = "$newIR"}
$result | ConvertTo-Json -Depth 5