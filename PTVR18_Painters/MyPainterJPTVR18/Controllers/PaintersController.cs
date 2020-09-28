﻿using System;
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
    public class PaintersController : Controller
    {
        private PainterContext db = new PainterContext();

        // GET: Painters
        public ActionResult Index()
        {
            //var painters = db.Painters.Include(p => p.pictures);
            return View(db.Painters.ToList());
        }

        // GET: Painters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Painter painter = db.Painters.Find(id);
            painter = db.Painters.Include(p => p.pictures).FirstOrDefault(p => p.Id == id);
            if (painter == null)
            {
                return HttpNotFound();
            }
            return View(painter);
        }

        //---------------GetImage
        public FileContentResult GetImage(int id)
        {
            //запрос в БД таблица Painters по переданному id
            Painter painter = db.Painters.FirstOrDefault(g => g.Id == id);
            if (painter != null)
            {
                return File(painter.Photo, painter.PhotoType);
            }
            return null;
        }

        // GET: Painters/Create
        public ActionResult Create()
        {
            ViewBag.PictureId = new SelectList(db.Pictures, "Id", "Name");
            return View();
        }

        // POST: Painters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Country,Biography,Photo,PhotoType")] Painter painter, HttpPostedFileBase
        Image)
        {
            if (ModelState.IsValid)
            { 
                //если выбрано и передано изображение
                if (Image != null)
                {
                    painter.PhotoType = Image.ContentType;
                    painter.Photo = new byte[Image.ContentLength];
                    Image.InputStream.Read(painter.Photo, 0, Image.ContentLength);
                }

                    db.Painters.Add(painter);
                    db.SaveChanges();
                    return RedirectToAction("Index");
            }

            //ViewBag.PictureId = new SelectList(db.Pictures, "Id", "Name", painter);
            return View(painter);
        }

        // GET: Painters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Painter painter = db.Painters.Find(id);
            if (painter == null)
            {
                return HttpNotFound();
            }
            //ViewBag.PictureId = new SelectList(db.Pictures, "Id", "Name", painter.PictureId);
            return View(painter);
        }

        // POST: Painters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Country,Biography,Photo,PhotoType")] Painter painter, HttpPostedFileBase
        Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    painter.PhotoType = Image.ContentType;
                    painter.Photo = new byte[Image.ContentLength];
                    Image.InputStream.Read(painter.Photo, 0, Image.ContentLength);
                }

                db.Entry(painter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.PictureId = new SelectList(db.Pictures, "Id", "Name", painter.PictureId);
            return View(painter);
        }

        // GET: Painters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Painter painter = db.Painters.Find(id);
            painter = db.Painters.Include(p => p.pictures).FirstOrDefault(p => p.Id == id);
            if (painter == null)
            {
                return HttpNotFound();
            }
            return View(painter);
        }

        // POST: Painters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Painter painter = db.Painters.Find(id);
            db.Painters.Remove(painter);
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


        [ChildActionOnly]
        public ActionResult PicturesInPainters(int id)
        {
            var pp = db.Pictures.Where(p => p.PainterId == id);
            return PartialView(pp);
        }
    }
}
