using System.ComponentModel.DataAnnotations;

namespace Nettside.Models
{
    /// <summary>
    /// Model for an area change, containing information about geojson data and description.
    /// </summary>
    public class AreaChange
    {
        /// <summary>
        /// Gets or sets the unique identifier for the area change, marked as the primary key.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the GeoJSON structure representing the change in the area.
        /// </summary>
        public string GeoJson { get; set; }

        /// <summary>
        /// Gets or sets the description of the area change.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the user type associated with the area change.
        /// </summary>
        public string UserType { get; set; } // Property to identify the user type
    }
}



