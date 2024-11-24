using Nettside.Models;

namespace Nettside.Repositiories
{
    public interface IGeoChangesRepository
    {
        Task<IEnumerable<GeoChanges>> GetAllAsync();

        Task<GeoChanges?> GetAsync(int id);

        Task<GeoChanges> AddAsync(GeoChanges newGeoChange);

        Task<GeoChanges?> UpdateAsync(GeoChanges newGeoChange);

        Task<GeoChanges?> DeleteAsync(int id);

            
    }
}
