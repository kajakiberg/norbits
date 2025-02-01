using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NorbitsChallenge.Dal;
using NorbitsChallenge.Helpers;
using NorbitsChallenge.Models;

namespace NorbitsChallenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            var model = GetCompanyModel();
            return View(model);
        }

        [HttpPost]
        public JsonResult Index(int companyId, string licensePlate)
        {
            var car = new CarDb(_config).GetCarByLicensePlate(companyId, licensePlate.ToUpper());
            var model = GetCompanyModel();

            if (car != null)
            {
                model.Car = car;
            }
            else
            {
                model.Car = null;
            }

            return Json(model);
        }

        public IActionResult CarsList()
        {
            var companyId = UserHelper.GetLoggedOnUserCompanyId();
            var carDb = new CarDb(_config);
            
            var cars = carDb.GetAllCars(companyId) ?? new List<Car>();  
            
            return View(cars);  
        }

        public IActionResult AddCar()
        {
            var companyId = UserHelper.GetLoggedOnUserCompanyId();  
            var car = new Car { CompanyId = companyId };  
            return View(car); 
        }

        [HttpPost]
        public IActionResult AddCar(Car car)
        {
            if (ModelState.IsValid)  
            {
                var carDb = new CarDb(_config);

                if (carDb.GetCarByLicensePlateAcrossCompanies(car.LicensePlate.ToUpper()) != null)
                {
                    ModelState.AddModelError("LicensePlate", "Bilen med dette bilskiltet er allerede registrert.");
                    return View(car);
                }
                carDb.AddCar(car); 
                return RedirectToAction("CarsList");  
            }

            return View(car);  
        }

        public IActionResult EditCar(int companyId, string licensePlate)
        {
            var carDb = new CarDb(_config);
            var car = carDb.GetCarByLicensePlate(companyId, licensePlate);

            if (car == null)
            {
                return RedirectToAction("CarsList");
            }

            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCar(Car car)
        {
            if (ModelState.IsValid)
            {
                var carDb = new CarDb(_config);
                carDb.UpdateCar(car);
                return RedirectToAction("CarsList");
            }
            return View(car);
        }

        [HttpPost]
        public IActionResult DeleteCar(string licensePlate)
        {
            if (!string.IsNullOrEmpty(licensePlate))
            {
                var carDb = new CarDb(_config);
                carDb.DeleteCar(licensePlate);
            }
            return RedirectToAction("CarsList");
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private HomeModel GetCompanyModel()
        {
            var companyId = UserHelper.GetLoggedOnUserCompanyId();
            var companyName = new SettingsDb(_config).GetCompanyName(companyId);
            return new HomeModel { CompanyId = companyId, CompanyName = companyName };
        }
    }
}
