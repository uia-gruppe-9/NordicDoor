using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordic_Door.Shared.Models.API
{
	public class UpdateTeamLeaderRequest
	{

            public int EmployeeId { get; set; }
            public List<string> TeamNames { get; set; }

    
    }
}
