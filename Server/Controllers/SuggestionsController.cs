using Microsoft.AspNetCore.Mvc;
using Nordic_Door.Server.Data;

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


}
}
