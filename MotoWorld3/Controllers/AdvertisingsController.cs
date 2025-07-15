using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoWorld3.Data;
using MotoWorld3.Models;
using MotoWorld3.Utilities;
using MotoWorld3.ViewModels.Advertisings;
using Microsoft.AspNetCore.Identity;
using MotoWorld3.ViewModels;
using System.Diagnostics;

namespace MotoWorld3.Controllers
{
    public class AdvertisingsController : Controller
    {
        private readonly ILogger<AdvertisingsController> _logger;

        private readonly ApplicationDbContext _context;

        private readonly UserManager<IdentityUser> _userManager;

        public AdvertisingsController(ILogger<AdvertisingsController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Results(string? brand, int? min_price, int? max_price, int? min_year, int? max_year, int? min_km, int? max_km, int? min_cylinder_capacity, int? max_cylinder_capacity, int? pageNumber)
        {
            var advertiesments = _context.MotorcycleAdvertisings.Include(x => x.Motorcycle).Include(x => x.Motorcycle.MotorcycleType).Include(x => x.Advertising).Include(x => x.Advertising.Place).AsQueryable();

            if (!string.IsNullOrEmpty(brand))
            {
                ViewBag.Brand = brand;
                advertiesments = advertiesments.Where(x => x.Motorcycle.MotorcycleType.Manufacturer == brand);
            }

            if (min_price.HasValue)
            {
                ViewBag.MinPrice = min_price.Value;
                advertiesments = advertiesments.Where(x => x.Advertising.Price >= min_price.Value);
            }

            if (max_price.HasValue)
            {
                ViewBag.MaxPrice = max_price.Value;
                advertiesments = advertiesments.Where(x => x.Advertising.Price <= max_price.Value);
            }

            if (min_year.HasValue)
            {
                ViewBag.MinYear = min_year.Value;
                advertiesments = advertiesments.Where(x => x.Motorcycle.YearOfManufacture >= min_year.Value);
            }

            if (max_year.HasValue)
            {
                ViewBag.MaxYear = max_year.Value;
                advertiesments = advertiesments.Where(x => x.Motorcycle.YearOfManufacture <= max_year.Value);
            }

            if (min_km.HasValue)
            {
                ViewBag.MinMileage = min_km.Value;
                advertiesments = advertiesments.Where(x => x.Motorcycle.Mileage >= min_km.Value);
            }

            if (max_km.HasValue)
            {
                ViewBag.MaxMileage = max_km.Value;
                advertiesments = advertiesments.Where(x => x.Motorcycle.Mileage <= max_km.Value);
            }

            if (min_cylinder_capacity.HasValue)
            {
                ViewBag.MinCylinderCapacity = min_cylinder_capacity.Value;
                advertiesments = advertiesments.Where(x => x.Motorcycle.CylinderCapacity >= min_cylinder_capacity.Value);
            }

            if (max_cylinder_capacity.HasValue)
            {
                ViewBag.MaxCylinderCapacity = max_cylinder_capacity.Value;
                advertiesments = advertiesments.Where(x => x.Motorcycle.CylinderCapacity <= max_cylinder_capacity.Value);
            }

            ViewBag.Manufacturers = _context.MotorcycleTypes.OrderBy(x => x.Manufacturer).Select(x => x.Manufacturer).Distinct().ToList();

            var advertisingIds = new HashSet<int>(advertiesments.Select(a => a.AdvertisingID));
            var images = await _context.Pictures.Include(x => x.Advertising).Where(p => advertisingIds.Contains(p.Advertising.Id)).ToListAsync();

            return View(new ResultViewModel(await PaginatedList<MotorcycleAdvertising>.CreateAsync(advertiesments.AsNoTracking(), pageNumber ?? 1, 10), images));
        }

        // GET: Advertisings/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var motorcycleAdvertising = await _context.MotorcycleAdvertisings
                .Include(x => x.Motorcycle)
                .Include(x => x.Motorcycle.MotorcycleType)
                .Include(x => x.Advertising)
                .Include(x => x.Advertising.IdentityUser)
                .Include(x => x.Advertising.Place)
                .FirstOrDefaultAsync(x => x.AdvertisingID == id);

            if (motorcycleAdvertising == null)
            {
                return NotFound();
            }

            var images = await _context.Pictures
                .Include(x => x.Advertising)
                .Where(x => x.AdvertisingID == motorcycleAdvertising.AdvertisingID)
                .ToListAsync();

            return View(new DetailsViewModel(motorcycleAdvertising, images));
        }

