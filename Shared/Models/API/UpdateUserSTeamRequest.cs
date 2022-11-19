using System;
namespace Nordic_Door.Shared.Models.API
{
	public class UpdateUserSTeamRequest
	{
		public int employeeId { get; set; }
		public List<string> teamNames { get; set; }

	}
}

