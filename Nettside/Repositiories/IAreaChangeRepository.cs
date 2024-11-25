using Nettside.Models;

namespace Nettside.Repositiories
{
    /// <summary>
    /// Interface for repository handling AreaChange operations.
    /// </summary>
    public interface IAreaChangeRepository
    {
        /// <summary>
        /// Gets all AreaChanges asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of AreaChanges.</returns>
        Task<IEnumerable<AreaChange>> GetAllAsync();

        /// <summary>
        /// Gets an AreaChange by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the AreaChange.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the AreaChange if found; otherwise, null.</returns>
        Task<AreaChange?> GetAsync(int id);

        /// <summary>
        /// Adds a new AreaChange asynchronously.
        /// </summary>
        /// <param name="areaChangeRepository">The AreaChange to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added AreaChange.</returns>
        Task<AreaChange> AddAsync(AreaChange areaChangeRepository);

        /// <summary>
        /// Updates an existing AreaChange asynchronously.
        /// </summary>
        /// <param name="areaChangeRepository">The AreaChange to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated AreaChange if found; otherwise, null.</returns>
        Task<AreaChange?> UpdateAsync(AreaChange areaChangeRepository);

        /// <summary>
        /// Deletes an AreaChange by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the AreaChange to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the deleted AreaChange if found; otherwise, null.</returns>
        Task<AreaChange?> DeleteAsync(int id);
    }
}




