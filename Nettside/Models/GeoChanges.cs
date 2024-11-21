using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Nettside.Models
{
    // Modell som representerer geodata for endringer, inkludert ID, geojson-data og beskrivelse.
    public class GeoChanges
    {
        // Unik identifikator for geodataendringen, merket som primærnøkkel.
        [Key]
        public int Id { get; set; }

        // GeoJSON-struktur som representerer endringen i området, kan være null.
        public string? GeoJson { get; set; }

        // Beskrivelse av geodataendringen, kan være null.
        public string? Description { get; set; }
    }
}
