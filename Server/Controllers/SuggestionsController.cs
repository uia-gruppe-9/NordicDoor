using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nordic_Door.Server.Data;
using Nordic_Door.Shared.Models.API;
using Nordic_Door.Shared.Models.Database;

namespace Nordic_Door.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuggestionsController : Controller
    {
        public NordicDoorsDbContext dbContext { get; set; }

        public SuggestionsController(NordicDoorsDbContext ctx)
        {
            dbContext = ctx;
        }

        [HttpGet]
        public async Task<IActionResult> GetSuggestions()
        {
            var suggestions = await dbContext.Suggestions.ToListAsync();
            var updateSuggestions = new List<GetSuggestionRequest>();

            foreach (var suggestion in suggestions)
            {
                var team = await dbContext.Teams.FindAsync(suggestion.TeamId);
                var resposibleEmployee = await dbContext.Employees.FindAsync(suggestion.ResponsibleEmployee);
                var responsibleTeam = await dbContext.Teams.FindAsync(suggestion.ResponsibleTeam);
                var createByEmployee = await dbContext.Employees.FindAsync(suggestion.CreatedBy);
                var Pictures = await dbContext.Pictures.Where(p => p.SuggestionId == suggestion.Id).ToListAsync();

                if (team == null || createByEmployee == null)
                {
                    continue;
                }

                updateSuggestions.Add(new GetSuggestionRequest()
                {
                    Team = team,
                    ResponsibleEmployee = resposibleEmployee,
                    ResponsibleTeam = responsibleTeam,
                    CreatedBy = createByEmployee,

                    Id = suggestion.Id,
                    Title = suggestion.Title,
                    CreatedAt = suggestion.CreatedAt,
                    DeadLine = suggestion.DeadLine,
                    LastUpdatedAt = suggestion.LastUpdatedAt,
                    Status = suggestion.Status,
                    Phase = suggestion.Phase,
                    Description = suggestion.Description,
                    Pictures = Pictures,
                }
                    );
            }

            return Ok(updateSuggestions);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetSuggestionById([FromRoute] int id)
        {

            var suggestion = await dbContext.Suggestions.FindAsync(id);
            var Pictures = await dbContext.Pictures.Where(p => p.SuggestionId == id).ToListAsync();

            if (suggestion == null)
            {
                return NotFound();
            }
            var team = await dbContext.Teams.FindAsync(suggestion.TeamId);
            var resposibleEmployee = await dbContext.Employees.FindAsync(suggestion.ResponsibleEmployee);
            var responsibleTeam = await dbContext.Teams.FindAsync(suggestion.ResponsibleTeam);
            var createByEmployee = await dbContext.Employees.FindAsync(suggestion.CreatedBy);


            if (team == null || createByEmployee == null)
            {
                return StatusCode(500);
            }
            var suggestionResponse = new GetSuggestionRequest()
            {

                Team = team,
                ResponsibleEmployee = resposibleEmployee,
                ResponsibleTeam = responsibleTeam,
                CreatedBy = createByEmployee,

                Id = suggestion.Id,
                Title = suggestion.Title,
                CreatedAt = suggestion.CreatedAt,
                DeadLine = suggestion.DeadLine,
                LastUpdatedAt = suggestion.LastUpdatedAt,
                Status = suggestion.Status,
                Phase = suggestion.Phase,
                Description = suggestion.Description,
                Pictures = Pictures,
            };

            return Ok(suggestionResponse);
        }

        [HttpGet]
        [Route("Search/Firstby/{title}")]
        public async Task<IActionResult> GetFirstSuggestionByTitle([FromRoute] string title)
        {

            var suggestion = await dbContext.Suggestions.FirstAsync(suggestion => suggestion.Title == title);

            if (suggestion == null)
            {
                return NotFound();
            }

            return Ok(suggestion);
        }


        [HttpGet]
        [Route("Search/All/Suggestion/{title}")]
        public async Task<IActionResult> GetAllSuggestionsByTitle([FromRoute] string title)
        {

            var suggestion = await dbContext.Suggestions.Where(suggestion => suggestion.Title == title).ToListAsync();

            if (suggestion == null)
            {
                return NotFound();
            }

            return Ok(suggestion);
        }



        [HttpPut] // Oppdatere Forbedringsforslag
        [Route("Update/{id:int}")]
        public async Task<IActionResult> UpdateSuggestion([FromRoute] int id, UpdateSuggestionRequest updateSuggestionRequest)

        {
            var suggestion = await dbContext.Suggestions.FindAsync(id);

            if (suggestion != null)
            {
                suggestion.ResponsibleEmployee = updateSuggestionRequest.ResponsibleEmployee;
                suggestion.ResponsibleTeam = updateSuggestionRequest.ResponsibleTeam;
                suggestion.LastUpdatedAt = updateSuggestionRequest.LastUpdatedAt;
                suggestion.Title = updateSuggestionRequest.Title;
                suggestion.DeadLine = updateSuggestionRequest.DeadLine;
                suggestion.Status = updateSuggestionRequest.Status;
                suggestion.Phase = updateSuggestionRequest.Phase;
                suggestion.Description = updateSuggestionRequest.Description;

                await dbContext.SaveChangesAsync();

                return Ok(suggestion);
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> AddSuggestion(AddSuggestionRequest addSuggestionRequest)
        {
            var suggestion = new Suggestion()
            {
                TeamId = addSuggestionRequest.TeamId,
                CreatedBy = addSuggestionRequest.CreatedBy,
                ResponsibleEmployee = addSuggestionRequest.ResponsibleEmployee,
                ResponsibleTeam = addSuggestionRequest.ResponsibleTeam,
                LastUpdatedAt = addSuggestionRequest.LastUpdatedAt,
                Title = addSuggestionRequest.Title,
                CreatedAt = addSuggestionRequest.CreatedAt,
                DeadLine = addSuggestionRequest.DeadLine,
                Status = addSuggestionRequest.Status,
                Phase = addSuggestionRequest.Phase,
                Description = addSuggestionRequest.Description,
            };

            await dbContext.Suggestions.AddAsync(suggestion);
            await dbContext.SaveChangesAsync();
            var value = await dbContext.Entry(suggestion).GetDatabaseValuesAsync();
            return Ok(value.GetValue<int>("Id"));


        }

        [HttpDelete]
        [Route("Delete/{id:int}")]

        public async Task<IActionResult> DeleteSuggestionById([FromRoute] int id)
        {
            var suggestion = await dbContext.Suggestions.FindAsync(id);

            if (suggestion != null)
            {
                dbContext.Remove(suggestion);
                await dbContext.SaveChangesAsync();
                return Ok();

            }
            return NotFound();
        }
    }
}

