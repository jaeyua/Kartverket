namespace Nettside.Models
{
    /// <summary>
    /// Model representing a position with latitude, longitude, and a description.
    /// </summary>
    public class PositionModel
    {
        /// <summary>
        /// Gets or sets the latitude of the position, stored as a string.
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the position, stored as a string.
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// Gets or sets the description providing more information about the position.
        /// </summary>
        public string Description { get; set; }
    }
}



