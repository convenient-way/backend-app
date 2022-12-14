namespace unitofwork_core.Model.TransactionModel
{
    public class ResponseTransactionModel
    {
        public string Status { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TransactionType { get; set; } = string.Empty;
        public decimal CoinExchange { get; set; }
        public decimal BalanceWallet { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid WalletId { get; set; }
        public Guid? PackageId { get; set; } = null;
    }
}
