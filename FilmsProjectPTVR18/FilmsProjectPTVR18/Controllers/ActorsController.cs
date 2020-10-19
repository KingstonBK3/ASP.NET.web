using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FilmsProjectPTVR18.Models;

namespace FilmsProjectPTVR18.Controllers
{
    public class ActorsController : Controller
    {
        private FilmsEntities db = new FilmsEntities();

        // GET: Actors
        public ActionResult Index()
        {
            return View(db.Actors.ToList());
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Cover,ImageMimeType")] Actor actor, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    actor.ImageMimeType = Image.ContentType;
                    actor.Cover = new byte[Image.ContentLength];
                    Image.InputStream.Read(actor.Cover, 0, Image.ContentLength);
                }
                actor.FirstName = actor.FirstName + " " + actor.LastName;
                db.Actors.Add(actor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(actor);
        }

        // GET: Actors/Edit/5
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
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Cover,ImageMimeType")] Actor actor, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    actor.ImageMimeType = Image.ContentType;
                    actor.Cover = new byte[Image.ContentLength];
                    Image.InputStream.Read(actor.Cover, 0, Image.ContentLength);
                }
                actor.FirstName = actor.FirstName + " " + actor.LastName;
                db.Entry(actor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(actor);
        }

        // GET: Actors/Delete/5
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
            if (actor.Cover != null)
            {
                return File(actor.Cover, actor.ImageMimeType);
            }
            return null;
        }

        //Get films partial view
        public ActionResult getActorFilm(int id)
        {
            return PartialView(this.db.FilmActors.Include(fa => fa.Film).Where(fa => fa.ActorId == id));
        }
    }
}
