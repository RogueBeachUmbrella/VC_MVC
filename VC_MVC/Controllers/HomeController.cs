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

        public IActionResult DNU_Index(int id)
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
        

        public IActionResult Index()
        {
            ViewBag.Message = "US National Parks";
            ParkViewModel mymodel = new ParkViewModel();
            mymodel.park = _context.Parks.First();
            mymodel.parklist = _context.Parks.OrderBy(p => p.states).ThenBy(p => p.fullName).ToList();
            TempData["ParkId"] = mymodel.park.ParkId;
            mymodel.ParkId = mymodel.park.ParkId;
            mymodel.mapquestkey = MAPQUEST_KEY;
            mymodel.mapquesturl = MAPQUEST_BASE_URL;
            return View(mymodel);
        }


        [HttpPost]
        public IActionResult Index(ParkViewModel mymodel)
        {
            ViewBag.Message = "US National Parks";
            //ParkViewModel mymodel = new ParkViewModel();
            TempData["ParkId"] = mymodel.ParkId;
            TempData.Keep("ParkId");
            mymodel.park = _context.Parks.Find(mymodel.ParkId);
            mymodel.parklist = _context.Parks.OrderBy(p => p.states).ThenBy(p => p.fullName).ToList();
            mymodel.mapquestkey = MAPQUEST_KEY;
            mymodel.mapquesturl = MAPQUEST_BASE_URL;
            return View(mymodel);
        }



        public ViewResult ParkDesignationChart()
        {
            List<string> ChartLabelsList = new List<string>();
            List<int> ChartDataList = new List<int>();

            var mydesignations = from p in _context.Parks group p by p.designation into c select new { designation = c.Key, count = c.Count() };

            foreach (var item in mydesignations)
            {
                ChartLabelsList.Add((String.IsNullOrEmpty(item.designation) ? "--Not Designated--" : item.designation));
                ChartDataList.Add(item.count);
            }

            string[] ChartLabels = ChartLabelsList.ToArray();
            int[] ChartData = ChartDataList.ToArray();

            ChartModel Model = new ChartModel
            {
                Charts = new List<Chart>
                {
                    new Chart{
                    ChartType = "pie",
                    Labels = String.Join(",", ChartLabels.Select(d => "'" + d + "'")),
                    Data = String.Join(",", ChartData.Select(d => d)),
                    Title = "US National Park Designations" }
                }
            };

            return View(Model);
        }




        public IActionResult MakeReservation(Reservation reservation, string reserveit, string cancel)
        {
            if (TempData["ParkId"] != null)
            {               
                reservation.Parks = _context.Parks.Find(TempData["ParkId"].ToString());
                TempData.Keep("ParkId");
            }
            else
            {
                return View(reservation);
            }

            if (!string.IsNullOrEmpty(reserveit))
            {
                var reserve = new Reservation
                {
                    ReservationNumber = GenerateRandomNo(),
                    EndDate = reservation.EndDate,
                    facility = Request.Form["Parkfacility"].ToString(),
                    ParkId = TempData["ParkId"].ToString(),
                    StartDate = reservation.StartDate,
                    Visitors = new Visitor
                    {
                        Address = reservation.Visitors.Address,
                        Email = reservation.Visitors.Email,
                        FirstName = reservation.Visitors.FirstName,
                        LastName = reservation.Visitors.LastName,
                        PhoneNumber = reservation.Visitors.PhoneNumber
                    }
                };
                _context.Reservation.Add(reserve);

                _context.SaveChanges();

                ViewBag.Message = $"reservation saved successfully! your confirmation number is {reserve.ReservationNumber} ";
                ModelState.Clear();
            }
            if (!string.IsNullOrEmpty(cancel))
            {
                ViewBag.Message = "The operation was cancelled!";
                ModelState.Clear();
            }
            return View(reservation);
        }

       
        private string GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max).ToString();
        }

        public IActionResult ManageReservation(Reservation reservation,string update, string edit, string find, string cancel, string delete)
        {

            if (!string.IsNullOrEmpty(edit))
            {
                reservation.ReservationNumber = TempData["ReservationNo"].ToString();
                TempData.Keep("ReservationNo");
                return View(reservation);
            }

            if (!string.IsNullOrEmpty(update))
            {
                reservation.ReservationNumber = TempData["ReservationNo"].ToString();
                TempData.Keep("ReservationNo");
                var reservationupdate = _context.Reservation
                                  .Where(c => c.ReservationNumber == reservation.ReservationNumber).FirstOrDefault();
               
                reservationupdate.StartDate = reservation.StartDate;
                reservationupdate.EndDate = reservation.EndDate;
                reservationupdate.facility = Request.Form["Parkfacility"].ToString();

                _context.Reservation.Update(reservationupdate);

                _context.SaveChanges();

                reservationupdate.Parks = _context.Parks
                                  .Where(c => c.ParkId == reservationupdate.ParkId).FirstOrDefault();
                reservationupdate.Visitors = _context.Visitor
                                   .Where(c => c.VisitorId == reservationupdate.VisitorId).FirstOrDefault();

                ViewBag.Message = "Reservation is updated!";

                return View(reservationupdate);
            }

            if (!string.IsNullOrEmpty(cancel))
            {
                ViewBag.Message = "The operation was cancelled!";
                ModelState.Clear();
                return View(reservation);
            }

            if (!string.IsNullOrEmpty(delete))
            {
                reservation.ReservationNumber = TempData["ReservationNo"].ToString();
                TempData.Keep("ReservationNo");
                reservation = _context.Reservation
                                  .Where(c => c.ReservationNumber == reservation.ReservationNumber).FirstOrDefault();

                _context.Reservation.Remove(reservation);

                _context.SaveChanges();
                reservation = null;
                ViewBag.Message = "The reservation is cancelled!";
                ModelState.Clear();
                return View(reservation);
            }

            if (!string.IsNullOrEmpty(find))
            {
                reservation = _context.Reservation
                                   .Where(c => c.ReservationNumber == reservation.ReservationNumber)
                                   .Where(c => c.Visitors.FirstName == reservation.Visitors.FirstName)
                                   .FirstOrDefault();
                if (reservation == null)
                {
                    ViewBag.Message = "No reservation found!";                 
                    return View(reservation);
                }
                reservation.Parks = _context.Parks
                                   .Where(c => c.ParkId == reservation.ParkId).FirstOrDefault();
                reservation.Visitors = _context.Visitor
                                   .Where(c => c.VisitorId == reservation.VisitorId).FirstOrDefault();
                TempData["ReservationNo"] = reservation.ReservationNumber;
                return View(reservation);
            }
             return View(reservation);
           
        }

        public IActionResult AboutUs()
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



