namespace unitofwork_core.Entities
{
    public class HistoryOrder : BaseEntity
    {
        public string FromStatus { get; set; } = string.Empty;
        public string ToStatus { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        #region Relationship
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
        #endregion
    }
}
