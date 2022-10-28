 using System.ComponentModel.DataAnnotations;

namespace unitofwork_core.Entities
{
    public class Admin : Actor
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;

    }
}
