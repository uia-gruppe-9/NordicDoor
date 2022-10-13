using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordic_Door.Shared.Models.Database
{

    // These are composite keys which are specified in OnDbModelCreating in NordicDoorsDbContext
    public class UserTeams
    {
        [Required]
        [Column("Employee_ID")]
        [ForeignKey("Employees")]
        public int EmployeeId { get; set; }

        [Required]
        [Column("Team_ID")]
        [ForeignKey("Teams")]
        public int TeamId { get; set; }
    }
}
