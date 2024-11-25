using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Nettside.Models
{
    /// <summary>
    /// Model representing geodata changes, including ID, geojson data, and description.
    /// </summary>
    public class GeoChanges
    {
        /// <summary>
        /// Gets or sets the unique identifier for the geodata change, marked as the primary key.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the GeoJSON structure representing the change in the area. Can be null.
        /// </summary>
        public string? GeoJson { get; set; }

        /// <summary>
        /// Gets or sets the description of the geodata change. Can be null.
        /// </summary>
        public string? Description { get; set; }
    }
}



