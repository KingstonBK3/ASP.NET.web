using filmJPTVR18.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList.Mvc;
using PagedList;

namespace filmJPTVR18.Controllers
{
    public class HomeController : Controller
    {

        private filmJPTVR18Entities db = new filmJPTVR18Entities();

        public ActionResult Index()
        {
            return View(db.Films.OrderByDescending(v => v.Id).Take(3).ToList());
        }

        public ActionResult Films()
        {
            return View(db.Films.OrderByDescending(f => f.Id).ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public FileContentResult GetImage(int id)
        {
            Film film = db.Films.FirstOrDefault(f => f.Id == id);
            if (film.Image != null)
            {
                return File(film.Image, film.ImageType);
            }
            return null;
        }

        [ChildActionOnly]
        public ActionResult GetFilmAuthors(int id)
        {
            return PartialView(db.FilmActors.Include(f => f.Actor).Where(f => f.FilmId == id));
        }
        
        //Show films with search 
        public ActionResult FilmsWithSearch(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var films = db.Films.OrderByDescending(f => f.Id).ToList();
            return View(films.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        //Show films by ajax search 
        public ActionResult FilmsWithSearchPost(string FilmName, int? page)
        {

            var films = this.db.Films.Where(f => f.Title.Contains(FilmName)).ToList();
            if (films.Count <= 0)
            {
                return HttpNotFound();
            }
            return PartialView(films);
        }
    }
}