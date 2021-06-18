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
            using (context)
            {
                if (!context.Parks.Any())
                {
                    List<Park> parks = ParkFile().data;
                    context.Parks.AddAsync(parks.First());
                    //context.Parks.AddRangeAsync(parks).Wait();
                    context.SaveChangesAsync().Wait();
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
