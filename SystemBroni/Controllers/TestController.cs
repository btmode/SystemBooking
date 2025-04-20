using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SystemBroni.Models;
using SystemBroni.Service;

namespace SystemBroni.Controllers;

[Route("Test")]
// Спросить GPT
public class TestController : Controller
{
    public string Index()
    {
        return "Hello World!";
    }
}
