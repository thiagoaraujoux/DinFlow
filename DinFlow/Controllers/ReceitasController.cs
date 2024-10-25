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
    public class ReceitasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Receitas
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId(); // Obtém o ID do usuário autenticado
            var receitas = db.Receitas.Where(r => r.UserId == userId).ToList();
            return View(receitas);
        }

        // GET: Receitas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receita receita = db.Receitas.Find(id);
            if (receita == null)
            {
                return HttpNotFound();
            }
            return View(receita);
        }

        // GET: Receitas/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nome");
            return View();
        }

        // POST: Receitas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Descricao,Valor,Data,CategoriaId")] Receita receita)
        {
            if (ModelState.IsValid)
            {
                receita.UserId = User.Identity.GetUserId(); // Define automaticamente o UserId
                db.Receitas.Add(receita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nome", receita.CategoriaId);
            return View(receita);
        }

        // GET: Receitas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receita receita = db.Receitas.Find(id);
            if (receita == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nome", receita.CategoriaId);
            return View(receita);
        }

        // POST: Receitas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Descricao,Valor,Data,CategoriaId")] Receita receita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(receita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nome", receita.CategoriaId);
            return View(receita);
        }

        // GET: Receitas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receita receita = db.Receitas.Find(id);
            if (receita == null)
            {
                return HttpNotFound();
            }
            return View(receita);
        }

        // POST: Receitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Receita receita = db.Receitas.Find(id);
            db.Receitas.Remove(receita);
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
