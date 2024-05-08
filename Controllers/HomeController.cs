using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication2.Models;

namespace WebApplication2.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly List<Item> _items; // Simulated data source

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        // Simulated data
        _items = Enumerable.Range(1, 100).Select(i => new Item { Id = i, Name = "Item " + i }).ToList();
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult GetItems(int pageNumber, int pageSize, string searchTerm = "")
    {
        var filteredItems = _items.Where(i => i.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        var totalItems = filteredItems.Count();
        var itemsOnPage = filteredItems.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        return Json(new { total = totalItems, items = itemsOnPage });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
