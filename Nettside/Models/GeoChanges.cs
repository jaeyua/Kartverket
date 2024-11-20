using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Nettside.Models
{
    public class GeoChanges
    {
        [Key]
        public int Id { get; set; }
        public string? GeoJson { get; set; }
        public string? Description { get; set; }
    }
}