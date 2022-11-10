using System.ComponentModel.DataAnnotations;
using Nordic_Door.Shared.Models.Database;
namespace Nordic_Door.Client.Models
{
	public class CreateUserModel
	{
        [Required]
        [StringLength(60, ErrorMessage = "Navn kan ikke være mer enn 60 tegn.")]
        public string Name { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "Epost kan ikke være mer enn 60 tegn")]
        public string Email { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "Passord kan ikke være mer enn 60 tegn")]
        public string Password { get; set; }

        [Required]
        public List<string> Teams { get; set; }

        [Required]
        public bool isAdmin { get; set; }
    }


}
