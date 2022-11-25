using System;
using System.Collections.Immutable;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nordic_Door.Server.Data;
using Nordic_Door.Shared.Models.API;
using Nordic_Door.Shared.Models.Database;
using static MudBlazor.Colors;


// Ønsker å se antall forbedringer utført pr.team som snitt av antall ansatte pr.team - grafisk fremstilt med farmer som bytter ved oppnådd måltall pr.mnd
// Ønsker å se antall forbedringer totalt pr.team hittil i mnd, hittil år, og utvikling år for år
// Ønsker å se antall utførte forbedringer totalt for bedriften år for år
// Ønsker å se antall forbedringer pr ansatt, men ikke som visning på hovedskjerm / Admin tilgang

namespace NordicDoor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticController : Controller
    {

        public NordicDoorsDbContext dbContext { get; set; }

        public StatisticController(NordicDoorsDbContext ctx)
        {
            dbContext = ctx;
        }


        [HttpGet]     
        [Route("byId/{id:int}")]
        public async Task<IActionResult> GetEmployeeStatistics([FromRoute] int id)
        {
            var responsibleEmployeeSuggestions = await dbContext.Suggestions.Where(r => r.ResponsibleEmployee == id).ToListAsync();
            return Ok(responsibleEmployeeSuggestions.Count);
        }

        [HttpGet]
        [Route("/teamStatisticRange")]
        //Ønsker å se antall forbedringer totalt pr.team hittil i mnd, hittil år, og utvikling år for år
        public async Task<IActionResult> GetTeamStatisticsByDateRange([FromQuery]GetTeamStatisticsByDateRange getTeamStatisticsByDateRange)
        {            
            var closedSuggestion = await dbContext.Suggestions.Where
            (s => (s.ResponsibleTeam == getTeamStatisticsByDateRange.Id) && (s.Status == getTeamStatisticsByDateRange.Status) && 
            (s.CreatedAt > getTeamStatisticsByDateRange.startTime) && (s.LastUpdatedAt < getTeamStatisticsByDateRange.endTime)).ToListAsync();
            return Ok(closedSuggestion.Count);
        }

        [HttpGet]
        [Route("/statusSuggestion")]
        //Ønsker å se antall utførte forbedringer totalt for bedriften år for år
        public async Task<IActionResult> GetTotalSuggestionsClosed ([FromQuery]GetTotalSuggestions getTotalSuggestions)
        {          
            var statusSuggestion = await dbContext.Suggestions.Where(s => s.Status == getTotalSuggestions.status).ToListAsync();
            return Ok(statusSuggestion.Count);
        }

        [HttpGet]
        [Route("/teamStatisticsPrEmployee")]
        //Ønsker å se antall forbedringer utført pr.team som snitt av antall ansatte pr.team - grafisk fremstilt med farmer som bytter ved oppnådd måltall pr.mnd  
        public async Task<IActionResult> GetTeamStatisticsByAverage([FromQuery] GetTeamSuggestionsStatisticByAverage getTeamSuggestionsStatistic)
        {
            var teamStatisticByAverage = await dbContext.Suggestions.Where(ts => (ts.ResponsibleTeam == getTeamSuggestionsStatistic.responsible)
            && (ts.ResponsibleTeam == getTeamSuggestionsStatistic.id)).ToListAsync();
            return Ok(teamStatisticByAverage.Count);
        }

        [HttpGet]
        [Route("/testTeamStatistics")]
        public async Task<IActionResult> TestGetTeamStatisticsByDateRange ([FromQuery]GetTeamStatisticsByDateRange getTeamStatisticsByDateRange)
        {
            var testTeamStatistics = await dbContext.Suggestions.Where(t => t.ResponsibleTeam == getTeamStatisticsByDateRange.Id).ToListAsync();
            return Ok(testTeamStatistics);
        }



    }

}
