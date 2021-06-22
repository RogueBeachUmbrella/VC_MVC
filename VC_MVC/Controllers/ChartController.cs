using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VC_MVC.Controllers
{
    public class ChartController : ChartController
    {
        public IActionResult Chart()
        {
            return ViewResult();
        }

        private IActionResult ViewResult()
        {
            throw new NotImplementedException();
        }
    }
