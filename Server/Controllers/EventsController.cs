using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nordic_Door.Server.Data;
using Nordic_Door.Shared.Models.API;
using Nordic_Door.Shared.Models.Database;



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

        //Lag funksjon som henter ut et event objekt

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await dbContext.Events.ToListAsync();
            var updateEvents = new List<GetEventRequest>();

            foreach (var _event in events)

            {
                var employee = await dbContext.Employees.FindAsync(_event.EmployeeId);
                var suggestion = await dbContext.Suggestions.FindAsync(_event.SuggestionId);

                if (employee == null || suggestion == null)
                {
                    continue;
                }
           
                updateEvents.Add(new GetEventRequest()
                {
                    Id = _event.Id,
                    Employee = employee,
                    Suggestion = suggestion,

                    Description = _event.Description,
                    Timestamp = _event.Timestamp,
                }
                    );



            }
            return Ok(updateEvents);

        }

        [HttpGet]
        [Route("{suggestionId:int}")]

        public async Task<IActionResult> GetEventsBySuggestion([FromRoute] int suggestionId)
        {
            var events = await dbContext.Events.Where(_event => _event.SuggestionId == suggestionId).ToListAsync();
            var updateEvents = new List<GetEventRequest>();

            foreach (var _event in events)

            {
                var employee = await dbContext.Employees.FindAsync(_event.EmployeeId);
                var suggestion = await dbContext.Suggestions.FindAsync(_event.SuggestionId);

                if (employee == null || suggestion == null)
                {
                    continue;
                }

                updateEvents.Add(new GetEventRequest()
                {
                    Id = _event.Id,
                    Employee = employee,
                    Suggestion = suggestion,

                    Description = _event.Description,
                    Timestamp = _event.Timestamp,
                }
                    );



            }
            return Ok(updateEvents);

        }

        //Funksjon som lager nye events(HttpPut)

        [HttpPost]
        [Route("Add")]

        public async Task<IActionResult> AddEvent(AddEventRequest addEventRequest)
        {
            var _event = new Event()
            {
                EmployeeId = addEventRequest.EmployeeId,
                SuggestionId = addEventRequest.SuggestionId,
                Description = addEventRequest.Description,
                Timestamp = addEventRequest.Timestamp,
            };
            await dbContext.Events.AddAsync(_event);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}

