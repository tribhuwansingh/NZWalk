using Microsoft.AspNetCore.Mvc;
using NZWalkAPICore8.Model.Domain;

namespace NZWalkAPICore8.Repositaries
{
    public interface IWalkRepositry
    {
        Task<List<Walk>> GetAllAsync( string? filterOn , string? filterQuery ,string? sortBy , bool isAscending =true ,int pageNo=1, int pageSize=1000);
        Task<Walk?> GetByIdAsync(Guid Id);
        Task<Walk> CreateAsync(Walk walk);
        Task<Walk?> UpdateAsync(Guid Id, Walk walk);
        Task<Walk?> DeleteByIdAsync(Guid Id);
    }
}
