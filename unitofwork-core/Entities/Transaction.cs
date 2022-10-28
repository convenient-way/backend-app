namespace unitofwork_core.Entities
{
    public class Transaction : BaseEntity
    {
        public string Status { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TransactionType { get; set; } = string.Empty;
        public decimal CoinExchange { get; set; }
        public decimal BalanceWallet{ get; set; }
        public DateTime CreatedAt { get; set; }

        #region Relationship
        public Guid WalletId { get; set; }
        public Wallet? Wallet { get; set; }

        public Guid? OrderId { get; set; }
        public Order? Order { get; set; }
        #endregion
    }
}
