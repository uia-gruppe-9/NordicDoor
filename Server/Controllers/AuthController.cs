using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nordic_Door.Server.Data;
using Nordic_Door.Shared.Models.Common;
using Nordic_Door.Shared.Models.API;
using Nordic_Door.Shared.Models.Database;




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
        [Route("Login")]
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
                    ) {
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

                };



                return Ok(userRelation);

            }

            return StatusCode(500);
        }



        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateUserxx(CreateUserRequest createUserRequest)
        {
            var createUser = new Employee()
            {
                Name = createUserRequest.EmployeeName,
                Id = createUserRequest.EmployeeId,
                Email = createUserRequest.EmployeeEmail,
                IsAdmin = createUserRequest.EmployeeIsAdmin,
                Password = createUserRequest.Password,
            };


            //var team = await dbContext.Teams.FindAsync();

            var createUserTeamAndRole = new UserTeam()
            {
                EmployeeId = createUser.Id,
                TeamId = createUserRequest.TeamId,
                Role = createUserRequest.Role,
            };

            await dbContext.Employees.AddAsync(createUser);
            await dbContext.UserTeams.AddAsync(createUserTeamAndRole);
            await dbContext.SaveChangesAsync();
            return Ok(createUserTeamAndRole);
        }

        [HttpPost]
        [Route("CreateUser")]

        public async Task<IActionResult> CreateUser(string name, string email, string password,
            int isAdmin, string teamName, string role)
        {
            var newEmployee = new Employee()
            {
                Name = name,
                Email = email,
                Password = password,
                IsAdmin = isAdmin,
                //Id = eid,
            };
            var findteam = await dbContext.Teams.FirstAsync(team => team.Name == teamName);
            await dbContext.Employees.AddAsync(newEmployee);
            await dbContext.SaveChangesAsync();

            var value = await dbContext.Entry(newEmployee).GetDatabaseValuesAsync();

            if (newEmployee != null && value != null)
            {
                var NewUserteamForNewEmployee = new UserTeam()
                {
                    EmployeeId = value.GetValue<int>("Id"),
                    TeamId = findteam.Id,
                    Role = role,
                };


                await dbContext.UserTeams.AddAsync(NewUserteamForNewEmployee);
                await dbContext.SaveChangesAsync();
                return Ok(NewUserteamForNewEmployee);
            }
            return StatusCode(500);
        }
    

    }
}

