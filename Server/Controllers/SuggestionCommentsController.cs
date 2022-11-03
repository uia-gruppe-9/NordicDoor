using System.Net.NetworkInformation;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nordic_Door.Server.Data;
using Nordic_Door.Shared.Models;
using Nordic_Door.Shared.Models.API;
using Nordic_Door.Shared.Models.Database;

namespace NordicDoor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class SuggestionCommentsController : Controller
    {
        public NordicDoorsDbContext dbContext { get; set; }
        public SuggestionCommentsController(NordicDoorsDbContext ctx)
        {
            dbContext = ctx;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCommentBySuggeestionId([FromRoute] int id)
        {
            // Finn alle comments hvor suggestion ID er like alle comments du får inn fra frontend
            var comments = await dbContext.SuggestionComments.Where(c => c.SuggestionId == id).ToListAsync();
            if (comments == null)
            {
                return NotFound();
            }
            var updateComment = new List<GetSuggestionCommentRequest>();
            foreach (var comment in comments)

            {
                var employee = await dbContext.Employees.FindAsync(comment.EmployeeId);
                

                if (employee == null)
                {
                    continue;
                }

                updateComment.Add(new GetSuggestionCommentRequest()
                {
                    Id = comment.Id,
                    EmployeeName = employee.Name,
                    Comment = comment.Comment,
                    Timestamp = comment.Timestamp,
                }
                    );



            }

            return Ok(updateComment);

        }

        [HttpPost]
        [Route("/Add/Comment")]

        public async Task<IActionResult> AddComment(AddSuggestionCommentRequest addSuggestionCommentRequest)
        {
            var comment = new SuggestionComment()
            {
                EmployeeId = addSuggestionCommentRequest.EmployeeId,
                SuggestionId = addSuggestionCommentRequest.SuggestionId,
                Comment = addSuggestionCommentRequest.Comment,
                Timestamp = addSuggestionCommentRequest.Timestamp,
            };
            await dbContext.SuggestionComments.AddAsync(comment);
            await dbContext.SaveChangesAsync();
            return Ok();

        }



    }
}