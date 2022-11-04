using System;
using Nordic_Door.Shared.Models.Database;

namespace Nordic_Door.Shared.Models.API
{
    public class CreateUserRequest
    {

        public string EmployeeName { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeEmail { get; set; }

        public int EmployeeIsAdmin { get; set; }

        public string Password { get; set; }

        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public string Role { get; set; }

            // lyst til å få tak i
            // navn, epost, passord, isadmin, team, rolle
    }
}

