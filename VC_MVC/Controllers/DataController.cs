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

            }
            catch (System.Exception e)
            {
                // This is a useful place to insert a breakpoint and observe the error message
                Console.WriteLine(e.Message);
            }




            return View();
        }

        //public async Task<ViewResult> DataCrud()
        //{
        //    // CREATE operation
        //    Company MyCompany = new Company();
        //    MyCompany.symbol = "MCOB";
        //    MyCompany.name = "ISM";
        //    MyCompany.date = "ISM";
        //    MyCompany.isEnabled = true;
        //    MyCompany.type = "ISM";
        //    MyCompany.iexId = "ISM";
        //
        //    Quote MyCompanyQuote1 = new Quote();
        //    //MyCompanyQuote1.EquityId = 123;
        //    MyCompanyQuote1.date = "11-23-2018";
        //    MyCompanyQuote1.open = 46.13F;
        //    MyCompanyQuote1.high = 47.18F;
        //    MyCompanyQuote1.low = 44.67F;
        //    MyCompanyQuote1.close = 47.01F;
        //    MyCompanyQuote1.volume = 37654000;
        //    MyCompanyQuote1.unadjustedVolume = 37654000;
        //    MyCompanyQuote1.change = 1.43F;
        //    MyCompanyQuote1.changePercent = 0.03F;
        //    MyCompanyQuote1.vwap = 9.76F;
        //    MyCompanyQuote1.label = "Nov 23";
        //    MyCompanyQuote1.changeOverTime = 0.56F;
        //    MyCompanyQuote1.symbol = "MCOB";
        //
        //    Quote MyCompanyQuote2 = new Quote();
        //    //MyCompanyQuote1.EquityId = 123;
        //    MyCompanyQuote2.date = "11-23-2018";
        //    MyCompanyQuote2.open = 46.13F;
        //    MyCompanyQuote2.high = 47.18F;
        //    MyCompanyQuote2.low = 44.67F;
        //    MyCompanyQuote2.close = 47.01F;
        //    MyCompanyQuote2.volume = 37654000;
        //    MyCompanyQuote2.unadjustedVolume = 37654000;
        //    MyCompanyQuote2.change = 1.43F;
        //    MyCompanyQuote2.changePercent = 0.03F;
        //    MyCompanyQuote2.vwap = 9.76F;
        //    MyCompanyQuote2.label = "Nov 23";
        //    MyCompanyQuote2.changeOverTime = 0.56F;
        //    MyCompanyQuote2.symbol = "MCOB";
        //
        //    dbContext.Companies.Add(MyCompany);
        //    dbContext.Quotes.Add(MyCompanyQuote1);
        //    dbContext.Quotes.Add(MyCompanyQuote2);
        //
        //    dbContext.SaveChanges();
        //
        //    // READ operation
        //    Company CompanyRead1 = dbContext.Companies
        //                            .Where(c => c.symbol == "MCOB")
        //                            .First();
        //
        //    Company CompanyRead2 = dbContext.Companies
        //                            .Include(c => c.Quotes)
        //                            .Where(c => c.symbol == "MCOB")
        //                            .First();
        //
        //    // UPDATE operation
        //    CompanyRead1.iexId = "MCOB";
        //    dbContext.Companies.Update(CompanyRead1);
        //    //dbContext.SaveChanges();
        //    await dbContext.SaveChangesAsync();
        //
        //    // DELETE operation
        //    //dbContext.Companies.Remove(CompanyRead1);
        //    //await dbContext.SaveChangesAsync();
        //
        //    return View();
        //}
        //
        //public ViewResult LINQOperations()
        //{
        //    Company CompanyRead1 = dbContext.Companies
        //                                    .Where(c => c.symbol == "MCOB")
        //                                    .First();
        //
        //    Company CompanyRead2 = dbContext.Companies
        //                                    .Include(c => c.Quotes)
        //                                    .Where(c => c.symbol == "MCOB")
        //                                    .First();
        //
        //    Quote Quote1 = dbContext.Companies
        //                            .Include(c => c.Quotes)
        //                            .Where(c => c.symbol == "MCOB")
        //                            .FirstOrDefault()
        //                            .Quotes
        //                            .FirstOrDefault();
        //
        //    return View();
        //}
        //
        //public ViewResult DataRelation()        
        //
        //{
        //    dbContext.Database.EnsureCreated();
        //    var car1 = new Car();
        //    var car2 = new Car();
        //   
        //    var showroom = new CarShowroom();
        //  
        //    var carToShowroom1 = new CarToCarShowroom();
        //    var carToShowroom2 = new CarToCarShowroom();
        //   
        //    carToShowroom1.Car = car1;
        //    carToShowroom1.CarShowroom = showroom;
        //    
        //    carToShowroom2.Car = car2;
        //    carToShowroom2.CarShowroom = showroom;
        //
        //    showroom.CarToCarShowrooms.Add(carToShowroom1);
        //    showroom.CarToCarShowrooms.Add(carToShowroom2);
        //  
        //    dbContext.CarShowrooms.Add(showroom);
        //
        //    dbContext.Cars.Add(car1);
        //    dbContext.Cars.Add(car2);
        //
        //    dbContext.SaveChanges();
        //
        //    return View();
        //}
    }
}
