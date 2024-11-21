namespace Nettside.Models
{
    // Modell som representerer en feilmelding, inkludert informasjon om forespørselen som forårsaket feilen.
    public class ErrorViewModel
    {
        // Forespørselens ID, som kan være null hvis ikke tilgjengelig.
        public string? RequestId { get; set; }

        // Egenskap som bestemmer om forespørselens ID skal vises, basert på om den er tilgjengelig.
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
