using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Nettside.Models;
using Nettside.Data;
using Microsoft.AspNetCore.Authorization;

namespace Nettside.Controllers
{

    /// <summary>
    /// respomsible for managing actions related to the homepage and error handling
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; // logger service for logging information and errors

        private readonly AppDbContext _context; // database context for accessing and saving data

        // static lists to temporarily store data in memory for positions and area changes
        private static List<PositionModel> positions = new List<PositionModel>();
        private static List<AreaChange> changes = new List<AreaChange>();



        /// <summary>
        /// constructor for injecting dependencies like the logger and database context
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="context"></param>
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

           
        // handles GET requests to display the homepage
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

     

        // displays the 'correctmap' view for submitting map corrections
        [HttpGet]
        public IActionResult CorrectMap()
        {
            return View();
        }


        /// <summary>
        /// handles post requests for map correction submissions
        /// </summary>
        /// <param name="model">the position model containing submitted correction data</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CorrectMap(PositionModel model)
        {
            if (ModelState.IsValid)
            {
                // add the valid position data to the in-memory list
                positions.Add(model);


                // redirect to the overview of corrections
                return View("CorrectionOverview", positions);
            }
            return View();
        }



       
        [HttpGet]
        public IActionResult CorrectionOverview()
        {
            return View(positions);
        }


        [Authorize(Roles = "Caseworker")]
        [HttpGet]
        public IActionResult RegisterAreaChange()
        {
            return View();
        }

        /// <summary>
        /// handles the submission of new area change data, validates it, and saves it to the database
        /// </summary>
        /// <param name="geoJson">the geojson string representing the area changw</param>
        /// <param name="description">description of the area change</param>
        /// <returns>redirects to the "arechangeoverview" view if successful, or returns a badrequest if data is invalid</returns>
        [Authorize(Roles = "Caseworker")]
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

                // Save to the database
                _context.GeoChange.Add(newGeoChange);
                _context.SaveChanges();

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
        public IActionResult AreaChangeOverview()
        {
            var changes_cb = _context.GeoChange.ToList();
            return View(changes_cb);
        }

    }
}
