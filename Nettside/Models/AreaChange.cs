using System.ComponentModel.DataAnnotations;

namespace Nettside.Models
{
    // Modell for en områdeendring, som inneholder informasjon om geojson-data og beskrivelse.
    public class AreaChange
    {
        // Unik identifikator for områdeendringen, merket som primærnøkkel.
        [Key]
        public int Id { get; set; }

        // GeoJSON-struktur som representerer endringen i området.
        public string GeoJson { get; set; }

        // Beskrivelse av områdeendringen.
        public string Description { get; set; }
    }
}
