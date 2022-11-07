using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nordic_Door.Server.Data;

namespace NordicDoor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeRolesController : Controller
    {

        public NordicDoorsDbContext dbContext { get; set; }
        public EmployeeRolesController(NordicDoorsDbContext ctx)
        {
            dbContext = ctx;
        }


        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var Emprole = await dbContext.EmployeeRoles.ToListAsync();


            return Ok(Emprole.Select(x => x.Role));
        }
    }
}

