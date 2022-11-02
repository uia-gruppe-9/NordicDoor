using System;
using Nordic_Door.Shared.Models.Database;
namespace Nordic_Door.Shared.Models.Common
{
    public class UserTeamRelation
    {
        public string EmployeeName { get; set; }

        public string UserRole { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeEmail { get; set; }

        public int EmployeeIsAdmin { get; set; }
        
        public Team Team { get; set; }


    }
}

