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

        [HttpPut] // må se på
        [Route("/UpdateTeamLeder")]
        // vil først hente ut teamleder i nevnt team
        // gjøre teamleder til medarbeider
        // gjøre innput employee til leder av innput team
        public async Task<IActionResult> UpdateTeamLeaderInTeam(string teamName, string employeeName)
        {
            var team = await dbContext.Teams.FirstAsync(e => e.Name == teamName);
            var userTeamOldTL = await dbContext.UserTeams.FirstAsync(tL => tL.TeamId == team.Id);

            var oldTeamLeader = await dbContext.UserTeams.FirstAsync(l => l.Role == "Leader");


            var employee = await dbContext.Employees.FirstAsync(e => e.Name == employeeName);
            var userTeam = await dbContext.UserTeams.FirstAsync(e => e.EmployeeId == employee.Id);
            return Ok();

        }


        [HttpPost] // Endre employee sitt team || TOBIAS SE PÅ - her skjer det mye rart
        [Route("/AddTeamsToUser")]
        public async Task<IActionResult> AddTeamsToUser(UpdateUserSTeamRequest updateUserSTeamRequest)
        {        
            foreach (var teamName in updateUserSTeamRequest.teamNames)
            {
                var tname = dbContext.Teams.FirstAsync(t => t.Name == teamName);
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

        [HttpPost] // FIKSE DENNE!
        // skal ved navninput opprette nytt team
        [Route("/Add/Team")]
        public async Task<IActionResult> AddTeam(AddTeamRequest addTeamRequest)
        {
            // prøver å error handle
            //var teamexist = await dbContext.Teams.FirstAsync(e => e.Name == addTeamRequest.teamName);
            //if (teamexist == null)
            //{
            //    return StatusCode(409);
            //}

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

