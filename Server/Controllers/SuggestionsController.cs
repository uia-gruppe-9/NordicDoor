using System.Xml.Linq;
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
    public class SuggestionController : Controller
    {
        public NordicDoorsDbContext dbContext { get; set; }

        public SuggestionController(NordicDoorsDbContext ctx)
        {
            dbContext = ctx;
        }

        [HttpGet]
        [Route("/Search/All/Suggestion")]
        public async Task<IActionResult> GetSuggestions()
        {
            var suggestion = await dbContext.Suggestions.ToListAsync();
            return Ok(suggestion);
        }

        [HttpGet]
        [Route("/Search/SuggestionById{id:int}")]
        public async Task<IActionResult> GetSuggestionById([FromRoute] int id)
        {
            var suggestion = await dbContext.Suggestions.FindAsync(id);

            if (suggestion == null)
            {
                return NotFound();
            }

            return Ok(suggestion);
        }

        [HttpGet]
        [Route("/Search/Firstby/{title}")]
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
        [Route("/Search/All/Suggestion/{title}")]
        public async Task<IActionResult> GetAllSuggestionsByTitle([FromRoute] string title)
        {

            var suggestion = await dbContext.Suggestions.Where( suggestion => suggestion.Title  == title).ToListAsync();

            if ( suggestion == null)
            {
                return NotFound();
            }

            return Ok(suggestion);
        }


        [HttpPut]
        [Route("/Update/Suggestion/{id:int}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, UpdateSuggestionRequest updateSuggestionRequest)
        {
            var suggestion = await dbContext.Suggestions.FindAsync(id);

            if (suggestion != null)
            {
                suggestion.TeamId = updateSuggestionRequest.TeamId;
                suggestion.ResponsibleEmployee = updateSuggestionRequest.ResponsibleEmployee;
                suggestion.ResponsibleTeam = updateSuggestionRequest.ResponsibleTeam;
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
        [Route("/Add/Suggestion")]
        public async Task<IActionResult> AddSuggestion(AddSuggestionRequest addSuggestionRequest)
        {
            var suggestion = new Suggestion()
            {

                TeamId = addSuggestionRequest.TeamId,
                CreatedBy = addSuggestionRequest.CreatedBy,
                ResponsibleEmployee = addSuggestionRequest.ResponsibleEmployee,
                ResponsibleTeam = addSuggestionRequest.ResponsibleTeam,
                Title = addSuggestionRequest.Title,
                DeadLine = addSuggestionRequest.DeadLine,
                Status = addSuggestionRequest.Status,
                Phase = addSuggestionRequest.Phase,
                Description = addSuggestionRequest.Description,
            };
            
                await dbContext.Suggestions.AddAsync(suggestion);
                await dbContext.SaveChangesAsync();
                return Ok(suggestion);

            
        }

        [HttpDelete]
        [Route("/Delete/Suggestion/{id:int}")]
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

