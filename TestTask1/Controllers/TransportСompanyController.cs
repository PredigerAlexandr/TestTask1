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
using System.Diagnostics;
using TestTask1.Models.Database;

namespace TestTask1.Controllers
{
    public class TransportСompanyController : Controller
    {
        ApplicationContext db = new ApplicationContext(); 
        public List<Order> orders = new List<Order>();
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AllCalculator()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AllCalculatorAsync(OrderViewModel order)
        {

            //получаем кординаты городов отправки и прибытия
            List<double> FirstCoords = await Coords.GetCoords(order.FirstPlace);
            List<double> LastCoords = await Coords.GetCoords(order.LastPlace);
            if (FirstCoords.Count == 0)
            {
                ModelState.AddModelError(nameof(order.FirstPlace), "Не можем найти пункт назначения");
            }
            if (LastCoords.Count == 0)
            {
                ModelState.AddModelError(nameof(order.LastPlace), "Не можем найти пункт назначения");
            }
            


            if (ModelState.IsValid)
            {
                var query_params = new Distance(FirstCoords.Concat(LastCoords).ToList());
                double distance = Distance.GetDistance(query_params);
                var companies = db.Transport_companies.ToList();
                foreach (var company in companies)
                {
                    orders.Add( new Order()
                    {
                        FirstName = order.FirstName,
                        SurName = order.SurName,
                        Phone = order.Phone,
                        FirstPlace = order.FirstPlace,
                        LastPlace = order.LastPlace,
                        Weight = order.Weight,
                        Size = order.Size,
                        Date = DateTime.Now,
                        Distance = Convert.ToInt32(distance),
                        Price = company.GetPrice(distance, order.Weight, order.Size),
                        Tc = company
                    });
                }
                ViewBag.Orders = orders;
                return View(order);

            }
            else
            {
                return View(order);
            }
        }

        [HttpGet]
        public IActionResult TcShow(string FirstName, string SurName, string Phone, string FirstPlace, string LastPlace, string Weight, string Size, string Distance, string Company, string Price, string tcName="")
        {
            if (tcName == "")
            {
                Order order = new Order()
                {
                    FirstName = FirstName,
                    SurName = SurName,
                    Phone = Phone,
                    FirstPlace = FirstPlace,
                    LastPlace = LastPlace,
                    Weight = Convert.ToDouble(Weight),
                    Size = Convert.ToDouble(Size),
                    Date = DateTime.Now,
                    Distance = Convert.ToInt32(Distance),
                    Price = Convert.ToDouble(Price),
                    Tc = db.Transport_companies.FirstOrDefault(p => p.Id == Convert.ToInt32(Company))
                };
                db.Orders.Add(order);
                db.SaveChanges();
               
                return Redirect("AllCalculator");
            }
            else
            {
                
                var Tc = db.Transport_companies.FirstOrDefault(t => t.Name == tcName);
                List<Order> orders = Tc.orders;
                return View(Tc);
            }
        }
    }
}
