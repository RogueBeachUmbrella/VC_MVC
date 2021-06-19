using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VC_MVC.Models;
using VC_MVC.DataAccess;

namespace VC_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public ParkContext _context;

        public HomeController(ParkContext context)
        {
            _context = context;
        }



        HttpClient httpClient;
        static string API_KEY = "Vr70dB2Pow7KCrTcaYZPIeB5ENkAl7omMCzTLuXZ";
        static string BASE_URL = "https://developer.nps.gov/api/v1/";

        static string MAPQUEST_KEY = "u4m3d6bHKG95zCg5v9YH9uWULp6D8W5D";
        static string MAPQUEST_BASE_URL = "https://open.mapquestapi.com/staticmap/v5/map?";


        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index(int id)
        {
            return View();
        }




        public ViewResult DemoChart()
        {
            string[] ChartLabels = new string[] { "Africa", "Asia", "Europe", "Latin America", "North America" };
            int[] ChartYoungData = new int[] { 2478, 5267, 734, 784, 433 };
            int[] ChartOlderData = new int[] { 1478, 4267, 334, 284, 333 };

            ChartModel Model = new ChartModel
            {
                Charts = new List<Chart>
                {
                    new Chart{
                    ChartType = "pie",
                    Labels = String.Join(",", ChartLabels.Select(d => "'" + d + "'")),
                    Data = String.Join(",", ChartOlderData.Select(d => d)),
                    Title = "Predicted world older population (millions) in 2050" },
                    new Chart{
                    ChartType = "bar",
                    Labels = String.Join(",", ChartLabels.Select(d => "'" + d + "'")),
                    Data = String.Join(",", ChartYoungData.Select(d => d)),
                    Title = "Predicted world younger population (millions) in 2050" }
                }
            };

            return View(Model);
        }
        

        public IActionResult Park()
        {
            ViewBag.Message = "Welcome to The American National Parks!";
            ParkViewModel mymodel = new ParkViewModel();
            mymodel.park = _context.Parks.First();
            mymodel.parklist = _context.Parks.OrderBy(p => p.states).ThenBy(p => p.fullName).ToList();
            mymodel.mapquestkey = MAPQUEST_KEY;
            mymodel.mapquesturl = MAPQUEST_BASE_URL;
            return View(mymodel);
        }


        [HttpPost]
        public IActionResult Park(ParkViewModel mymodel)
        {
            ViewBag.Message = "Welcome to The American National Parks!";
            //ParkViewModel mymodel = new ParkViewModel();

            mymodel.park = _context.Parks.Find(mymodel.park.ParkId);
            mymodel.parklist = _context.Parks.OrderBy(p => p.states).ThenBy(p => p.fullName).ToList();
            mymodel.mapquestkey = MAPQUEST_KEY;
            mymodel.mapquesturl = MAPQUEST_BASE_URL;
            return View(mymodel);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Carla()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}



