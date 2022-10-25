using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nordic_Door.Server.Data;
using Nordic_Door.Shared.Models.API;
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
        [Route("/Search/All/teams")]
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
        public async Task<IActionResult> UpdateTeam([FromRoute] int id, UpdateTeamRequest updateTeamRequest)
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


        [HttpPost]
        [Route("/Add/Team")]
        public async Task<IActionResult> AddTeam(AddTeamRequest addTeamRequest)
        {
            var team = new Team()
            {

                Id = addTeamRequest.Id,
                Name = addTeamRequest.Name,

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

