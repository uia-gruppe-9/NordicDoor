using System;
using Nordic_Door.Shared.Models.Database;

namespace Nordic_Door.Shared.Models.API
{
    public class AddUserTeamRequest
    {
        public int EmployeeId { get; set; }
        public int TeamId { get; set; }
        public string Role { get; set; }
    }
}

