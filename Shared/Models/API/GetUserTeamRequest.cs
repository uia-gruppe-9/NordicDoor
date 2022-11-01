using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nordic_Door.Shared.Models.Database;

namespace Nordic_Door.Shared.Models.API
{
    public class GetUserTeamRequest
    {
        public Employee Employee { get; set; }
        public Team Team { get; set; }
        public EmployeeRole Role { get; set; }
    }
}

