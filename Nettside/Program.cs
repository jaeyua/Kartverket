using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nettside.Data;
using Nettside.Models;

var builder = WebApplication.CreateBuilder(args);

// Legger til tjenester for MVC-kontrollere og visninger.
builder.Services.AddControllersWithViews();

// Konfigurerer databasekonteksten for MariaDB ved hjelp av en tilkoblingsstreng fra konfigurasjonsfilen.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MariaDbConnection"),
    new MySqlServerVersion(new Version(10, 5, 9))));

// Konfigurerer Identity-tjenester for autentisering og autorisasjon, inkludert passord- og påloggingskrav.
builder.Services.AddIdentity<Users, IdentityRole>(options =>
{
    // Angir passordpolicy for brukere.
    options.Password.RequireNonAlphanumeric = false; // Ikke-alfanumeriske tegn kreves ikke.
    options.Password.RequiredLength = 8;            // Minimum passordlengde er 8 tegn.
    options.Password.RequireUppercase = false;      // Store bokstaver kreves ikke.
    options.User.RequireUniqueEmail = true;         // E-postadresser må være unike.

    // Angir påloggings- og kontobekreftelseskrav.
    options.SignIn.RequireConfirmedAccount = false; // Kontoen trenger ikke å bekreftes.
    options.SignIn.RequireConfirmedEmail = false;   // E-postbekreftelse kreves ikke.
    options.SignIn.RequireConfirmedPhoneNumber = false; // Bekreftelse av telefonnummer kreves ikke.
})
    .AddEntityFrameworkStores<AppDbContext>() // Bruker `AppDbContext` til å lagre Identity-data.
    .AddDefaultTokenProviders();              // Legger til standard token-providere for funksjoner som passordgjenoppretting.

var app = builder.Build();

// Konfigurerer HTTP-forespørselspipelinen.
if (!app.Environment.IsDevelopment())
{
    // Bruker en feilhåndteringsside for produksjon.
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Aktiverer HTTP Strict Transport Security (HSTS).
}

app.UseHttpsRedirection(); // Omdirigerer HTTP-forespørsler til HTTPS.
app.UseStaticFiles();      // Gjør statiske filer tilgjengelige for klienten.

app.UseRouting();          // Aktiverer ruting av forespørsler.

app.UseAuthentication();   // Aktiverer autentiseringstjenester.
app.UseAuthorization();    // Aktiverer autorisasjonstjenester.

// Konfigurerer standard rute for MVC.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Ruter til `HomeController` og `Index`-handling som standard.

app.Run(); // Starter applikasjonen.
