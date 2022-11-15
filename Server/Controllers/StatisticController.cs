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
        [Route("/byId/{id:int}")]
        public async Task<IActionResult> GetEmployeeStatistics([FromRoute] int id)
        {
            var responsibleEmployeeSuggestions = await dbContext.Suggestions.Where(r => r.ResponsibleEmployee == id).ToListAsync();
            return Ok(responsibleEmployeeSuggestions.Count);
        }

        [HttpGet]
        [Route("/teamStatisticRange")]
        //Ønsker å se antall forbedringer totalt pr.team hittil i mnd, hittil år, og utvikling år for år
        public async Task<IActionResult> GetTeamStatisticsByDateRange(int id, string status, DateTime startTime, DateTime endTime)
        {
            var closedSuggestion = await dbContext.Suggestions.Where
            (s => (s.ResponsibleTeam == id) && (s.Status == status) && (s.CreatedAt > startTime) && (s.LastUpdatedAt < endTime)).ToListAsync();
            return Ok(closedSuggestion.Count);
        }

        [HttpGet]
        [Route("/statusSuggestion")]
        //Ønsker å se antall utførte forbedringer totalt for bedriften år for år
        public async Task<IActionResult> GetTotalSuggestionsClosed (string status)
        {          
            var statusSuggestion = await dbContext.Suggestions.Where(s => s.Status == status).ToListAsync();
            return Ok(statusSuggestion.Count);
        }
        [HttpGet]
        [Route("/teamStatisticsPrEmployee")]    
        //Ønsker å se antall forbedringer utført pr.team som snitt av antall ansatte pr.team - grafisk fremstilt med farmer som bytter ved oppnådd måltall pr.mnd  
        public async Task<IActionResult> GetTeamSuggestionsStatisticsByAverage(int responsible, int id)
        {
            var averageResponsibleTeamStatistics = await dbContext.Suggestions.Where(a => a.ResponsibleTeam == responsible).ToListAsync();
            var averageEmployeeInTeam = await dbContext.UserTeams.Where(a => a.TeamId == id).ToListAsync();
            return Ok(averageResponsibleTeamStatistics.Count / averageEmployeeInTeam.Count);
        }

        
    }

}
