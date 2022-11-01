namespace unitofwork_core.Model.Wallet
{
    public class ResponseWalletModel
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
        public string WalletType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ModifiedAt { get; set; }
    }
}
