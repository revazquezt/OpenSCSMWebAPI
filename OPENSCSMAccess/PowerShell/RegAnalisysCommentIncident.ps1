

Import-module SMlets 

$irId = $incidentID
$commentText = $incidentCommentText
$commentAddedBy = $incidentCommentAddedBy 
if($incidentIsPrivateComment -eq "true"){
    $isPrivateComment = $true 
}else{
    $isPrivateComment = $false
}

   
$smdefaultserver = $scsmEnviroment
$incidentClass = Get-SCSMClass -Name System.workitem.Incident$ 

$irObj = Get-SCSMObject -Class $incidentClass -filter "ID -eq $irId" 

$newGUID = ([guid]::NewGuid()).ToString() 
$projection = @{__CLASS = "System.WorkItem.TroubleTicket";
    __SEED = $irObj;
    AnalystComments = @{__CLASS = "System.WorkItem.TroubleTicket.AnalystCommentLog";
        __OBJECT = @{id = $newGUID;
            DisplayName = $newGUID;
            Comment = $commentText;
            EnteredBy = $commentAddedBy;
            EnteredDate = (Get-Date).ToUniversalTime();
            IsPrivate = $isPrivateComment 
        }
    }
}
New-SCSMObjectProjection -Type "System.WorkItem.IncidentPortalProjection" -Projection $projection

$result = @{result = "OK"}
$result | ConvertTo-Json -Depth 5