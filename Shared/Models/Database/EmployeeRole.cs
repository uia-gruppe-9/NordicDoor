using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nordic_Door.Shared.Models.Database
{
    public class EmployeeRole
    {
        [Key]
        [Required]
        [Column("EmpRole")]
        public string Role { get; set; }
    }
}

