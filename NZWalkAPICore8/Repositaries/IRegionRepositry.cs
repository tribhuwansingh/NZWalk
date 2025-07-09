using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Identity.Client;
using NZWalkAPICore8.Model.Domain;


namespace NZWalkAPICore8.Repositaries
{
    public interface IRegionRepositry
    {
         Task<List<Region>> GetAllAsync();
        Task<Region?> GetByIdAsync(Guid Id); 
        Task<Region> CreateAsync(Region region);
        Task<Region?> UpdateAsync(Guid Id,Region region);
        Task<Region?> DeleteByIdAsync(Guid Id);

    }
}
