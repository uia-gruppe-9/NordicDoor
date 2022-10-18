using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordic_Door.Shared.Models.Database
{
    public class Suggestion
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Suggestion_ID")]
        public int Id { get; set; }

        [Required]
        [Column("Team_ID")]
        [ForeignKey("Teams")]
        public int TeamId { get; set; }

        [Column("Responsible_Employee_ID")]
        [ForeignKey("Employees")]
        public int? ResponsibleEmployee { get; set; }

        [Column("Responsible_Team_ID")]
        [ForeignKey("Teams")]
        public int? ResponsibleTeam { get; set; }

        [Required]
        [Column("CreatedBy_ID")]
        [ForeignKey("Employees")]
        public int CreatedBy { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Column("Deadline")]
        public DateTime? DeadLine { get; set; }

        public DateTime? LastUpdatedAt { get; set; }

        public string? Status { get; set; }

        public string? Phase { get; set; }

        public string? Description { get; set; }




    }
}
