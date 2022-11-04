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

    public class SuggestionPhaseController : Controller
    {
        public NordicDoorsDbContext dbContext { get; set; }
        public SuggestionPhaseController(NordicDoorsDbContext ctx)
        {
            dbContext = ctx;
        }

        [HttpGet] 
        public async Task<IActionResult>GetPhases()
        {
            var Phase = await dbContext.SuggestionPhase.ToListAsync();
            return Ok(Phase.Select(x => x.Phase));
        }




    }


}