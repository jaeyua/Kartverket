using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Nettside.Models;
using Nettside.Data;

namespace Nettside.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private static List<PositionModel> positions = new List<PositionModel>();
        private static List<AreaChange> changes = new List<AreaChange>();

        // Konstrukt�r som initialiserer loggeren og databasen.
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Vist hovedsiden (Index).
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Vist kartkorreksjonssiden (CorrectMap) for GET-foresp�rsler.
        [HttpGet]
        public IActionResult CorrectMap()
        {
            return View();
        }

        // H�ndter POST-foresp�rsel for kartkorreksjon og lagre posisjonsdata hvis modellen er gyldig.
        [HttpPost]
        public IActionResult CorrectMap(PositionModel model)
        {
            if (ModelState.IsValid)
            {
                positions.Add(model);
                return View("CorrectionOverview", positions);
            }
            return View();
        }

        // Vist oversikt over lagrede posisjoner (CorrectionOverview).
        [HttpGet]
        public IActionResult CorrectionOverview()
        {
            return View(positions);
        }

        // Vist siden for registrering av omr�deendring (RegisterAreaChange).
        [HttpGet]
        public IActionResult RegisterAreaChange()
        {
            return View();
        }

        // H�ndter POST-foresp�rsel for � registrere en omr�deendring i databasen.
        [HttpPost]
        public IActionResult RegisterAreaChange(string geoJson, string description)
        {
            try
            {
                if (string.IsNullOrEmpty(geoJson) || string.IsNullOrEmpty(description))
                {
                    return BadRequest("Invalid data.");
                }

                var newGeoChange = new GeoChanges
                {
                    GeoJson = geoJson,
                    Description = description
                };

                _context.GeoChange.Add(newGeoChange);
                _context.SaveChanges();

                return RedirectToAction("AreaChangeOverview");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}, Inner Exception: {ex.InnerException?.Message}");
                throw;
            }
        }

        // Vist personvernspolitikk-siden (Privacy).
        public IActionResult Privacy()
        {
            return View();
        }

        // Hent oversikt over omr�deendringer fra databasen og vis den.
        [HttpGet]
        public IActionResult AreaChangeOverview()
        {
            var changes_cb = _context.GeoChange.ToList();
            return View(changes_cb);
        }
    }
}
