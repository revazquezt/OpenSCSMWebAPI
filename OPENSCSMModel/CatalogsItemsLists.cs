
namespace OPENSCSMModel
{
    // Structures of Incident Request Catalogs
    public record TroubleTicketUrgency(string DisplayName);
    public record TroubleTicketImpact(string DisplayName);
    public record IncidentSource(string DisplayName);
    public record IncidentStatus(string DisplayName);
    public record IncidentClassification(string DisplayName); 
    public record IncidentTier(string DisplayName);

    // Structures of Service Request Catalogs
    public record ServiceRequestAreaEnum(string DisplayName);
    public record ServiceRequestAreaEnum_Content(string DisplayName);
    public record ServiceRequestAreaEnum_Directory(string DisplayName);
    public record ServiceRequestAreaEnum_Facilities(string DisplayName);
    public record ServiceRequestAreaEnum_File(string DisplayName);
    public record ServiceRequestAreaEnum_Hardware(string DisplayName);
    public record ServiceRequestAreaEnum_Infrastructure(string DisplayName);
    public record ServiceRequestAreaEnum_Messaging(string DisplayName);
    public record ServiceRequestAreaEnum_Operations(string DisplayName);
    public record ServiceRequestAreaEnum_Other(string DisplayName);
    public record ServiceRequestAreaEnum_Security(string DisplayName);
    public record ServiceRequestAreaEnum_Software(string DisplayName);
    public record ServiceRequestImplementationResultsEnum(string DisplayName);
    public record ServiceRequestPriorityEnum(string DisplayName);
    public record ServiceRequestSourceEnum(string DisplayName);
    public record ServiceRequestStatusEnum(string DisplayName);
    public record ServiceRequestSupportGroupEnum(string DisplayName);
    public record ServiceRequestUrgencyEnum(string DisplayName);
}
