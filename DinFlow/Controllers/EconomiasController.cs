using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DinFlow.Models;
using Microsoft.AspNet.Identity;

namespace DinFlow.Controllers
{
    [Authorize]
    public class EconomiasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Economias
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId(); // Obtém o ID do usuário autenticado
            var receitas = db.Receitas.Where(r => r.UserId == userId).ToList();
            var economias = db.Economias.Include(e => e.User);
            return View(economias.ToList());
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
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Economias/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Valor,Data,UserId")] Economia economia)
        {
            if (ModelState.IsValid)
            {
                db.Economias.Add(economia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", economia.UserId);
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
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", economia.UserId);
            return View(economia);
        }

        // POST: Economias/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Valor,Data,UserId")] Economia economia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(economia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", economia.UserId);
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
