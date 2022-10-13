using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nordic_Door.Server.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NordicDoor.Server.Controllers
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
            return Ok();
        }

    }
}

