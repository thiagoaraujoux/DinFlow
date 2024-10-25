using DinFlow.Models;
using Microsoft.AspNet.Identity;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DinFlow.Controllers
{
    [Authorize]
    public class EconomiasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Economias
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var economias = db.Economias.Where(e => e.UserId == userId).ToList();
            return View(economias);
        }

        // GET: Economias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Economia economia = db.Economias.Find(id);
            if (economia == null)
            {
                return HttpNotFound();
            }
            return View(economia);
        }

        // GET: Economias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Economias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Valor,Data")] Economia economia)
        {
            if (ModelState.IsValid)
            {
                // Assign the logged-in user's ID to the new Economia
                economia.UserId = User.Identity.GetUserId();

                db.Economias.Add(economia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(economia);
        }

        // GET: Economias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Economia economia = db.Economias.Find(id);
            if (economia == null)
            {
                return HttpNotFound();
            }
            return View(economia);
        }

        // POST: Economias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Valor,Data")] Economia economia)
        {
            if (ModelState.IsValid)
            {
                economia.UserId = User.Identity.GetUserId(); // Ensure UserId remains correct on Edit
                db.Entry(economia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(economia);
        }

        // GET: Economias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Economia economia = db.Economias.Find(id);
            if (economia == null)
            {
                return HttpNotFound();
            }
            return View(economia);
        }

        // POST: Economias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Economia economia = db.Economias.Find(id);
            db.Economias.Remove(economia);
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
    }
}
