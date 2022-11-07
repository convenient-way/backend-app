using System.ComponentModel.DataAnnotations;

namespace unitofwork_core.Model.ShipperModel
{
    public class RegisterShipperModel
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
/*        public string HomeAddress { get; set; } = string.Empty;
        public double HomeLongitude { get; set; }
        public double HomeLatitude { get; set; }
        public string DestinationAddress { get; set; } = string.Empty;
        public double DestinationLongitude { get; set; }
        public double DestinationLatitude { get; set; }*/
    }
}