        // GET: Advertisings/Create
        [HttpGet, Authorize]
        public async Task<IActionResult> Create()
        {
            var identityUser = await _userManager.FindByNameAsync(User.Identity?.Name);

            if (identityUser != null)
            {
                ViewBag.Email = identityUser.Email;
                ViewBag.Tel = identityUser.PhoneNumber;
            }

            ViewBag.Category = new List<string>
            {
                "Adventure", "Chopper", "Cruiser", "Egyedi",
                "Enduro", "Modern Klasszikus", "Naked", "Robogó",
                "Sportmotor", "Sport-Túra", "Túra", "Vintage"
            };

            var brandsAndModels = _context.MotorcycleTypes.ToList();

            ViewBag.Manufacturers = brandsAndModels.Select(x => x.Manufacturer).Distinct().ToList();
            ViewBag.Models = brandsAndModels.Select(x => x.Model).ToList();

            ViewBag.Condition = new List<string> { "Újszerű", "Normál", "Sérült", "Enyhén sérült" };
            ViewBag.CylinderArrangement = new List<string> { "Soros", "Boxer", "V-2", "V-4" };
            ViewBag.NumberOfCylinders = new List<int> { 1, 2, 4, 6 };
            ViewBag.WorkSchedule = new List<int> { 2, 4 };
            ViewBag.ValvesPerCylinders = new List<int> { 2, 4 };
            ViewBag.Mixture = new List<string> { "Injektor", "Karburátor" };
            ViewBag.Cooling = new List<string> { "Víz", "Lég", "Lég-Olaj" };
            ViewBag.DriveType = new List<string> { "Szíj", "Lánc", "Kardán" };
            ViewBag.Transmission = new List<string> { "Manuális", "Automata" };

            return View();
        }

