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
    public class PaintersMenuController : Controller
    {
        private PainterContext db = new PainterContext();

        // GET: PaintersMenu
        public ActionResult Index()
        {
            return View(db.Painters.ToList());
        }

        [ChildActionOnly]
        public ActionResult PaintersMenu()
        {
            var paintersName = db.Painters.ToList();
            return PartialView(paintersName);
        }

        public ActionResult Browse(int id)
        {
            var painterPictures = db.Painters.Include("Pictures").Single(g => g.Id == id);
            return View(painterPictures);
        }
    }
}
