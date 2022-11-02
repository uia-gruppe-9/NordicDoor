using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nordic_Door.Server.Data;
using Nordic_Door.Shared.Models.Common;



namespace NordicDoor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {

        public NordicDoorsDbContext dbContext { get; set; }

        public AuthController(NordicDoorsDbContext ctx)
        {
            dbContext = ctx;
        }


        [HttpPost]
        public async Task<IActionResult> AuthUsernameAndPassword(string email, string password)
        {
            var user = await dbContext.Employees.SingleAsync(e => e.Email == email && e.Password == password);

           if (user != null)
           {

                var userInUserteams = await dbContext.UserTeams.Where(e => e.EmployeeId == user.Id).ToListAsync();

                var teamRelations = new List<UserTeamRelation>();
                foreach (var userteam in userInUserteams)
                {

                    var team = await dbContext.Teams.FindAsync(userteam.TeamId);
                    
                    var userRole = userteam.Role;

                    teamRelations.Add(new UserTeamRelation(
                    ){
                        EmployeeName = user.Name,
                        EmployeeEmail = user.Email,
                        EmployeeIsAdmin = user.IsAdmin,
                        EmployeeId = user.Id,

                        UserRole = userRole,
                        Team = team
                    });
                }


                return Ok(teamRelations);
            }

            return StatusCode(500);
        }
    }
}

