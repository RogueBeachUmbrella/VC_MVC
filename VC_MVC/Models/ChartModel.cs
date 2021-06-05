using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VC_MVC.Models
{
    public class ChartModel
    {
        public List<Chart> Charts { get; set; }      
    }

    public class Chart
    {
        public string ChartType { get; set; }
        public string Labels { get; set; }
        public string Data { get; set; }
        public string Title { get; set; }
    }
}
