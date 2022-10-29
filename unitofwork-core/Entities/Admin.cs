using System.ComponentModel.DataAnnotations;
using unitofwork_core.Model.Admin;

namespace unitofwork_core.Entities
{
    public class Admin : Actor
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;


        public ResponseAdminModel ToResponseModel()
        {
            ResponseAdminModel model = new ResponseAdminModel();
            model.Id = this.Id;
            model.UserName = this.UserName;
            model.Email = this.Email;
            model.DisplayName = this.DisplayName;
            model.PhoneNumber = this.PhoneNumber;
            model.PhotoUrl = this.PhotoUrl;
            model.Status = this.Status;
            model.Address = this.Address;
            model.FirstName = this.FirstName;
            model.LastName = this.LastName;
            model.Gender = this.Gender;
            model.CreatedAt = this.CreatedAt;
            model.ModifiedAt = this.ModifiedAt;
            return model;
        }

    }
}
