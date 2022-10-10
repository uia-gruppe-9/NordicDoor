
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordic_Door.Shared.Models.Database
{
    public class Teams
    {
        [Key]
        [Column("Team_ID")]
        public int Id { get; set; }

        [Required]
        [Column("Leader_ID")]
        public int LeaderId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Role Role { get; set; }

    }
}
