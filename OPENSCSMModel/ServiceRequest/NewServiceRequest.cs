
namespace OPENSCSMModel.ServiceRequest
{
    public class NewServiceRequest
    {
        public string Id { get; } = "SR{0}";
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Urgency { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string SupportGroup { get; set; } = string.Empty;
        public string AffectedUser { get; set; } = string.Empty;
        public string Status { get; set; } = "New";  // esto es cierto?
    }
}
