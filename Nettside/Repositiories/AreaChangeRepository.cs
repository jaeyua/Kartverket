using Microsoft.EntityFrameworkCore;
using Nettside.Data;
using Nettside.Models;

namespace Nettside.Repositiories
{
    public class AreaChangeRepository : IAreaChangeRepository
    {
        private readonly AppDbContext appDbContext;

        public AreaChangeRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<AreaChange> AddAsync(AreaChange areaChangeRepository)
        {
            await appDbContext.AreaChanges.AddAsync(areaChangeRepository);
            await appDbContext.SaveChangesAsync();
            return areaChangeRepository;
        }

        public async Task<AreaChange?> DeleteAsync(int id)
        {
            var areaChangeRepository = await appDbContext.AreaChanges.FindAsync(id);
            if (areaChangeRepository == null)
            {
                return null;
            }

            appDbContext.AreaChanges.Remove(areaChangeRepository);
            await appDbContext.SaveChangesAsync();
            return areaChangeRepository;
        }

        public async Task<IEnumerable<AreaChange>> GetAllAsync()
        {
            return await appDbContext.AreaChanges.ToListAsync();
        }

        public async Task<AreaChange?> GetAsync(int id)
        {
            return await appDbContext.AreaChanges.FindAsync(id);
        }

        public async Task<AreaChange?> UpdateAsync(AreaChange areaChangeRepository)
        {
            var existingAreaChange = await appDbContext.AreaChanges.FindAsync(areaChangeRepository.Id);
            if (existingAreaChange == null)
            {
                return null;
            }

            appDbContext.Entry(existingAreaChange).CurrentValues.SetValues(areaChangeRepository);
            await appDbContext.SaveChangesAsync();
            return existingAreaChange;
        }
    }
}
