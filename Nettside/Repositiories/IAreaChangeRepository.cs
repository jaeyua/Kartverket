using Nettside.Models;

namespace Nettside.Repositiories
{
    public interface IAreaChangeRepository
    {
        Task<IEnumerable<AreaChange>> GetAllAsync();

        Task<AreaChange?> GetAsync(int id);

        Task<AreaChange> AddAsync(AreaChange areaChangeRepository);

        Task<AreaChange?> UpdateAsync(AreaChange areaChangeRepository);

        Task<AreaChange?> DeleteAsync(int id);

            
    }
}
