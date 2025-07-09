using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalkAPICore8.CustomActionFilter;
using NZWalkAPICore8.Data;
using NZWalkAPICore8.Model.Domain;
using NZWalkAPICore8.Model.DTO;
using NZWalkAPICore8.Repositaries;

namespace NZWalkAPICore8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly NZWalkDBContext nZWalkDBContext;
        private readonly IWalkRepositry walkRepositry;

        public WalksController(IMapper mapper,NZWalkDBContext nZWalkDBContext,IWalkRepositry walkRepositry)
        {
            this.mapper = mapper;
            this.nZWalkDBContext = nZWalkDBContext;
            this.walkRepositry = walkRepositry;
        }
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult> AddWalk([FromBody] AddWalkDto addWalkDto)
        {
            //if (ModelState.IsValid)
            //{
                //Map Walk DTO to Walk Domain Model
                var walkDomain = mapper.Map<Walk>(addWalkDto);
                //await nZWalkDBContext.Walks.AddAsync(walkDomain);
                //await nZWalkDBContext.SaveChangesAsync();
                walkDomain =await walkRepositry.CreateAsync(walkDomain);
                walkDomain = await walkRepositry.GetByIdAsync(walkDomain.Id);
                //Map Walk Domain to Walk DTO Model
                WalkDto walkDto = mapper.Map<WalkDto>(walkDomain);
                return Ok(walkDto);
                //return CreatedAtAction(nameof(GetWalkById), new { Id = walkDomain.Id }, walkDto);
            //}
            //return BadRequest(ModelState);
        }
        //GET: /api/Walks?filterOn=Name&filterQuery=track&sortBy=Name&isAscending=true&pageNo=1&pageSize=1000
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn=null,[FromQuery] string? filterQuery=null, [FromQuery] string? sortBy = null, 
            [FromQuery] bool isAscending = true, [FromQuery] int pageNo=1, [FromQuery] int pageSize = 1000)
        {
            var walksDomainModel = await walkRepositry.GetAllAsync(filterOn, filterQuery, sortBy, isAscending, pageNo, pageSize);
            var walksDto = mapper.Map<List<WalkDto>>(walksDomainModel);
            return Ok(walksDto);
        }

        [HttpGet]
        [Route("{guid:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid guid)
        {
            //var result = await nZWalkDBContext.Walks.FirstOrDefaultAsync(x => x.Id == guid);
            var walksDomainModel = await walkRepositry.GetByIdAsync(guid);
            if (walksDomainModel == null)
                return NotFound();
            var walkDto = mapper.Map<WalkDto>(walksDomainModel);
            return Ok(walkDto);
        }
        [HttpDelete]
        [Route("{guid:Guid}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid guid)
        {
            var result =await walkRepositry.DeleteByIdAsync(guid);
            if (result == null)
                return NotFound();
            return Ok(mapper.Map<WalkDto>(result));

        }
        [HttpPut]
        [Route("{guid:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid guid, [FromBody] UpdateWalkDto updateWalkDto) 
        {
            //if (ModelState.IsValid)
            //{
                var updatedWalkDomain = mapper.Map<Walk>(updateWalkDto);
                var result = await walkRepositry.UpdateAsync(guid, updatedWalkDomain);
                if (result == null)
                    return NotFound();

                return Ok(mapper.Map<WalkDto>(result));
            //}
            //return BadRequest(ModelState);
        }
    }
}
