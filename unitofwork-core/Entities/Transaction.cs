using unitofwork_core.Model.TransactionModel;

namespace unitofwork_core.Entities
{
    public class Transaction : BaseEntity
    {
        public string Status { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TransactionType { get; set; } = string.Empty;
        public decimal CoinExchange { get; set; }
        public decimal BalanceWallet { get; set; }
        public DateTime CreatedAt { get; set; }

        #region Relationship
        public Guid WalletId { get; set; }
        public Wallet? Wallet { get; set; }

        public Guid? PackageId { get; set; }
        public Package? Package { get; set; }
        #endregion

        public ResponseTransactionModel ToResponseModel()
        {
            ResponseTransactionModel model = new ResponseTransactionModel();
            model.Status = this.Status;
            model.Description = this.Description;
            model.TransactionType = this.TransactionType;
            model.CoinExchange = this.CoinExchange;
            model.BalanceWallet = this.BalanceWallet;
            model.PackageId = this.PackageId;
            model.CreatedAt = this.CreatedAt;
            return model;
        }
    }
}
