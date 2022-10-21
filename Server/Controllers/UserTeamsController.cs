using System.Net.NetworkInformation;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nordic_Door.Server.Data;
using Nordic_Door.Shared.Models.API;
using Nordic_Door.Shared.Models.Database;

namespace Nordic_Door.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserTeamController : Controller
    {

        public NordicDoorsDbContext dbContext { get; set; }

        public UserTeamController(NordicDoorsDbContext ctx)
        {
            dbContext = ctx;
        }


        [HttpGet]

        public async Task<IActionResult> GetUserTeams()
        {
            var userTeams = await dbContext.UserTeam.ToListAsync();

            var updateUserTeams = new List<GetUserTeamRequest>();

            foreach ( var userTeam in userTeams)
            {
                var team = await dbContext.Teams.FindAsync(userTeam.TeamId);
                var employee = await dbContext.Employees.FindAsync(userTeam.EmployeeId);
                var role = await dbContext.UserTeam.FindAsync(userTeam.Role);

                if (team == null || employee == null || role == null)
                {
                    continue;
                }
                updateUserTeams.Add(new GetUserTeamRequest()
                {
                    Team = team,
                    Employee = employee,
                    Role = role,
                }

                    );
            }
            return Ok(updateUserTeams);
        }


    }
}

