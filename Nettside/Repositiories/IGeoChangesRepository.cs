using Nettside.Models;

namespace Nettside.Repositiories
{
    /// <summary>
    /// Interface for repository handling GeoChanges operations.
    /// </summary>
    public interface IGeoChangesRepository
    {
        /// <summary>
        /// Gets all GeoChanges asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of GeoChanges.</returns>
        Task<IEnumerable<GeoChanges>> GetAllAsync();

        /// <summary>
        /// Gets a GeoChange by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the GeoChange.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the GeoChange if found; otherwise, null.</returns>
        Task<GeoChanges?> GetAsync(int id);

        /// <summary>
        /// Adds a new GeoChange asynchronously.
        /// </summary>
        /// <param name="newGeoChange">The GeoChange to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added GeoChange.</returns>
        Task<GeoChanges> AddAsync(GeoChanges newGeoChange);

        /// <summary>
        /// Updates an existing GeoChange asynchronously.
        /// </summary>
        /// <param name="newGeoChange">The GeoChange to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated GeoChange if found; otherwise, null.</returns>
        Task<GeoChanges?> UpdateAsync(GeoChanges newGeoChange);

        /// <summary>
        /// Deletes a GeoChange by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the GeoChange to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the deleted GeoChange if found; otherwise, null.</returns>
        Task<GeoChanges?> DeleteAsync(int id);
    }
}