using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nordic_Door.Server.Data;
using Nordic_Door.Shared.Models.API;
using Nordic_Door.Shared.Models.Database;
using static MudBlazor.Colors;


namespace Nordic_Door.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class EventsController : Controller
    {

        public NordicDoorsDbContext dbContext { get; set; }

        public EventsController(NordicDoorsDbContext ctx)
        {
            dbContext = ctx;
        }



        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await dbContext.Events.ToListAsync();
            return Ok(events);
        }

        //Lage funksjon som viser informasjon til spesifik ID (HttpGet)

        [HttpGet]
        [Route("/Search/EventsById{id:int}")]
        public async Task<IActionResult> GetEventsById([FromRoute] int id)
        {
            var events = await dbContext.Events.FindAsync(id);

            if (events == null)
            {
                return NotFound();
            }

            return Ok(events);
        }

        //Funksjon som lager nye events(HttpPut)



    }
}

