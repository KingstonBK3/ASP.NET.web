using Praktika2HotelsMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Praktika2HotelsMVC.Controllers
{
    public class CitiesController : Controller
    {
        // GET: Cities
        public ActionResult Index()
        {
            ViewBag.Cities = CitiesCollection.listCities;
            return View();
        }
        public ActionResult Details(int id = 0)
        {
            ViewBag.Cities = CitiesCollection.listCities;
            if (id != 0)
            {
                ViewBag.HotelId = id;
                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}