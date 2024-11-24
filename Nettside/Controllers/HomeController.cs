using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Nettside.Models;
using Nettside.Repositiories;

namespace Nettside.Controllers
{
    public class HomeController : Controller
    {
        private static List<PositionModel> positions = new List<PositionModel>();
        private readonly IGeoChangesRepository geoChangesRepository;
        private readonly IAreaChangeRepository areaChangeRepository;

        // Constructor that initializes the repositories.
        public HomeController(IGeoChangesRepository geoChangesRepository, IAreaChangeRepository areaChangeRepository)
        {
            this.geoChangesRepository = geoChangesRepository;
            this.areaChangeRepository = areaChangeRepository;
        }

        // Display the main page (Index).
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Display the map correction page (CorrectMap) for GET requests.
        [HttpGet]
        public IActionResult CorrectMap()
        {
            return View();
        }

        // Handle POST request for map correction and save position data if the model is valid.
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

        // Display the overview of saved positions (CorrectionOverview).
        [HttpGet]
        public IActionResult CorrectionOverview()
        {
            return View(positions);
        }

        // Display the page for registering an area change (RegisterAreaChange).
        [HttpGet]
        public IActionResult RegisterAreaChange()
        {
            return View();
        }

        // Handle POST request to register an area change in the database.
        [HttpPost]
        public async Task<IActionResult> RegisterAreaChange(string geoJson, string description)
        {
            try
            {
                if (string.IsNullOrEmpty(geoJson) || string.IsNullOrEmpty(description))
                {
                    return BadRequest("Invalid data.");
                }

                var newAreaChange = new AreaChange
                {
                    GeoJson = geoJson,
                    Description = description
                };

                await areaChangeRepository.AddAsync(newAreaChange);

                return RedirectToAction("AreaChangeOverview");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}, Inner Exception: {ex.InnerException?.Message}");
                throw;
            }
        }

        // Display the privacy policy page (Privacy).
        public IActionResult Privacy()
        {
            return View();
        }

        // Fetch the overview of area changes from the database and display it.
        [HttpGet]
        public async Task<IActionResult> AreaChangeOverview()
        {
            var areaChanges = await areaChangeRepository.GetAllAsync();
            return View(areaChanges);
        }
    }
}
