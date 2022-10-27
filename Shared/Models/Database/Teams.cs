
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordic_Door.Shared.Models.Database
{
    public class Team
    {
        [Key]
        [Column("Team_ID")]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }



    }
}
