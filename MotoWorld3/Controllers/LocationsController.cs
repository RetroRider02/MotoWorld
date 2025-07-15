using Microsoft.AspNetCore.Mvc;
using MotoWorld3.Data;
using MotoWorld3.Models;

namespace MotoWorld3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly GeocodingService _geocodingService;

        public LocationsController(GeocodingService geocodingService)
        {
            _geocodingService = geocodingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCoordinate(string street, string cityName, int postalcode, string houseNumber)
        {
            Place place = new Place { CityName = cityName, ZipCode = postalcode, Street = street, HouseNumber = houseNumber };
            var value = await _geocodingService.GetCoordinatesAsync(place.Street, place.CityName, place.ZipCode, "Hungary");

            var results = new List<object>
            {
                new { place.CityName, place.Street, place.HouseNumber, value.lat, value.lon }
            };
           
            return Ok(results);
        }
    }
}
