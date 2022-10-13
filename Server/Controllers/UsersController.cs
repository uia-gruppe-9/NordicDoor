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
        [Route("/All/{name:String}")]
        public async Task<IActionResult> GetAllUsersByName([FromRoute] string name)
        {

            var user = await dbContext.Employees.SingleAsync(employee => employee.Name == name);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        

        //[HttpGet]
        //[Route("{name:string}")]
        //public async Task<IActionResult> GetUserByName([FromRoute] string name)
        //{
            
        //    var user = await dbContext.Employees.AllAsync(employee => employee.Name == name);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(user);
        //}


        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, UpdateUserRequest updateUserRequest)
        {
            var user = await dbContext.Employees.FindAsync(id);

            if (user != null)
            {
                user.TeamId = updateUserRequest.TeamId;
                user.Name = updateUserRequest.Name;
                user.Email = updateUserRequest.Email;
                user.Password = updateUserRequest.Password;
                user.Role = updateUserRequest.Role;

                await dbContext.SaveChangesAsync();

                return Ok(user);
            }
            return NotFound();
        }
        

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserRequest addUserRequest)
        {
            var user = new Employees()
            {

                Id = addUserRequest.Id,
                TeamId = addUserRequest.TeamId,
                Name = addUserRequest.Name,
                Email = addUserRequest.Email,
                Password = addUserRequest.Password,
                Role = addUserRequest.Role,
            };

            try
            {
                await dbContext.Employees.AddAsync(user);
                await dbContext.SaveChangesAsync();
                return Ok(user);
            
            }
            catch
            {
                return StatusCode(409);
            }

        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteUserById([FromRoute] int id)
        {
            var user = await dbContext.Employees.FindAsync(id);

            if(user != null)
            {
                dbContext.Remove(user);
                await dbContext.SaveChangesAsync();
                return Ok();

            }
            return NotFound();
        }

}
}
