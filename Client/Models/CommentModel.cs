using System.ComponentModel.DataAnnotations;

namespace Nordic_Door.Client.Models
{
    public class CommentModel
    {
        [Required(ErrorMessage = "Du må gi en begrunnelse")]
        [StringLength(60, ErrorMessage = "Navn kan ikke være mer enn 60 tegn.")]
        public string Reason { get; set; }
    }
}