        [HttpGet]
        public IActionResult GetModels(string manufacturer)
        {
            return Json(_context.MotorcycleTypes.Where(x => x.Manufacturer == manufacturer).Select(x => x.Model).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken, Authorize]
        public async Task<IActionResult> Create(CreateViewModel model, List<IFormFile> images)
        {
            var DB_User = await _userManager.FindByIdAsync(model.Advertising.IdentityUserID);
            var DB_MotorcycleType = _context.MotorcycleTypes.FirstOrDefault(x => x.Manufacturer == model.MotorcycleType.Manufacturer && x.Model == model.MotorcycleType.Model);

            if (DB_User == null || DB_MotorcycleType == null)
            {
                return NotFound();
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _userManager.SetPhoneNumberAsync(DB_User, model.PhoneNumber);

                // Save motorcycle
                var motorcycle = new Motorcycle
                {
                    MotorcycleType = DB_MotorcycleType,
                    Category = model.Motorcycle.Category,
                    YearOfManufacture = model.Motorcycle.YearOfManufacture,
                    Color = model.Motorcycle.Color,
                    Condition = model.Motorcycle.Condition,
                    Mileage = model.Motorcycle.Mileage,
                    Weight = model.Motorcycle.Weight,
                    Fuel = "Benzin",
                    Power = model.Motorcycle.Power,
                    WorkSchedule = model.Motorcycle.WorkSchedule,
                    NumberOfCylinders = model.Motorcycle.NumberOfCylinders,
                    CylinderCapacity = model.Motorcycle.CylinderCapacity,
                    ValvesPerCylinders = model.Motorcycle.ValvesPerCylinders,
                    CylinderArrangement = model.Motorcycle.CylinderArrangement,
                    Mixture = model.Motorcycle.Mixture,
                    Cooling = model.Motorcycle.Cooling,
                    DriveType = model.Motorcycle.DriveType,
                    Transmission = model.Motorcycle.Transmission
                };
                await _context.Motorcycles.AddAsync(motorcycle);

                // Save place
                var place = new Place
                {
                    ZipCode = model.Place.ZipCode,
                    CityName = model.Place.CityName,
                    Street = model.Place.Street,
                    HouseNumber = model.Place.HouseNumber
                };
                await _context.Places.AddAsync(place);

                // Save advertising
                var advertising = new Advertising
                {
                    IdentityUser = DB_User,
                    Place = place,
                    Price = model.Advertising.Price,
                    Description = model.Advertising.Description,
                    Created = DateTime.UtcNow,
                    LastModification = DateTime.UtcNow,
                    Frozen = false,
                };
                await _context.Advertisings.AddAsync(advertising);

                // Save pictures
                foreach (var image in images.OrderBy(x => x.Name).Take(10))
                {
                    if (image.Length == 0 || !image.ContentType.StartsWith("image/")) continue;

                    using var memoryStream = new MemoryStream();
                    await image.CopyToAsync(memoryStream);

                    await _context.Pictures.AddAsync(new Picture
                    {
                        Advertising = advertising,
                        FileName = image.FileName,
                        ContentType = image.ContentType,
                        Data = memoryStream.ToArray(),
                        UploadDate = DateTime.UtcNow
                    });
                }

                // Save motorcycleAdvertising
                await _context.MotorcycleAdvertisings.AddAsync(new MotorcycleAdvertising
                {
                    Advertising = advertising,
                    Motorcycle = motorcycle,
                });

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                _logger.LogError("Failed to add advertising: " + ex.Message);
                return View(model);
            }
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> DeletePicture(int id)
        {
            var picture = await _context.Pictures.FindAsync(id);
            if (picture == null)
            {
                _logger.LogError("Picture not found for ID: " + id);
                return NotFound();
            }

            try
            {
                _context.Pictures.Remove(picture);
                await _context.SaveChangesAsync();

                return RedirectToAction("Edit", new { id = picture.AdvertisingID });
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong while deleting the picture: " + ex.Message);
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> Icing(int id, bool method)
        {
            var motorcycleAdvertising = await _context.MotorcycleAdvertisings
            .Include(x => x.Motorcycle)
            .Include(x => x.Motorcycle.MotorcycleType)
            .Include(x => x.Advertising)
            .Include(x => x.Advertising.Place)
            .Include(x => x.Advertising.IdentityUser)
            .FirstOrDefaultAsync(x => x.AdvertisingID == id);

            if (motorcycleAdvertising == null)
            {
                _logger.LogError("Motorcycle advertising not found for ID: " + id);
                return NotFound();
            }

            try
            {
                motorcycleAdvertising.Advertising.Frozen = method;
                _context.Advertisings.Update(motorcycleAdvertising.Advertising);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while icing the motorcycle advertising: " + ex.Message);
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        // GET: Advertisings/Edit/5
        [HttpGet, Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var motorcycleAdvertising = await _context.MotorcycleAdvertisings
            .Include(x => x.Motorcycle)
            .Include(x => x.Motorcycle.MotorcycleType)
            .Include(x => x.Advertising)
            .Include(x => x.Advertising.Place)
            .Include(x => x.Advertising.IdentityUser)
            .FirstOrDefaultAsync(x => x.AdvertisingID == id);

            if (motorcycleAdvertising == null)
            {
                _logger.LogError("MotorcycleAdvertising not found for ID: " + id);
                return NotFound();
            }

            var pictures = await _context.Pictures.Where(x => x.AdvertisingID == motorcycleAdvertising.AdvertisingID).ToListAsync();
            var user = await _userManager.FindByIdAsync(motorcycleAdvertising.Advertising.IdentityUserID);

            if (user == null)
            {
                _logger.LogError("User is not found for Advertising ID: " + id);
                return NotFound();
            }

            EditViewModel viewModel = new EditViewModel
            {
                MotorcycleType = motorcycleAdvertising.Motorcycle.MotorcycleType,
                Motorcycle = motorcycleAdvertising.Motorcycle,
                Advertising = motorcycleAdvertising.Advertising,
                Pictures = pictures,
                Place = motorcycleAdvertising.Advertising.Place
            };

            ViewBag.Email = user.Email;
            ViewBag.Tel = user.PhoneNumber;

            ViewBag.Category = new List<string>
            {
                "Adventure", "Chopper", "Cruiser", "Egyedi",
                "Enduro", "Modern klasszikus", "Naked", "Robogó",
                "Sportmotor", "Sport-Túra", "Túra", "Vintage"
            };

            var brandsAndModels = await _context.MotorcycleTypes.ToListAsync();

            ViewBag.Manufacturers = brandsAndModels.Select(x => x.Manufacturer).Distinct().ToList();
            ViewBag.Models = brandsAndModels.Select(x => x.Model).ToList();

            ViewBag.Condition = new List<string> { "Újszerű", "Normál", "Sérült", "Enyhén sérült" };
            ViewBag.CylinderArrangement = new List<string> { "Soros", "Boxer", "V-2", "V-4" };
            ViewBag.NumberOfCylinders = new List<int> { 1, 2, 4, 6 };
            ViewBag.WorkSchedule = new List<int> { 2, 4 };
            ViewBag.ValvesPerCylinders = new List<int> { 2, 4 };
            ViewBag.Mixture = new List<string> { "Injektor", "Karburátor" };
            ViewBag.Cooling = new List<string> { "Víz", "Lég", "Lég-Olaj" };
            ViewBag.DriveType = new List<string> { "Szíj", "Lánc", "Kardán" };
            ViewBag.Transmission = new List<string> { "Manuális", "Automata" };

            return View(viewModel);
        }

        // POST: Advertisings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditViewModel model, List<IFormFile> images)
        {
            var motorcycleAdvertising = _context.MotorcycleAdvertisings
                .Include(x => x.Motorcycle)
                .Include(x => x.Motorcycle.MotorcycleType)
                .Include(x => x.Advertising)
                .Include(x => x.Advertising.Place)
                .Include(x => x.Advertising.IdentityUser)
                .FirstOrDefault(x => x.AdvertisingID == model.Advertising.Id);

            if (motorcycleAdvertising == null)
            {
                _logger.LogError("Motorcycle advertising not found for ID: " + model.Advertising.Id);
                return NotFound();
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var DB_User = await _userManager.FindByIdAsync(motorcycleAdvertising.Advertising.IdentityUserID);
                await _userManager.SetPhoneNumberAsync(DB_User, model.PhoneNumber);

                // Save pictures
                foreach (var image in images.OrderBy(x => x.Name).Take(10))
                {
                    if (image.Length == 0 || !image.ContentType.StartsWith("image/")) continue;

                    using var memoryStream = new MemoryStream();
                    await image.CopyToAsync(memoryStream);

                    await _context.Pictures.AddAsync(new Picture
                    {
                        Advertising = motorcycleAdvertising.Advertising,
                        FileName = image.FileName,
                        ContentType = image.ContentType,
                        Data = memoryStream.ToArray(),
                        UploadDate = DateTime.UtcNow
                    });
                }

                // Update MotorcycleType
                var motorcycleTypeFromDb = _context.MotorcycleTypes.FirstOrDefault(x => x.Id == motorcycleAdvertising.Motorcycle.MotorcycleTypeID);
                if (motorcycleTypeFromDb != null)
                {
                    motorcycleTypeFromDb.Manufacturer = model.MotorcycleType.Manufacturer;
                    motorcycleTypeFromDb.Model = model.MotorcycleType.Model;
                    _context.MotorcycleTypes.Update(motorcycleTypeFromDb);
                }

                // Update Motorcycle
                var motorcycleFromDb = _context.Motorcycles.FirstOrDefault(x => x.Id == motorcycleAdvertising.MotorcycleID);
                if (motorcycleFromDb != null)
                {
                    motorcycleFromDb.Category = model.Motorcycle.Category;
                    motorcycleFromDb.YearOfManufacture = model.Motorcycle.YearOfManufacture;
                    motorcycleFromDb.Color = model.Motorcycle.Color;
                    motorcycleFromDb.Condition = model.Motorcycle.Condition;
                    motorcycleFromDb.Mileage = model.Motorcycle.Mileage;
                    motorcycleFromDb.Weight = model.Motorcycle.Weight;
                    motorcycleFromDb.Fuel = "Benzin";
                    motorcycleFromDb.Power = model.Motorcycle.Power;
                    motorcycleFromDb.WorkSchedule = model.Motorcycle.WorkSchedule;
                    motorcycleFromDb.NumberOfCylinders = model.Motorcycle.NumberOfCylinders;
                    motorcycleFromDb.CylinderCapacity = model.Motorcycle.CylinderCapacity;
                    motorcycleFromDb.ValvesPerCylinders = model.Motorcycle.ValvesPerCylinders;
                    motorcycleFromDb.CylinderArrangement = model.Motorcycle.CylinderArrangement;
                    motorcycleFromDb.Mixture = model.Motorcycle.Mixture;
                    motorcycleFromDb.Cooling = model.Motorcycle.Cooling;
                    motorcycleFromDb.DriveType = model.Motorcycle.DriveType;
                    motorcycleFromDb.Transmission = model.Motorcycle.Transmission;
                    _context.Motorcycles.Update(motorcycleFromDb);
                }

                // Update Place
                var placeFromDb = _context.Places.FirstOrDefault(x => x.Id == motorcycleAdvertising.Advertising.PlaceID);
                if (placeFromDb != null)
                {
                    placeFromDb.ZipCode = model.Place.ZipCode;
                    placeFromDb.CityName = model.Place.CityName;
                    placeFromDb.Street = model.Place.Street;
                    placeFromDb.HouseNumber = model.Place.HouseNumber;
                    _context.Places.Update(placeFromDb);
                }

                // Update Advertising
                var adFromDb = _context.Advertisings.FirstOrDefault(x => x.Id == motorcycleAdvertising.AdvertisingID);
                if (adFromDb != null)
                {
                    adFromDb.IdentityUserID = motorcycleAdvertising.Advertising.IdentityUserID;
                    adFromDb.PlaceID = motorcycleAdvertising.Advertising.PlaceID;
                    adFromDb.Price = model.Advertising.Price;
                    adFromDb.Description = model.Advertising.Description;
                    adFromDb.Created = model.Advertising.Created;
                    adFromDb.LastModification = DateTime.UtcNow;
                    adFromDb.Frozen = false;
                    _context.Advertisings.Update(adFromDb);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to update advertising: " + ex.Message);
                await transaction.RollbackAsync();

                return View(model);
            }
        }

        // POST: Advertisings/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var motorcycleAdvertising = _context.MotorcycleAdvertisings
            .Include(x => x.Motorcycle)
            .Include(x => x.Advertising)
            .Include(x => x.Advertising.Place)
            .FirstOrDefault(x => x.Advertising.Id == id);

            if (motorcycleAdvertising == null)
            {
                _logger.LogError("MotorcycleAdvertising not found for ID: " + id);
                return NotFound();
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var motorcycle = motorcycleAdvertising.Motorcycle;
                _context.Motorcycles.Remove(motorcycle);

                var advertising = motorcycleAdvertising.Advertising;
                _context.Advertisings.Remove(advertising);

                var place = await _context.Places.FindAsync(motorcycleAdvertising.Advertising.PlaceID);
                _context.Places.Remove(place);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to delete advertising: " + ex.Message);
                await transaction.RollbackAsync();

                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        private bool AdvertisingExists(int id)
        {
            return _context.Advertisings.Any(x => x.Id == id);
        }
    }
}
