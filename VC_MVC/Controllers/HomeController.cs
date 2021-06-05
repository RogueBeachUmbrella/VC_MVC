using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VC_MVC.Models;

namespace VC_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        HttpClient httpClient;
        static string API_KEY = "Vr70dB2Pow7KCrTcaYZPIeB5ENkAl7omMCzTLuXZ";
        static string BASE_URL = "https://developer.nps.gov/api/v1/";


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int id)
        {
            return View();
        }


        public IActionResult Employee()
        {

            var employee = new Employee
            {
                EmployeeId = 123,
                Name = "VibhaNa",
                Email = "vibhana@usf.edu",
                Phone = "7776668888"
            };

            return View(employee);
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
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string NATIONAL_PARK_API_PATH = BASE_URL + "parks?parkCode=acad&api_key=Vr70dB2Pow7KCrTcaYZPIeB5ENkAl7omMCzTLuXZ";
            string parksData = "";

            Parks parks = null;

            httpClient.BaseAddress = new Uri(NATIONAL_PARK_API_PATH);

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(NATIONAL_PARK_API_PATH).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    parksData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!parksData.Equals(""))
                {
                    // JsonConvert is part of the NewtonSoft.Json Nuget package
                    parks = JsonConvert.DeserializeObject<Parks>(parksData);
                }
            }
            catch (Exception e)
            {
                // This is a useful place to insert a breakpoint and observe the error message
                Console.WriteLine(e.Message);
            }

            return View(parks);
        }


        public IActionResult Privacy()
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



