namespace unitofwork_core.Model.Order
{
    public class ChangeOrderStatusModel
    {
        public Guid OrderId { get; set; }
        public Guid Shipper { get; set; }
        public string Status { get; set; } = string.Empty;
        public string WalletType { get; set; } = string.Empty;
    }
}
