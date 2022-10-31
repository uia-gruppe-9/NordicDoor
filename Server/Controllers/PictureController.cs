using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Nordic_Door.Server.Data;
using Nordic_Door.Shared.Models.API;
using Nordic_Door.Shared.Models.Database;
using System.IO;
namespace Nordic_Door.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PictureController : Controller
    {
        public NordicDoorsDbContext dbContext { get; set; }

        public PictureController(NordicDoorsDbContext ctx)
        {
            dbContext = ctx;
        }

        [HttpPost]
        [Route("/Add/Pictures")] // Fjern denne, holder den for markus sin skyld

        public async Task<IActionResult> AddPicture(AddPictureRequest addPictureRequest)
        {

            var picture = new Picture()
            {
                Id = addPictureRequest.Id,
                SuggestionId = addPictureRequest.SuggestionId,
                EmployeeId = addPictureRequest.EmployeeId,
                UploadedAt = addPictureRequest.UploadedAt,
                Image = addPictureRequest.Image,

            };
            await dbContext.Pictures.AddAsync(picture);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
