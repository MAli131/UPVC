using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UPVC.Models;
using UPVC.Filters;
using UPVC.Data;
using UPVC.ViewModels;

namespace UPVC.Controllers;

[PreventAdminAccess]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = await GetHomeViewModelAsync("Index");
        return View(viewModel);
    }

    public async Task<IActionResult> Home2()
    {
        var viewModel = await GetHomeViewModelAsync("Home2");
        return View(viewModel);
    }

    public async Task<IActionResult> Home3()
    {
        var viewModel = await GetHomeViewModelAsync("Home3");
        return View(viewModel);
    }

    public async Task<IActionResult> Home4()
    {
        var viewModel = await GetHomeViewModelAsync("Home4");
        return View(viewModel);
    }

    private async Task<HomeViewModel> GetHomeViewModelAsync(string pageKey)
    {
        var viewModel = new HomeViewModel
        {
            HomePage = await _context.HomePages
                .Where(h => h.PageKey == pageKey && h.IsActive && !h.IsDeleted)
                .FirstOrDefaultAsync(),
            
            CompanyInfo = await _context.CompanyInfos
                .Where(c => c.IsActive && !c.IsDeleted)
                .FirstOrDefaultAsync(),
            
            SocialMedias = await _context.SocialMedias
                .Where(s => s.IsActive && !s.IsDeleted)
                .OrderBy(s => s.DisplayOrder)
                .ToListAsync()
        };

        return viewModel;
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
