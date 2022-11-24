using System.Net.NetworkInformation;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nordic_Door.Server.Data;
using Nordic_Door.Shared.Models;
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
            var userTeams = await dbContext.UserTeams.ToListAsync();

            var updateUserTeams = new List<GetUserTeamRequest>();

            foreach (var userTeam in userTeams)
            {
                var team = await dbContext.Teams.FindAsync(userTeam.TeamId);
                var employee = await dbContext.Employees.FindAsync(userTeam.EmployeeId);
                var role = await dbContext.EmployeeRoles.FindAsync(userTeam.Role);

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

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetUserTeams(int id)
        {
            var userTeams = await dbContext.UserTeams.Where(x => x.EmployeeId == id).ToListAsync();

            var updateUserTeams = new List<GetUserTeamRequest>();

            foreach (var userTeam in userTeams)
            {
                var team = await dbContext.Teams.FindAsync(userTeam.TeamId);
                var employee = await dbContext.Employees.FindAsync(userTeam.EmployeeId);
                var role = await dbContext.EmployeeRoles.FindAsync(userTeam.Role);

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



        [HttpPut]
        public async Task<IActionResult> UpdateRole(int eId, int tId, UpdateUserTeamRoleRequest updateRole)
        {
            var role = await dbContext.UserTeams.FindAsync(eId, tId);


            if (role != null)
            {
                role.Role = updateRole.Role;

                await dbContext.SaveChangesAsync();

                return Ok(role);
            }

            return NotFound();
        }

        // overflødig???

        //[HttpPost]
        //public async Task<IActionResult> AddUserToTeam(AddUserTeamRequest addUserTeamRequest)
        //{
        //    var user = new UserTeam()
        //    {

        //        TeamId = addUserTeamRequest.TeamId,
        //        EmployeeId = addUserTeamRequest.EmployeeId,
        //        Role = addUserTeamRequest.Role,
        //    };

        //    try
        //    {
        //        await dbContext.UserTeams.AddAsync(user);
        //        await dbContext.SaveChangesAsync();
        //        return Ok(user);
        //    }
        //    catch
        //    {
        //        return StatusCode(409);
        //    }
        //}

        [HttpDelete]
        public async Task<IActionResult> DeleteUserfromTeam(int eId, int tID)
        {
            var user = await dbContext.UserTeams.FindAsync(eId, tID);

            if (user != null)
            {
                dbContext.Remove(user);
                await dbContext.SaveChangesAsync();
                return Ok();

            }
            return NotFound();
        }
    }
}

