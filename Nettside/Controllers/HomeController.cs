using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Nettside.Models;
using Nettside.Data;
using Microsoft.AspNetCore.Authorization;
using Nettside.Repositories;

namespace Nettside.Controllers
{

    /// <summary>
    /// respomsible for managing actions related to the homepage, including displaying views,
    /// handling error management and processing area change registrations.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; // logger service for logging information and errors

        private readonly AppDbContext _context; // database context for accessing and saving data

        // static lists to temporarily store data in memory for positions and area changes
        private static List<PositionModel> positions = new List<PositionModel>();
        private static List<AreaChange> changes = new List<AreaChange>();

        private readonly IGeoChangesRepository geoChangesRepository;
        private readonly IAreaChangeRepository areaChangeRepository;


        /// <summary>
        /// constructor for injecting dependencies like the logger and database context
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="context"></param>
        public HomeController(IGeoChangesRepository geoChangesRepository, IAreaChangeRepository areaChangeRepository)
        {
            this.geoChangesRepository = geoChangesRepository;
            this.areaChangeRepository = areaChangeRepository;
        }

           
        // handles GET requests to display the homepage
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

     


        [Authorize(Roles = "Caseworker")]
        [HttpGet]
        public  IActionResult RegisterAreaChange()
        {
            return View();
        }

        /// <summary>
        /// handles the submission of new area change data, validates it, and saves it to the database
        /// </summary>
        /// <param name="geoJson">the geojson string representing the area change</param>
        /// <param name="description">description of the area change</param>
        /// <returns>redirects to the "arechangeoverview" view if successful, or returns a badrequest if data is invalid</returns>
        [Authorize(Roles = "Caseworker, PrivateUser")]
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

                // Save to the database
               await areaChangeRepository.AddAsync(newAreaChange);

                // Redirect to the overview of changes
                return RedirectToAction("AreaChangeOverview");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}, Inner Exception: {ex.InnerException?.Message}");
                throw;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Display the overview of registered changes fetched from the database
        [Authorize(Roles = "Caseworker, PrivateUser")]
        [HttpGet]
        public async Task<ActionResult> AreaChangeOverview()
        {
            var areaChanges = await areaChangeRepository.GetAllAsync();
            return View(areaChanges);
        }

    }
}
