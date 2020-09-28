using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyPainterJPTVR18.Models;

namespace MyPainterJPTVR18.Controllers
{
    public class PicturesController : Controller
    {
        private PainterContext db = new PainterContext();

        // GET: Pictures
        public ActionResult Index()
        {
            return View(db.Pictures.OrderBy(a => a.Title).ToList());
        }

        // GET: Pictures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return HttpNotFound();
            }

            picture = db.Pictures.Include(p => p.painter).FirstOrDefault(t => t.Id == id);
            return View(picture);
        }

        // GET: Pictures/Create
        public ActionResult Create()
        {
            ViewBag.PainterId = new SelectList(db.Painters,"id","Name");
            return View();
        }

        // POST: Pictures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Year,Museum,Canvas,CanvasType, PainterId")] Picture picture, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    picture.CanvasType = Image.ContentType;
                    picture.Canvas = new byte[Image.ContentLength];
                    Image.InputStream.Read(picture.Canvas, 0, Image.ContentLength);
                }
                db.Pictures.Add(picture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(picture);
        }

        // GET: Pictures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            ViewBag.PainterId = new SelectList(db.Painters, "id", "Name");
            return View(picture);
        }

        // POST: Pictures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Year,Museum,Canvas,CanvasType, PainterId")] Picture picture, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    picture.CanvasType = Image.ContentType;
                    picture.Canvas = new byte[Image.ContentLength];
                    Image.InputStream.Read(picture.Canvas, 0, Image.ContentLength);
                }
                db.Entry(picture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(picture);
        }

        // GET: Pictures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            return View(picture);
        }

        // POST: Pictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Picture picture = db.Pictures.Find(id);
            db.Pictures.Remove(picture);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public FileContentResult GetImage(int id)
        {
            //запрос в БД таблица Players по переданному id
            Picture picture = db.Pictures.FirstOrDefault(p => p.Id == id);
            if (picture != null)
            {
                return File(picture.Canvas, picture.CanvasType);
            }
            return null;
        }
    }
}
