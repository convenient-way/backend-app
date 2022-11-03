namespace unitofwork_core.Model.HistoryPackageModel
{
    public class ResponseHistoryPackageModel
    {
        public string FromStatus { get; set; } = string.Empty;
        public string ToStatus { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public Guid PackageId { get; set; }
    }
}
