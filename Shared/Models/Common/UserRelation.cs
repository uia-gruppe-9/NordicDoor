using System;
namespace Nordic_Door.Shared.Models.Common
{
    public class UserRelation
    {
        public string EmployeeName { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeEmail { get; set; }

        public int EmployeeIsAdmin { get; set; }

        public List<UserTeamRelation> userTeamRelations { get; set; }
    }
}

