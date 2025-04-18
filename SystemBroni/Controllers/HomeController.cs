using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SystemBroni.Models;
using SystemBroni.Service;

namespace SystemBroni.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IVipRoomBookingService _vipRoomBookingService;
    private readonly ITableBookingService _tableBookingService;

    public HomeController(
        ILogger<HomeController> logger,
        IVipRoomBookingService vipRoomBookingService,
        ITableBookingService tableBookingService)
    {
        _logger = logger;
        _vipRoomBookingService = vipRoomBookingService;
        _tableBookingService = tableBookingService;
    }
   
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        ViewBag.VipRooms = await _vipRoomBookingService.GetAllVipRooms();
        ViewBag.Tables = await _tableBookingService.GetAllTables();
        
        return View();
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
