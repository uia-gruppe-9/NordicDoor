using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordic_Door.Shared.Models.Database
{
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Employee_ID")]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Teams")]
        [Column("Team_ID")]
        public int TeamId { get; set; }

        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Role Role { get; set; }
    }
}
