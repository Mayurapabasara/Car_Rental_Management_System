using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CarRentalSystem.Models;

namespace CarRentalSystem.Controllers
{
    public class HomeController : Controller
    {
        // Temporary in-memory data
        private static List<Car> cars = new List<Car>
        {
            new Car { Id = 1, Make = "Toyota", Model = "Corolla", Year = 2020, IsAvailable = true },
            new Car { Id = 2, Make = "Honda", Model = "Civic", Year = 2019, IsAvailable = true },
            new Car { Id = 3, Make = "Ford", Model = "Focus", Year = 2021, IsAvailable = true }
        };

        public ActionResult Index()
        {
            return View(cars);
        }

        public ActionResult Book(int id)
        {
            var car = cars.FirstOrDefault(c => c.Id == id);
            if (car == null || !car.IsAvailable)
                return HttpNotFound();

            return View(car);
        }

        [HttpPost]
        public ActionResult Book(int id, string customerName)
        {
            var car = cars.FirstOrDefault(c => c.Id == id);
            if (car != null && car.IsAvailable)
            {
                car.IsAvailable = false;
                TempData["Message"] = $"Car {car.Make} {car.Model} booked successfully for {customerName}!";
            }
            return RedirectToAction("Index");
        }
    }
}

