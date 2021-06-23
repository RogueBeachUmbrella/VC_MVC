using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using VC_MVC.Models;

namespace VC_MVC.DataAccess
{
    public class ParkDbInitializer 
    {
        public static void Initialize(ParkContext context)
        {
            try 
            {
                if (!context.Parks.Any())
                {
                    List<Park> parks = ParkFile().data;
                    foreach (var park in parks)
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
                        context.Parks.Add(tempPark);
                        tempPark = null;
                    }
                    context.SaveChanges();
                
                }
                //if (!context.Activities.Any())
                //{
                //    var activities = ParkFile().data
                //                    .Select(p => p.activities.Select(a => new Activity()
                //                    {
                //                        ActivityId = a.ActivityId,
                //                        name = a.name
                //                        //park = p,
                //                        //ParkId = p.ParkId
                //
                //                    }).First()).First();
                //    context.Database.EnsureCreated();
                //
                //    //context.Parks.
                //    //context.Activities.Add(activities);
                //
                //    var park = context.Parks.Include(p => p.activities).First();
                //    park.activities.Add(activities);
                //    //var post = new Post { Title = "Intro to EF Core" };
                //    //
                //    //blog.Posts.Add(post);
                //    //context.SaveChanges();
                //
                //    context.SaveChanges();
                //}

            }
            catch (System.Exception e)
            {
                // This is a useful place to insert a breakpoint and observe the error message
                Console.WriteLine(e.Message);
            }


        }

        static Parks ParkFile()
        {
            return JsonConvert.DeserializeObject<Parks>(File.ReadAllText(@"parks.json"));
        }
        static void InitializeParkTable()
        {

        }
    }
}
