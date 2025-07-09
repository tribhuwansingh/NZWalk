using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NZWalkAPICore8.Data;

namespace NZWalkAPICore8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiffercultyController : ControllerBase
    {
        public readonly NZWalkDBContext nZWalkDBContext;
        public DiffercultyController(NZWalkDBContext _nZWalkDBContext)
        {
            nZWalkDBContext= _nZWalkDBContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetDifferculty()
        {
            var result = await nZWalkDBContext.Difficulty.ToListAsync();
            return Ok(result);
        }

    }
}
