using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using Newtonsoft.Json;
using VC_MVC.DataAccess;
using VC_MVC.Models;

namespace VC_MVC.Controllers
{
    public class DataController : Controller
    {
        HttpClient httpClient;
        static string API_KEY = "Vr70dB2Pow7KCrTcaYZPIeB5ENkAl7omMCzTLuXZ";
        static string BASE_URL = "https://developer.nps.gov/api/v1";
        static string NPS_SOURCE = "US NPS API";
        static string LOCAL_SOURCE = "Internal JSON Dataset";

        //private readonly ParkContext _context;
        public ParkContext _context;
        
        public DataController(ParkContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ViewResult> LoadParks()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string NATIONAL_PARK_API_PATH = BASE_URL + "/parks?limit=512";
            string parksData = String.Empty;
            Parks parks = null;
            ParkLoadModel parkload = new ParkLoadModel();
            parkload.parkCount = 0;
            parkload.dataSource = NPS_SOURCE;

            httpClient.BaseAddress = new Uri(NATIONAL_PARK_API_PATH);

            // It can take a few requests to get back a prompt response, if the API has not received
            //  calls in the recent past and the server has put the service on hibernation
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(NATIONAL_PARK_API_PATH).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    parksData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!parksData.Equals(String.Empty))
                {
                    // Truncate MyParks
                    _context.Database.ExecuteSqlRaw("DELETE from [Parks]");

                    // JsonConvert is part of the NewtonSoft.Json Nuget package
                    parks = JsonConvert.DeserializeObject<Parks>(parksData);

                    foreach (var park in parks.data)
                    {
                        Park tempPark = new Park();
                        tempPark.ParkId = park.ParkId;
                        tempPark.url = park.url;
                        tempPark.fullName = park.fullName;
                        tempPark.parkCode = park.parkCode;
                        tempPark.description = park.description;
                        tempPark.latitude = park.latitude;
                        tempPark.longitude = park.longitude;
                        tempPark.latLong = park.latLong;
                        tempPark.states = park.states;
                        tempPark.directionsInfo = park.directionsInfo;
                        tempPark.directionsUrl = park.directionsUrl;
                        tempPark.weatherInfo = park.weatherInfo;
                        tempPark.name = park.name;
                        tempPark.designation = park.designation;
                        _context.Parks.Add(tempPark);
                        tempPark = null;


                    }
                    //await _context.SaveChangesAsync();
                                    }
                else
                {

                    // Delete current parks
                    _context.Database.ExecuteSqlRaw("DELETE from [Parks]");

                    ParkDbInitializer.Initialize(_context);
                }

                await _context.SaveChangesAsync();
                

                //Initialize ViewModel
                parkload.parkCount = (from p in _context.Parks select p).Count();
                parkload.designationList = new List<designationCount>();

                //var mydesignations = _context.Parks.GroupBy(p => p.designation).Select(p => new { designation = x.designation, count = p.Count() });
                var mydesignations = from p in _context.Parks group p by p.designation into c select new { designation = c.Key, count = c.Count() };

                foreach (var item in mydesignations)
                {
                    designationCount mydesignation = new designationCount();

                    mydesignation.count = item.count;
                    mydesignation.designation = (String.IsNullOrEmpty(item.designation) ? " --Not Designated-- " : item.designation);

                    parkload.designationList.Add(mydesignation);
                    mydesignation = null;
                }


            }
            catch (System.Exception e)
            {
                // This is a useful place to insert a breakpoint and observe the error message
                Console.WriteLine(e.Message);
            }




            return View(parkload);
        }


        public async Task<ViewResult> LoadInternalParks()
        {
           
            ParkLoadModel parkload = new ParkLoadModel();
            parkload.parkCount = 0;
            parkload.dataSource = LOCAL_SOURCE;
            parkload.designationList = new List<designationCount>();

            try
            {
                // Delete current parks
                _context.Database.ExecuteSqlRaw("DELETE from [Parks]");
                ParkDbInitializer.Initialize(_context);
                

                //Initialize ViewModel
                parkload.parkCount = (from p in _context.Parks select p).Count();
              

                //var mydesignations = _context.Parks.GroupBy(p => p.designation).Select(p => new { designation = x.designation, count = p.Count() });
                var mydesignations = from p in _context.Parks group p by p.designation into c select new { designation = c.Key, count = c.Count() };

                foreach (var item in mydesignations)
                {
                    designationCount mydesignation = new designationCount();

                    mydesignation.count = item.count;
                    mydesignation.designation = (String.IsNullOrEmpty(item.designation) ? " --Not Designated-- " : item.designation);

                    parkload.designationList.Add(mydesignation);
                    mydesignation = null;
                }
            }
            catch (System.Exception e)
            {
                // This is a useful place to insert a breakpoint and observe the error message
                Console.WriteLine(e.Message);
            }

            return View("LoadParks", parkload);
        }

    }
}
