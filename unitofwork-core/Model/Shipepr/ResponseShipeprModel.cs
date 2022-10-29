﻿namespace unitofwork_core.Model.Shipepr
{
    public class ResponseShipeprModel
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public double HomeLongitude { get; set; }
        public double HomeLatitude { get; set; }
        public double DestinationLongitude { get; set; }
        public double DestinationLatitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
