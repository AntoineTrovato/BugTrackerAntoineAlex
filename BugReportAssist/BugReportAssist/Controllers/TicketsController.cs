using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;
using BugReportAssist.Models;

namespace BugReportAssist.Controllers
{
    public class TicketsController : Controller
    {
        
        // GET: Tickets
        public ActionResult Index(string sortOrder, string searchString)
        {
            using (var db = new ApplicationDbContext())
            {
                ViewBag.SujetSortParm = String.IsNullOrEmpty(sortOrder) ? "Sujet" : "";
                ViewBag.DescriptionSortParm = String.IsNullOrEmpty(sortOrder) ? "Description" : "";
                ViewBag.DateCreationSortParm = sortOrder == "Date de Creation" ? "date_cre" : "Date de Creation";
                ViewBag.DateModificationSortParm = sortOrder == "Date de Modification" ? "date_modif" : "Date de Modification";
                ViewBag.ApplicationSortParm = String.IsNullOrEmpty(sortOrder) ? "Application" : "";
                ViewBag.StatutSortParm = String.IsNullOrEmpty(sortOrder) ? "Statut" : "";
                ViewBag.ImportanceSortParm = String.IsNullOrEmpty(sortOrder) ? "Importance" : "";

                var tickets = from Ticket in db.Tickets
                              select Ticket;

                if (!String.IsNullOrEmpty(searchString))
                {
                    tickets = tickets.Where(Ticket => Ticket.Sujet.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "Sujet":
                        tickets = tickets.OrderBy(Ticket => Ticket.Sujet);
                        break;
                    case "Description":
                        tickets = tickets.OrderBy(Ticket => Ticket.Description);
                        break;
                    case "Date de Creation":
                        tickets = tickets.OrderBy(Ticket => Ticket.DateCreation);
                        break;
                    case "date_cre":
                        tickets = tickets.OrderBy(Ticket => Ticket.DateCreation);
                        break;
                    case "Date de Modification":
                        tickets = tickets.OrderBy(Ticket => Ticket.DateModification);
                        break;
                    case "date_modif":
                        tickets = tickets.OrderBy(Ticket => Ticket.DateModification);
                        break;
                    case "Application":
                        tickets = tickets.OrderBy(Ticket => Ticket.Application);
                        break;
                    case "Statut":
                        tickets = tickets.OrderBy(Ticket => Ticket.Statut);
                        break;
                    case "Importance":
                        tickets = tickets.OrderBy(Ticket => Ticket.Importance);
                        break;
                    default:
                        tickets = tickets.OrderBy(Ticket => Ticket.ID);
                        break;
                }
                return View(tickets.ToList());
            }
        }

        // GET: Tickets/NotResolved
        public ActionResult NotResolved()
        {
            using (var db = new ApplicationDbContext())
            {
                return View(db.Database.SqlQuery<Ticket>("Select * from dbo.Tickets where dbo.Tickets.Statut = 'Non Traité'").ToList());
            }
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var db = new ApplicationDbContext())
            {
                Ticket ticket = db.Tickets.Find(id);

                if (ticket == null)
                {
                    return HttpNotFound();
                }
                return View(ticket);
            }
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Sujet,Description,Application,Statut,Importance")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    ticket.DateCreation = DateTime.Now;
                    db.Tickets.Add(ticket);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new ApplicationDbContext())
            {
                Ticket ticket = db.Tickets.Find(id);
                if (ticket == null)
                {
                    return HttpNotFound();
                }
                return View(ticket);
            }
        }

        // POST: Tickets/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Sujet,Description,DateCreation,Application,Statut,Importance")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    db.Entry(ticket).State = EntityState.Modified;
                    db.SaveChanges();  
                }
                return RedirectToAction("Index");
            }
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new ApplicationDbContext())
            {
                Ticket ticket = db.Tickets.Find(id);
                if (ticket == null)
                {
                    return HttpNotFound();
                }
                return View(ticket);
            }
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                Ticket ticket = db.Tickets.Find(id);
                db.Tickets.Remove(ticket);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                using (var db = new ApplicationDbContext())
                {
                    db.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}
