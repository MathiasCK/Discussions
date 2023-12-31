﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Discussions.Models;

namespace Discussions.Controllers;

[ServiceFilter(typeof(UserSessionFiler))]
public class HomeController : Controller
{

    public IActionResult Index()
    {
        if (TempData.ContainsKey("ErrorMessage"))
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"]?.ToString();
        }

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

