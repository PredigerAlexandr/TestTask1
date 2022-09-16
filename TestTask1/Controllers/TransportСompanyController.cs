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
using ClosedXML.Excel;

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
            var query_params = new Distance(FirstCoords.Concat(LastCoords).ToList());
            double distance = Distance.GetDistance(query_params);
            if (distance == 0)
            {
                ModelState.AddModelError(nameof(order.LastPlace), "Нет дорог между населёнными пунктами");
            }


            if (ModelState.IsValid)
            {
                
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
                db.Orders.ToList();
                var Tc = db.Transport_companies.FirstOrDefault(t => t.Name == tcName);
                IOrderedQueryable orders = db.Orders.Where(o => o.Tc.Id == Tc.Id).OrderBy(o => o.Date);
                ViewBag.Orders = orders;
                //List<Order> orders = (List<Order>)Tc.orders.OrderBy(p => p.Date);
                return View(Tc);
            }
        }
        [HttpGet]
        public IActionResult ExportExel(int tcId)
        {
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var worksheet = workbook.Worksheets.Add("Заказы");

                worksheet.Cell(1, 1).Value = "Имя";
                worksheet.Cell(1, 2).Value = "Фамилия";
                worksheet.Cell(1, 3).Value = "Откуда";
                worksheet.Cell(1, 4).Value = "Куда";
                worksheet.Cell(1, 5).Value = "Вес";
                worksheet.Cell(1, 6).Value = "Размер";
                worksheet.Cell(1, 7).Value = "Расстояние";
                worksheet.Cell(1, 8).Value = "Цена";
                worksheet.Cell(1, 9).Value = "ТК";
                worksheet.Cell(1, 10).Value = "Дата";
                worksheet.Row(1).Style.Font.Bold = true;

                db.Transport_companies.ToList();
                IOrderedQueryable orders = db.Orders.Where(o => o.Tc.Id == tcId).OrderBy(o => o.Date);
                int iter = 1;
                foreach(Order item in orders)
                {
                    iter++;
                    worksheet.Cell(iter, 1).Value = item.FirstName;
                    worksheet.Cell(iter, 2).Value = item.SurName;
                    worksheet.Cell(iter, 3).Value = item.FirstPlace;
                    worksheet.Cell(iter, 4).Value = item.LastPlace;
                    worksheet.Cell(iter, 5).Value = item.Weight;
                    worksheet.Cell(iter, 6).Value = item.Size;
                    worksheet.Cell(iter, 7).Value = item.Distance;
                    worksheet.Cell(iter, 8).Value = item.Price;
                    worksheet.Cell(iter, 9).Value = item.Tc.Name;
                    worksheet.Cell(iter, 10).Value = item.Date;
                }
                using(var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = "orders.xlsx"
                    };
                }
            }
        }
    }
}
