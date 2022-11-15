using Nordic_Door.Shared.Models.Database;
using System.ComponentModel.DataAnnotations;

namespace Nordic_Door.Client.Models
{
    public class CreateSuggestionModel
    {
        [Required]
        [StringLength(25, ErrorMessage = "Tittel kan ikke være lenger enn 30 tegn.")]
        public string Title { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Beskrivelse må ikke overstride 250 tegn.")]
        public string Description { get; set; }

        public DateTime? Deadline { get; set; }

        public Employee ResponsibleEmployee { get; set; }
        public Team ResponsibleTeam { get; set; }

        [Required]
        public Team AssociatedTeam { get; set; } // This is the team that the user want so associate the suggestion with (if the user is a member of multiple teams)

        public string Phase { get; set; }

        public string Status { get; set; }

    }
}
