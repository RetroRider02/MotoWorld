using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoWorld3.Data;
using MotoWorld3.ViewModels;
using MotoWorld3.ViewModels.Home;

namespace MotoWorld3.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var advertisings = await _context.MotorcycleAdvertisings.Include(a => a.Motorcycle).Include(a => a.Motorcycle.MotorcycleType).Include(a => a.Advertising).Take(12).ToListAsync();
        var pictures = await _context.Pictures.Include(x => x.Advertising).ToListAsync();

        var advertisingIds = advertisings.Select(a => a.AdvertisingID).ToHashSet();
        var resultPictures = pictures.Where(p => advertisingIds.Contains(p.AdvertisingID)).ToList();

        ViewBag.ManufacturerList = _context.MotorcycleTypes.Select(x => x.Manufacturer).Distinct().ToList();

        return View(new IndexViewModel(advertisings, resultPictures));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
