using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using filmJPTVR18.Models;

namespace filmJPTVR18.Controllers
{
    public class ActorsController : Controller
    {
        private filmJPTVR18Entities db = new filmJPTVR18Entities();

        // GET: Actors
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.Actors.OrderByDescending(f => f.Id).ToList());
        }

        // GET: Actors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        // GET: Actors/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,SecondName,Image,ImageType")] Actor actor, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {

                if(Image != null)
                {
                    actor.ImageType = Image.ContentType;
                    actor.Image = new byte[Image.ContentLength];
                    Image.InputStream.Read(actor.Image, 0, Image.ContentLength);
                }
                actor.FullName = actor.FirstName + " " + actor.SecondName;
                db.Actors.Add(actor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(actor);
        }

        // GET: Actors/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "Id,FirstName,SecondName,Image, ImageType")] Actor actor, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    actor.ImageType = Image.ContentType;
                    actor.Image = new byte[Image.ContentLength];
                    Image.InputStream.Read(actor.Image, 0, Image.ContentLength);
                }
                actor.FullName = actor.FirstName + " " + actor.SecondName;
                db.Entry(actor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(actor);
        }

        // GET: Actors/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        // POST: Actors/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var fa = this.db.FilmActors.Include(f => f.Actor).FirstOrDefault(f => f.ActorId == id);
            if (fa != null)
            {
                return RedirectToAction("Delete", "FilmActors", new { id = fa.Id });
            }
            else
            {
                Actor actor = db.Actors.Find(id);
                db.Actors.Remove(actor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Get image
        public FileContentResult GetImage(int id)
        {
            Actor actor = db.Actors.FirstOrDefault(f => f.Id == id);
            if(actor.Image != null)
            {
                return File(actor.Image, actor.ImageType);
            }
            return null;
        }

        //Get films partial view
        public ActionResult getActorFilm(int id)
        {
            return PartialView(this.db.FilmActors.Include( fa => fa.Film ).Where(fa => fa.ActorId == id));
        }
    }
}
