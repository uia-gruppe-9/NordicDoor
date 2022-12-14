using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nordic_Door.Server.Data;
using Nordic_Door.Shared.Models.API;
using Nordic_Door.Shared.Models.Database;

namespace Nordic_Door.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {

        public NordicDoorsDbContext dbContext { get; set; }

        public UsersController(NordicDoorsDbContext ctx)
        {
            dbContext = ctx;
        }


        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await dbContext.Employees.ToListAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var user = await dbContext.Employees.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> GetFirstUserByName([FromRoute] string name)
        {

            var user = await dbContext.Employees.FirstAsync(employee => employee.Name == name);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpPut]
        [Route("/Update/User/{id:int}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, UpdateUserRequest updateUserRequest)
        {
            var user = await dbContext.Employees.FindAsync(id);

            if (user != null)
            {
                user.Name = updateUserRequest.Name;
                user.Email = updateUserRequest.Email;
                user.Password = updateUserRequest.Password;

                await dbContext.SaveChangesAsync();

                return Ok(user);
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserRequest addUserRequest)
        {
            var user = new Employee()
            {
                Name = addUserRequest.Name,
                Email = addUserRequest.Email,
                Password = addUserRequest.Password,
                IsAdmin = addUserRequest.IsAdmin ? 1 : 0,
                IsDisabled = 0,
            };
            await dbContext.Employees.AddAsync(user);
            await dbContext.SaveChangesAsync();

            var value = await dbContext.Entry(user).GetDatabaseValuesAsync();
            var id = value.GetValue<int>("Id");

            foreach (var name in addUserRequest.TeamNames)
            {
                var team = dbContext.Teams.FirstAsync(t => t.Name == name);
                if (team == null) continue; // burde kanskje håndteres ordentlig

                await dbContext.UserTeams.AddAsync(new UserTeam()
                {
                    EmployeeId = id,
                    TeamId = team.Id,
                    Role = "Medarbeider", //default
                });
            }
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("/Delete/User/{id:int}")]
        public async Task<IActionResult> DeleteUserById([FromRoute] int id)
        {
            var user = await dbContext.Employees.FindAsync(id);

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
