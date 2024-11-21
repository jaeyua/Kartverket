namespace Nettside.Models
{
    // Modell som representerer en feilmelding, inkludert informasjon om foresp�rselen som for�rsaket feilen.
    public class ErrorViewModel
    {
        // Foresp�rselens ID, som kan v�re null hvis ikke tilgjengelig.
        public string? RequestId { get; set; }

        // Egenskap som bestemmer om foresp�rselens ID skal vises, basert p� om den er tilgjengelig.
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
