namespace unitofwork_core.Entities
{
    public class Wallet : BaseEntity
    {
        public decimal Balance { get; set; }
        public string WalletType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ModifiedAt { get; set; }

        #region Relationship
        public Guid? ShipperId { get; set; }
        public Shipper? Shipper { get; set; }

        public Guid? ShopId { get; set; }
        public Shop? Shop { get; set; }

        public IList<Transaction> Transactions { get; set; }
        #endregion
        public Wallet()
        {
            Transactions = new List<Transaction>();
        }
    }
}
