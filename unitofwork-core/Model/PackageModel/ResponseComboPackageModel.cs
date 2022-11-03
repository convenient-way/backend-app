namespace unitofwork_core.Model.PackageModel
{
    public class ResponseComboPackageModel
    {
        public Guid ShopId { get; set; }
        public double Time { get; set; }
        public double Distance { get; set; }
        public decimal ComboPrice { get; set; }
        public List<ResponsePackageModel> packages = new List<ResponsePackageModel>();
    }
}
