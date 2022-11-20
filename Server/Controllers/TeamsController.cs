using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nordic_Door.Server.Data;
using Nordic_Door.Shared.Models.API;
using Nordic_Door.Shared.Models.Common;
using Nordic_Door.Shared.Models.Database;

namespace Nordic_Door.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : Controller
    {

        public NordicDoorsDbContext dbContext { get; set; }
        public TeamsController(NordicDoorsDbContext ctx)
        {
            dbContext = ctx;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeams()
        {
            var team = await dbContext.Teams.ToListAsync();
            return Ok(team);
        }

        [HttpGet]
        [Route("/Search/TeamById/{id:int}")]
        public async Task<IActionResult> GetTeamById([FromRoute] int id)
        {
            var team = await dbContext.Teams.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        [HttpGet]
        [Route("/Search/First/Team/{name}")]
        public async Task<IActionResult> GetFirstTeamByName([FromRoute] string name)
        {

            var team = await dbContext.Teams.FirstAsync(team => team.Name == name);

            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }


        [HttpGet]
        [Route("/Search/All/Team/teamname{name}")]
        public async Task<IActionResult> GetAllTeamsByName([FromRoute] string name)
        {

            var team = await dbContext.Teams.Where(Team => Team.Name == name).ToListAsync();

            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        [HttpPut]
        [Route("/Update/Team/{id:int}")]
        public async Task<IActionResult> UpdateTeamName([FromRoute] int id, UpdateTeamRequest updateTeamRequest)
        {
            var team = await dbContext.Teams.FindAsync(id);

            if (team != null)
            {
                team.Name = updateTeamRequest.Name;

                await dbContext.SaveChangesAsync();

                return Ok(team);
            }
            return NotFound();
        }

        [HttpPut]
        [Route("/UpdateTeamLeder")]
        public async Task<IActionResult> UpdateTeamLeaderInTeam(UpdateTeamLeaderRequest updateTeamLeaderRequest)
        {
            if (updateTeamLeaderRequest.employeeName != null && updateTeamLeaderRequest.teamName != null)
            {

                var OLteam = await dbContext.Teams.FirstAsync(t => t.Name == updateTeamLeaderRequest.teamName);
                var userTeamOldTL = await dbContext.UserTeams.Where(tL => tL.TeamId == OLteam.Id).ToListAsync();
                var oldTeamLeader = await dbContext.UserTeams.FirstAsync(l => l.Role == "Teamleder");
                if (oldTeamLeader != null)
                {
                    oldTeamLeader.Role = "Medarbeider";
                }

                var employee = await dbContext.Employees.FirstAsync(e => e.Name == updateTeamLeaderRequest.employeeName);
                var employeeInUserTeam = await dbContext.UserTeams.FirstAsync(e => e.EmployeeId == employee.Id);
                if (employeeInUserTeam != null)
                {
                    employeeInUserTeam.Role = "TeamLeder";
                }

                await dbContext.SaveChangesAsync();
                return Ok();
            }
            // bad request
            return StatusCode(400);
        }


        [HttpPost]
        [Route("/AddUserToTeams")]
        public async Task<IActionResult> AddUserToTeams(UpdateUserSTeamRequest updateUserSTeamRequest)
        {
            foreach (var teamName in updateUserSTeamRequest.teamNames)
            {
                var tname = await dbContext.Teams.FirstAsync(t => t.Name == teamName);
                var updateuserteam = new UserTeam()
                {
                    EmployeeId = updateUserSTeamRequest.employeeId,
                    TeamId = tname.Id,
                    Role = "Medarbeider", // default
                };
                await dbContext.UserTeams.AddAsync(updateuserteam);
            }
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("/Add/Team")]
        public async Task<IActionResult> AddTeam(AddTeamRequest addTeamRequest)
        {         
            var teamexist = await dbContext.Teams.FirstOrDefaultAsync(e => e.Name == addTeamRequest.teamName);
            if (teamexist != null)
            {
                return StatusCode(409);
            }

            var team = new Team()
                {
                    Name = addTeamRequest.teamName,
                };

                try
                {
                    await dbContext.Teams.AddAsync(team);
                    await dbContext.SaveChangesAsync();
                    return Ok(team);
                }
                catch
                {
                    return StatusCode(409);
                }
            
        }

        [HttpDelete]
        [Route("/Delete/Team/{id:int}")]
        public async Task<IActionResult> DeleteTeamById([FromRoute] int id)
        {
            var team = await dbContext.Teams.FindAsync(id);

            if (team != null)
            {
                dbContext.Remove(team);
                await dbContext.SaveChangesAsync();
                return Ok();

            }
            return NotFound();

        }
    }
}

