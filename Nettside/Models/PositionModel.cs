namespace Nettside.Models
{
    // Modell som representerer en posisjon med breddegrad, lengdegrad og en beskrivelse.
    public class PositionModel
    {
        // Breddegrad for posisjonen, lagret som en streng.
        public string Latitude { get; set; }

        // Lengdegrad for posisjonen, lagret som en streng.
        public string Longitude { get; set; }

        // Beskrivelse som gir mer informasjon om posisjonen.
        public string Description { get; set; }
    }
}
