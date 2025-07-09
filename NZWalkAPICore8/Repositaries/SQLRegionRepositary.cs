using Microsoft.EntityFrameworkCore;
using NZWalkAPICore8.Data;
using NZWalkAPICore8.Model.Domain;
using NZWalkAPICore8.Model.DTO;

namespace NZWalkAPICore8.Repositaries
{
    public class SQLRegionRepositary : IRegionRepositry
    {
        private readonly NZWalkDBContext _dbContext;
        public SQLRegionRepositary(NZWalkDBContext dBContext)
        {
            this._dbContext = dBContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            var result = await _dbContext.Regions.ToListAsync();
            return result;
            //or
            //return await _dbContext.Regions.ToListAsync();

        }

        public async Task<Region?> GetByIdAsync(Guid Id)
        {
            return await _dbContext.Regions.FirstOrDefaultAsync(r => r.Id == Id);
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateAsync(Guid Id, Region region)
        {
            var existingRegion = _dbContext.Regions.FirstOrDefault(r => r.Id == Id);
            if (existingRegion == null) { return null; }
            existingRegion.Code = region.Code;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Name = region.Name;
            existingRegion.Population = region.Population;

            await _dbContext.SaveChangesAsync();
            return existingRegion;

        }

        public async Task<Region?> DeleteByIdAsync(Guid Id)
        {
            var existingRegion = _dbContext.Regions.FirstOrDefault(r => r.Id == Id);
            if (existingRegion == null) { return null; }
            
            _dbContext.Regions.Remove(existingRegion);
            await _dbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
