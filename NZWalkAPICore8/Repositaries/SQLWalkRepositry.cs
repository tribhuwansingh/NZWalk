using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalkAPICore8.Data;
using NZWalkAPICore8.Model.Domain;

namespace NZWalkAPICore8.Repositaries
{
    public class SQLWalkRepositry : IWalkRepositry
    {
        public NZWalkDBContext DbContext { get; }
        public SQLWalkRepositry(NZWalkDBContext dbContext)
        {
            DbContext = dbContext;
        }

        

        public async Task<Walk>  CreateAsync(Walk walk)
        {
           await DbContext.Walks.AddAsync(walk);
            await DbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteByIdAsync(Guid Id)
        {
            var result= await DbContext.Walks.FirstOrDefaultAsync(p=> p.Id ==Id );
            if (result == null)
                return null;
            DbContext.Walks.Remove(result);
            await DbContext.SaveChangesAsync();

            return result;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null,  string? filterQuery = null,string? sortBy=null,bool isAscending=true,int pageNo = 1, int pageSize = 1000)
        {
            //var result= await DbContext.Walks.Include(p=> p.Region).Include(q=> q.Difficulty).ToListAsync();
            //var result = await DbContext.Walks.Include("Region").Include("Difficulty").ToListAsync();

            var walks = DbContext.Walks.Include("Region").Include("Difficulty").AsQueryable();
            //Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if(filterOn.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(p=> p.Name.Contains(filterQuery));
                }

            }
            //Sorting 
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
               
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Length) : walks.OrderByDescending(x => x.Length);
                }
            }
            //Paging
            int skipNoRecord = (pageNo - 1) * pageSize;
            walks = walks.Skip(skipNoRecord).Take(pageSize);

            return await walks.ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid Id)
        {
            return await DbContext.Walks.Include("Region").Include("Difficulty").FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<Walk?> UpdateAsync(Guid Id, Walk walk)
        {
            var walkDomain = await DbContext.Walks.FirstOrDefaultAsync(p => p.Id == Id);
            if (walkDomain == null)
                return null;
            walkDomain.DifficultyId =walk.DifficultyId;
            walkDomain.RegionId =walk.RegionId;
            walkDomain.Name =walk.Name;
            walkDomain.Length =walk.Length;

            //DbContext.Walks.Update(walk);
            await DbContext.SaveChangesAsync();
            return walkDomain;

        }
    }
}
