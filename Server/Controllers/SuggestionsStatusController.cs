using System.Net.NetworkInformation;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nordic_Door.Server.Data;
using Nordic_Door.Shared.Models;
using Nordic_Door.Shared.Models.API;
using Nordic_Door.Shared.Models.Database;

namespace NordicDoor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuggestionStatusController : Controller
    {

        public NordicDoorsDbContext dbContext { get; set; }

        public SuggestionStatusController(NordicDoorsDbContext ctx)
        {
            dbContext = ctx;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatus()
        {
            var SuStatus = await dbContext.SuggestionStatus.ToListAsync();
            return Ok(SuStatus.Select(x => x.Status));
        }

    }
}

