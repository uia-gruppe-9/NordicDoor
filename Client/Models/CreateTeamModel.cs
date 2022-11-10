using System.ComponentModel.DataAnnotations;
using Nordic_Door.Shared.Models.Database;
namespace Nordic_Door.Client.Models
{
    public class CreateTeamModel
    {
        [Required]
        [StringLength(60, ErrorMessage = "Team-navn kan ikke være mer enn 60 tegn.")]
        public string TeamName { get; set; }
    }
}
