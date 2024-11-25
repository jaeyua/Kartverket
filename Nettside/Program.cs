using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nettside.Data;
using Nettside.Models;
using Nettside.Repositiories;

var builder = WebApplication.CreateBuilder(args);

/// <summary>
/// Adds services to the container and configures the application.
/// </summary>
/// <param name="args">The command-line arguments.</param>

// Legger til tjenester for MVC-kontrollere og visninger.
builder.Services.AddControllersWithViews();

// Konfigurerer databasekonteksten for MariaDB ved hjelp av en tilkoblingsstreng fra konfigurasjonsfilen.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MariaDbConnection"),
    new MySqlServerVersion(new Version(10, 5, 9))));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IGeoChangesRepository, GeoChangesRepository>();
builder.Services.AddScoped<IAreaChangeRepository, AreaChangeRepository>();

// Konfigurerer Identity-tjenester for autentisering og autorisasjon, inkludert passord- og p�loggingskrav.
builder.Services.AddIdentity<Users, IdentityRole>(options =>
{
    // Angir passordpolicy for brukere.
    options.Password.RequireNonAlphanumeric = false; // Ikke-alfanumeriske tegn kreves ikke.
    options.Password.RequiredLength = 8;            // Minimum passordlengde er 8 tegn.
    options.Password.RequireUppercase = false;      // Store bokstaver kreves ikke.
    options.User.RequireUniqueEmail = true;         // E-postadresser m� v�re unike.

    // Angir p�loggings- og kontobekreftelseskrav.
    options.SignIn.RequireConfirmedAccount = false; // Kontoen trenger ikke � bekreftes.
    options.SignIn.RequireConfirmedEmail = false;   // E-postbekreftelse kreves ikke.
    options.SignIn.RequireConfirmedPhoneNumber = false; // Bekreftelse av telefonnummer kreves ikke.
})
    .AddEntityFrameworkStores<AppDbContext>() // Bruker `AppDbContext` til � lagre Identity-data.
    .AddDefaultTokenProviders();              // Legger til standard token-providere for funksjoner som passordgjenoppretting.

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        // Log the error (you can use a logging framework here)
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
        // Optionally, rethrow or handle the exception as needed
    }
}

// Konfigurerer HTTP-foresp�rselspipelinen.
if (!app.Environment.IsDevelopment())
{
    // Bruker en feilh�ndteringsside for produksjon.
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Aktiverer HTTP Strict Transport Security (HSTS).
}

app.UseHttpsRedirection(); // Omdirigerer HTTP-foresp�rsler til HTTPS.
app.UseStaticFiles();      // Gj�r statiske filer tilgjengelige for klienten.

app.UseRouting();          // Aktiverer ruting av foresp�rsler.

app.UseAuthentication();   // Aktiverer autentiseringstjenester.
app.UseAuthorization();    // Aktiverer autorisasjonstjenester.

// Konfigurerer standard rute for MVC.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Ruter til `HomeController` og `Index`-handling som standard.

app.Run(); // Starter applikasjonen.





