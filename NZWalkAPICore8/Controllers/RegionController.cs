using System;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.InteropServices;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalkAPICore8.Data;
using NZWalkAPICore8.Model.Domain;
using NZWalkAPICore8.Model.DTO;
using NZWalkAPICore8.Repositaries;

namespace NZWalkAPICore8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RegionController : ControllerBase
    {
        private readonly NZWalkDBContext _dbContext;
        private readonly IRegionRepositry _regionRepositary;

        public  IMapper mapper { get; }

        public RegionController(NZWalkDBContext nZWalkDBContext, IRegionRepositry regionRepositary, IMapper _mapper)
        {
            _dbContext = nZWalkDBContext;
            this._regionRepositary = regionRepositary;
            mapper = _mapper;
        }

        [HttpGet]
        [Authorize(Roles ="Reader")]
        public async Task<IActionResult> GetRegions()
        {
            //Get Domain Model from Database
            //var result = await _dbContext.Regions.ToListAsync();
            var result = await _regionRepositary.GetAllAsync();

            ////Convert From Domain Model to DTO
            //var regionDto = new List<RegionDto>();
            //foreach (var region in result) {
            //    regionDto.Add(new RegionDto()
            //    {
            //        Id = region.Id, Name = region.Name, Code = region.Code, Area = region.Area,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population
            //    });
            //}

            // Convert From Domain Model to DTO via AutoMapper
            var regionDto = mapper.Map<List<RegionDto>>(result);
            return Ok(regionDto);
        }
        [HttpGet]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetRegionById([FromRoute]Guid Id)
        {
            //var result = await _dbContext.Regions.AsNoTracking().Where(p => p.Id == Id).FirstOrDefaultAsync();
            //or
            //var result = await _dbContext.Regions.FirstOrDefaultAsync(p => p.Id == Id); 
            //or
            var result = await _regionRepositary.GetByIdAsync(Id);

            if (result == null) 
                return NotFound();
            else
            {
                ////Convert from Domain to DTO
                //var regionDTO= new RegionDto()
                //{
                //    Id = result.Id,
                //    Name = result.Name,
                //    Code = result.Code,
                //    Area = result.Area,
                //    Lat = result.Lat,
                //    Long = result.Long,
                //    Population = result.Population
                //};

                //Convert From Domain to DTO via Mapper
                var regionDTO = mapper.Map<RegionDto>(result);
                return Ok(regionDTO);
            }
                
        }
        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> AddRegion([FromBody] AddRegionDto regionDto)
        {
            ////Map or Convert Dto to Domain Model
            //Region regionDomain = new Region()
            //{ 
            //    Id = Guid.NewGuid() ,
            //    Code = regionDto.Code,
            //    Name = regionDto.Name,
            //    Area = regionDto.Area,
            //    Lat = regionDto.Lat,
            //    Long = regionDto.Long,
            //    Population = regionDto.Population
            //};
            if (ModelState.IsValid)
            {
                //Convert AddRegionDto DTO to Domain Model
                var regionDomain = mapper.Map<Region>(regionDto);
                //Save the Region
                //await _dbContext.Regions.AddAsync(regionDomain);
                //await _dbContext.SaveChangesAsync();
                //OR
                var result = await _regionRepositary.CreateAsync(regionDomain);

                regionDto = mapper.Map<AddRegionDto>(result);

                return CreatedAtAction(nameof(GetRegionById), new { Id = regionDomain.Id }, regionDto);
            }
            return BadRequest(ModelState);
        }
        
        [HttpPut]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] UpdateRegionDto regionDto)
        {
            //var result = await _dbContext.Regions.Where(p => p.Id == Id).FirstOrDefaultAsync();
            //Or
            //var result = await _dbContext.Regions.FirstOrDefaultAsync(p => p.Id == Id);
            //if (result == null)
            //    return NotFound();

            ////Map or Convert Dto to Domain Model
            //result.Name = regionDto.Name;
            //result.Area = regionDto.Area;
            //result.Lat = regionDto.Lat;
            //result.Long = regionDto.Long;
            //result.Population = regionDto.Population;

            //await _dbContext.SaveChangesAsync();
            //Convert UpdateRegionDTO to Region Domain Model
            //var result = new Region()
            //{
            //    Code = "",
            //    Name = regionDto.Name,
            //    Area = regionDto.Area,
            //    Lat = regionDto.Lat,
            //    Long = regionDto.Long,
            //    Population = regionDto.Population
            //};
            if (ModelState.IsValid)
            {
                var RegionDomain = mapper.Map<Region>(regionDto);
                RegionDomain = await _regionRepositary.UpdateAsync(Id, RegionDomain);
                if (RegionDomain == null)
                    return NotFound();

                ////Convert domain to DTO
                //RegionDto regionDto1 = new RegionDto()
                //{
                //    Id = (Guid)result?.Id,
                //    Code = result.Code,
                //    Name = result.Name,
                //    Area = result.Area,
                //    Lat = result.Lat,
                //    Long = result.Long,
                //    Population = result.Population
                //};
                var regionDto1 = mapper.Map<RegionDto>(RegionDomain);
                return Ok(regionDto1);
            }
            return BadRequest(ModelState);

        }
        [HttpDelete]
        [Route("{ID:GUID}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid ID)
        {
            // var result = await _dbContext.Regions.Where(p => p.Id == ID).FirstOrDefaultAsync();
            // if (result == null)
            //     return NotFound();
            // _dbContext.Regions.Remove(result);
            //await _dbContext.SaveChangesAsync();

            var result = await _regionRepositary.DeleteByIdAsync(ID);
            if (result == null) return NotFound();
            return Ok();

        }


    }
}
