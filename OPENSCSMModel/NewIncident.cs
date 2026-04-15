
namespace OPENSCSMModel
{
    public class NewIncident
    {
        public string Id { get; } = "IR{0}";
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Urgency { get; set; } = string.Empty;
        public string Impact { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Classification { get; set; } = string.Empty;
        public string TierQueue { get; set; } = string.Empty;
        public string AffectedUser { get; set; } = string.Empty;
    }
}
