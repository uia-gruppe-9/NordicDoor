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
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            var comment = await dbContext.SuggestionComments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);

        }

        [HttpGet]
        public async Task<IActionResult> GetCommentRequest()
        {
            var comments = await dbContext.SuggestionComments.ToListAsync();
            var updateComment = new List<GetSuggestionCommentRequest>();

            foreach (var comment in comments)

            {
                var employee = await dbContext.Employees.FindAsync(comment.EmployeeId);
                var suggestion = await dbContext.Suggestions.FindAsync(comment.SuggestionId);

                if (employee == null || suggestion == null)
                {
                    continue;
                }

                updateComment.Add(new GetSuggestionCommentRequest()
                {
                    Id = comment.Id,
                    Employee = employee,
                    Suggestion = suggestion,

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
                Id = addSuggestionCommentRequest.Id,
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