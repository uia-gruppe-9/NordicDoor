using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nordic_Door.Server.Data;

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

}
}
