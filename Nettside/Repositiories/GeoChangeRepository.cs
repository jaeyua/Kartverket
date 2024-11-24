using Microsoft.EntityFrameworkCore;
using Nettside.Data;
using Nettside.Models;

namespace Nettside.Repositiories
{
    public class GeoChangesRepository : IGeoChangesRepository
    {
        private readonly AppDbContext appDbContext;

        public GeoChangesRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<GeoChanges> AddAsync(GeoChanges newGeoChange)
        {
            await appDbContext.GeoChange.AddAsync(newGeoChange);
            await appDbContext.SaveChangesAsync();
            return newGeoChange;
        }

        public async Task<GeoChanges?> DeleteAsync(int id)
        {
            var geoChange = await appDbContext.GeoChange.FindAsync(id);
            if (geoChange == null)
            {
                return null;
            }

            appDbContext.GeoChange.Remove(geoChange);
            await appDbContext.SaveChangesAsync();
            return geoChange;
        }

        public async Task<IEnumerable<GeoChanges>> GetAllAsync()
        {
            return await appDbContext.GeoChange.ToListAsync();
        }

        public async Task<GeoChanges?> GetAsync(int id)
        {
            return await appDbContext.GeoChange.FindAsync(id);
        }

        public async Task<GeoChanges?> UpdateAsync(GeoChanges newGeoChange)
        {
            var existingGeoChange = await appDbContext.GeoChange.FindAsync(newGeoChange.Id);
            if (existingGeoChange == null)
            {
                return null;
            }

            appDbContext.Entry(existingGeoChange).CurrentValues.SetValues(newGeoChange);
            await appDbContext.SaveChangesAsync();
            return existingGeoChange;
        }
    }
}
