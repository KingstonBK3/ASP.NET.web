using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FootballClub_PTVR18.Models;

namespace FootballClub_PTVR18.Controllers
{
    public class TeamsParticalController : Controller
    {
        private FootballContext db = new FootballContext();

        // GET: TeamsPartical
        public ActionResult Index()
        {
            return View(db.Teams.ToList());
        }

        // GET: TeamsPartical/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            team = db.Teams.Include(t => t.Players).FirstOrDefault(t => t.Id == id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }
        [ChildActionOnly]

        public ActionResult PlayersInTeam(int id)//Team.id
        {
            var teamPlayers = db.Players.Where(p => p.TeamId == id);
            return PartialView(teamPlayers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
