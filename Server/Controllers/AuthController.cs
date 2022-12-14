using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nordic_Door.Server.Data;
using Nordic_Door.Shared.Models.Common;
using Nordic_Door.Shared.Models.API;
using Nordic_Door.Shared.Models.Database;
using System.Web.Helpers;

// AuthController har API punkter for autentsisering, her skal de metodene og kallene som er ment for innlogging og registering ligge. 

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

        // Autentisering av innlogging ved E-post og HasedPassord.
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> AuthUsernameAndPassword(LoginRequest loginRequest)
        {
            var user = await dbContext.Employees.FirstOrDefaultAsync(e => e.Email == loginRequest.Email);
            if (user != null)
            {
                // Innsjekk av Passord i databasen (Allerede hashed)  = Boolen.
                if (!Crypto.VerifyHashedPassword(user.Password, loginRequest.Password) )
                {
                    // unauthorized
                    return StatusCode(403);
                }
                
                var userTeams = await dbContext.UserTeams.Where(i => i.EmployeeId == user.Id).ToListAsync();
                var teamRelations = new List<UserTeamRelation>();
                
                foreach (var _user in userTeams)
                {
                    var team = await dbContext.Teams.FindAsync(_user.TeamId);
                    var userRole = _user.Role;

                    if (team == null) continue; // Se over nullhåndtering

                    teamRelations.Add(new UserTeamRelation()
                    {
                        UserRole = userRole,
                        Team = team
                    });
                }
                var userRelation = new UserRelation()
                {
                    EmployeeId = user.Id,
                    EmployeeName = user.Name,
                    EmployeeIsAdmin = user.IsAdmin,
                    EmployeeEmail = user.Email,
                    userTeamRelations = teamRelations,
                    EmployeeIsDisabled = user.IsDisabled,
                };
                return Ok(userRelation);
            }
            return StatusCode(401);
        }



        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(AddUserRequest addUserRequest)
        {
            var newEmployee = new Employee()
            {
                Name = addUserRequest.Name,
                Email = addUserRequest.Email,
                Password = Crypto.HashPassword(addUserRequest.Password),
                IsAdmin = addUserRequest.IsAdmin ? 1 : 0,
                IsDisabled = 0,
            };
            
            await dbContext.Employees.AddAsync(newEmployee);
            await dbContext.SaveChangesAsync();
            var value = await dbContext.Entry(newEmployee).GetDatabaseValuesAsync();
            
            if (newEmployee == null && value == null)
            {
                return StatusCode(500);
            }
 
            foreach (var team in addUserRequest.TeamNames)
            {
                var _team = await dbContext.Teams.FirstAsync(t => t.Name == team);

                    var NewUserteamForNewEmployee = new UserTeam()
                    {
                        EmployeeId = value.GetValue<int>("Id"),
                        TeamId = _team.Id,
                        Role = "Medarbeider", // default verdi
                    };
                   await dbContext.UserTeams.AddAsync(NewUserteamForNewEmployee);
            }
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}

