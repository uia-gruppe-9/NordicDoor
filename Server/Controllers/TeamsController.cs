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
        [Route("{id:int}")]
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
        [Route("{name}")]
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
        [Route("/Search/All/{names}")]
        public async Task<IActionResult> GetAllTeamsByName([FromRoute] string name)
        {

            var team = await dbContext.Teams.Where(employee => employee.Name == name).ToListAsync();

            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateTeam([FromRoute] int id, UpdateTeamRequest updateTeamRequest)
        {
            var team = await dbContext.Teams.FindAsync(id);

            if (team != null)
            {
                team.LeaderId = updateTeamRequest.LeaderId;
                team.Name = updateTeamRequest.Name;


                await dbContext.SaveChangesAsync();

                return Ok(team);
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> AddTeam(AddTeamRequest addTeamRequest)
        {
            var team = new Team()
            {

                Id = addTeamRequest.Id,
                LeaderId = addTeamRequest.LeaderId,
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
        [Route("{id:int}")]
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

