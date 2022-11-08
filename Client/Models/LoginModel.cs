using System.ComponentModel.DataAnnotations;

namespace Nordic_Door.Client.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
