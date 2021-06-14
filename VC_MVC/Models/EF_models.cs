using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VC_MVC.Models
{
    public class Company
    {
        [Key]
        public string symbol { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public bool isEnabled { get; set; }
        public string type { get; set; }
        public string iexId { get; set; }
        public List<Quote> Quotes { get; set; }
    }

    public class Quote
    {
        public int QuoteId { get; set; }
        public string date { get; set; }
        public float open { get; set; }
        public float high { get; set; }
        public float low { get; set; }
        public float close { get; set; }
        public int volume { get; set; }
        public int unadjustedVolume { get; set; }
        public float change { get; set; }
        public float changePercent { get; set; }
        public float vwap { get; set; }
        public string label { get; set; }
        public float changeOverTime { get; set; }
        public string symbol { get; set; }
    }

    public class ChartRoot
    {
        public Quote[] chart { get; set; }
    }

    public class CarShowroom
    {
        public int CarShowroomId { get; set; }
        public virtual ICollection<CarToCarShowroom> CarToCarShowrooms { get; set; } = new List<CarToCarShowroom>();
    }

    public class Car
    {
        public int CarId { get; set; }
        public virtual ICollection<CarToCarShowroom> CarToCarShowrooms { get; set; } = new List<CarToCarShowroom>();
    }

    public class CarToCarShowroom
    {
        public int CarId { get; set; }
        public virtual Car Car { get; set; }

        public int CarShowroomId { get; set; }
        public virtual CarShowroom CarShowroom { get; set; }
    }

}
