


Import-Module SMLets -Force

$IRclass=Get-SCSMclass -name System.Workitem.Incident$ # Get SCSM Incident class object
$IRobject=Get-SCSMobject -class $IRclass -filter "ID -eq $IRid" # Get IR object
$WorkItemId=$IRobject.__InternalId
$Server=$scsmEnviroment


$FileAttachmentManagedTypeId       = "68A35B6D-CA3D-8D90-F93D-248CEFF935C0"
$WorkItemHasFileAttachmentRelTypeId = "AA8C26DC-3A12-5F88-D9C7-753E5A8A55B4"
$DiscoverySourceId                 = "7431E155-3D9E-4724-895E-C03BA951A352"


function Exec-SQL-Text {
    param([string]$Query, [string]$StepName)
    try {
        Invoke-Sqlcmd -ServerInstance $Server -Database $Database -Query $Query -ErrorAction Stop
        Write-Host "[OK] $StepName"
    } catch {
        Write-Error "[ERROR] $StepName : $_"
        exit 1
    }
}



if (-not (Test-Path $FilePath)) {
    Write-Error "❌ Archivo no encontrado: $FilePath"
    exit 1
}





$fileInfo      = Get-Item $FilePath
$fileName      = $fileInfo.Name
$fileExtension = $fileInfo.Extension.TrimStart(".")
$fileBytes     = [System.IO.File]::ReadAllBytes($FilePath)
$fileSize      = $fileBytes.Length
$now           = (Get-Date).ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.fff")

$fileAttachmentId = [System.Guid]::NewGuid().ToString().ToUpper()
$relationshipId   = [System.Guid]::NewGuid().ToString().ToUpper()
$blobId           = [System.Guid]::NewGuid().ToString().ToUpper()

Write-Host ""
Write-Host "================================================================"
Write-Host " SCSM File Attachment Inserter"
Write-Host "================================================================"
Write-Host " Archivo     : $fileName"
Write-Host " Tamano      : $fileSize bytes"
Write-Host " WorkItem ID : $WorkItemId"
Write-Host " Attachment  : $fileAttachmentId"
Write-Host " BlobId      : $blobId"
Write-Host " Relationship: $relationshipId"
Write-Host "================================================================"
Write-Host ""

try {
    $conn = New-Object System.Data.SqlClient.SqlConnection
    $conn.ConnectionString = "Server=$Server;Database=$Database;Integrated Security=True;"
    $conn.Open()

    $cmd = $conn.CreateCommand()
    $cmd.CommandText = @"
INSERT INTO dbo.BlobStorage (BlobId, FileName, ManagedTypeId, Value)
VALUES (@BlobId, @FileName, @ManagedTypeId, @Value)
"@
    $cmd.Parameters.Add("@BlobId",        [System.Data.SqlDbType]::UniqueIdentifier).Value = [System.Guid]::Parse($blobId)
    $cmd.Parameters.Add("@FileName",      [System.Data.SqlDbType]::NVarChar, 256).Value    = $fileName
    $cmd.Parameters.Add("@ManagedTypeId", [System.Data.SqlDbType]::UniqueIdentifier).Value = [System.Guid]::Parse($FileAttachmentManagedTypeId)
    $cmd.Parameters.Add("@Value",         [System.Data.SqlDbType]::VarBinary, -1).Value    = $fileBytes

    $cmd.ExecuteNonQuery() | Out-Null
    $conn.Close()
    Write-Host "[OK] PASO 1: BlobStorage (blob binario)"
} catch {
    Write-Error "[ERROR] PASO 1: BlobStorage : $_"
    exit 1
}

$q2 = @"
INSERT INTO dbo.BaseManagedEntity (
    BaseManagedEntityId,
    BaseManagedTypeId,
    FullName,
    Path,
    Name,
    DisplayName,
    TopLevelHostEntityId,
    IsDeleted,
    LastModified,
    OverrideTimestamp,
    TimeAdded,
    LastModifiedBy
)
VALUES (
    '$fileAttachmentId',
    '$FileAttachmentManagedTypeId',
    'System.FileAttachment:$($fileAttachmentId.ToLower())',
    NULL,
    '$fileName',
    '$fileName',
    NULL,
    0,
    '$now',
    '$now',
    '$now',
    '$DiscoverySourceId'
)
"@
Exec-SQL-Text -Query $q2 -StepName "PASO 2: BaseManagedEntity"

$q3 = @"
INSERT INTO dbo.[MT_System`$FileAttachment] (
    BaseManagedEntityId,
    DisplayName,
    [Id_1C7A525A_E7EC_59B9_6FF3_CFCECA1A64A0],
    [Extension_E16D5F19_3266_59AA_42AB_261442BDEB5C],
    [Content_D925815A_4E9C_D3E6_0C31_EDCC820DFF7C],
    [Size_CC8C4AE2_E621_A1F1_06D7_09E3129624F9],
    [AddedDate_E5CFF8F9_E80E_53E1_8FD5_19765A889EB4]
)
VALUES (
    '$fileAttachmentId',
    '$fileName',
    '$fileAttachmentId',
    '$fileExtension',
    '$blobId',
    $fileSize,
    '$now'
)
"@
Exec-SQL-Text -Query $q3 -StepName "PASO 3: MT_System`$FileAttachment (metadata)"

$q4 = @"
INSERT INTO dbo.Relationship (
    RelationshipId,
    RelationshipTypeId,
    SourceEntityId,
    TargetEntityId,
    IsDeleted,
    TimeAdded,
    LastModified
)
VALUES (
    '$relationshipId',
    '$WorkItemHasFileAttachmentRelTypeId',
    '$WorkItemId',
    '$fileAttachmentId',
    0,
    '$now',
    '$now'
)
"@
Exec-SQL-Text -Query $q4 -StepName "PASO 4: Relationship (WorkItem -> FileAttachment)"

$q5 = @"
INSERT INTO dbo.TypedManagedEntity (
    TypedManagedEntityId,
    BaseManagedEntityId,
    ManagedTypeId,
    IsDeleted,
    LastModified
)
VALUES (
    '$fileAttachmentId',
    '$fileAttachmentId',
    '$FileAttachmentManagedTypeId',
    0,
    '$now'
)
"@
Exec-SQL-Text -Query $q5 -StepName "PASO 5: TypedManagedEntity (visibilidad DAS)"


$verify = @"
SELECT
    bme.DisplayName  AS Archivo,
    bme.IsDeleted    AS BME_Eliminado,
    tme.IsDeleted    AS TME_Eliminado,
    r.IsDeleted      AS Relacion_Eliminada,
    bs.BlobId        AS BlobId
FROM dbo.BaseManagedEntity bme
JOIN dbo.TypedManagedEntity tme
    ON tme.BaseManagedEntityId = bme.BaseManagedEntityId
JOIN dbo.Relationship r
    ON r.TargetEntityId = bme.BaseManagedEntityId
JOIN dbo.[MT_System`$FileAttachment] fa
    ON fa.BaseManagedEntityId = bme.BaseManagedEntityId
JOIN dbo.BlobStorage bs
    ON bs.BlobId = fa.[Content_D925815A_4E9C_D3E6_0C31_EDCC820DFF7C]
WHERE bme.BaseManagedEntityId = '$fileAttachmentId'
"@

$result = Invoke-Sqlcmd -ServerInstance $Server -Database $Database -Query $verify


