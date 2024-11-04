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
    public class DespesasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Despesas
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var despesas = db.Despesas.Include(d => d.Categoria).Where(d => d.UserId == userId).ToList();
            return View(despesas);
        }

        // GET: Despesas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Despesa despesa = db.Despesas.Include(d => d.Tags).FirstOrDefault(d => d.Id == id);
            if (despesa == null)
            {
                return HttpNotFound();
            }
            return View(despesa);
        }

        // GET: Despesas/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nome");
            ViewBag.Tags = db.Tags.ToList();
            return View();
        }

        // POST: Despesas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Descricao,Valor,Data,CategoriaId")] Despesa despesa, int[] SelectedTags)
        {
            if (ModelState.IsValid)
            {
                despesa.UserId = User.Identity.GetUserId();
                if (SelectedTags != null)
                {
                    despesa.Tags = db.Tags.Where(t => SelectedTags.Contains(t.Id)).ToList();
                }
                db.Despesas.Add(despesa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nome", despesa.CategoriaId);
            ViewBag.Tags = db.Tags.ToList();
            return View(despesa);
        }

        // GET: Despesas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Despesa despesa = db.Despesas.Include(d => d.Tags).FirstOrDefault(d => d.Id == id);
            if (despesa == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nome", despesa.CategoriaId);
            ViewBag.Tags = db.Tags.ToList();

            despesa.SelectedTags = despesa.Tags?.Select(t => t.Id).ToList() ?? new List<int>();
            return View(despesa);
        }

        // POST: Despesas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Descricao,Valor,Data,CategoriaId,SelectedTags")] Despesa despesa)
        {
            if (ModelState.IsValid)
            {
                var despesaFromDb = db.Despesas.Include(d => d.Tags).FirstOrDefault(d => d.Id == despesa.Id);

                if (despesaFromDb == null)
                {
                    return HttpNotFound();
                }

                despesaFromDb.Nome = despesa.Nome;
                despesaFromDb.Descricao = despesa.Descricao;
                despesaFromDb.Valor = despesa.Valor;
                despesaFromDb.Data = despesa.Data;
                despesaFromDb.CategoriaId = despesa.CategoriaId;

                var selectedTags = despesa.SelectedTags ?? new List<int>();
                var currentTags = despesaFromDb.Tags.Select(t => t.Id).ToList();

                foreach (var tag in despesaFromDb.Tags.ToList())
                {
                    if (!selectedTags.Contains(tag.Id))
                    {
                        despesaFromDb.Tags.Remove(tag);
                    }
                }

                foreach (var tagId in selectedTags)
                {
                    if (!currentTags.Contains(tagId))
                    {
                        var tagToAdd = db.Tags.Find(tagId);
                        if (tagToAdd != null)
                        {
                            despesaFromDb.Tags.Add(tagToAdd);
                        }
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nome", despesa.CategoriaId);
            ViewBag.Tags = db.Tags.ToList();
            return View(despesa);
        }

        // GET: Despesas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Despesa despesa = db.Despesas.Find(id);
            if (despesa == null)
            {
                return HttpNotFound();
            }
            return View(despesa);
        }

        // POST: Despesas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Despesa despesa = db.Despesas.Find(id);
            db.Despesas.Remove(despesa);
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
