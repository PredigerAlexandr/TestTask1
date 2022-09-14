using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static TestTask1.Models.JsonModels.GetCoordsJson;
using static TestTask1.Models.JsonModels.GetDistanceJson;
using System.Linq;
using TestTask1.Models.JsonModels;
using TestTask1.Models.AnotherModels;
using TestTask1.Models.ViewModels;

namespace TestTask1.Controllers
{
    public class TransportСompanyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AllCalculator()
        {
            List<double> FirstCoords = await Coords.GetCoords("Ульяновск");
            List<double> LastCoords = await Coords.GetCoords("Москва");
            var query_params = new Distance(FirstCoords.Concat(LastCoords).ToList());

            
            double mainResult = Distance.GetDistance(query_params);

            return View();
        }
        [HttpPost]
        public IActionResult AllCalculator(OrderViewModel order)
        {
            return View();
        }
    }
}
